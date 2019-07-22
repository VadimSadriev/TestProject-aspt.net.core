using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Core;

namespace TestProject.WebServer.Controllers
{
    [Authorize(Roles = "Admin, CanViewRequest")]
    public class RequestController : Controller
    {
        private readonly IRepository repository;
        IHubContext<RequestNotificationHub> requestNotificationHub;

        public RequestController(IRepository repository,
                                 IHubContext<RequestNotificationHub> requestNotificationHub)
        {
            this.repository = repository;
            this.requestNotificationHub = requestNotificationHub;
        }


        #region public methods

        public IActionResult Index()
        {
            ViewData["Title"] = "Заявки";

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, CanViewRequest")]
        public async Task<ApiResponse> GetSecondaryData()
        {
            var requestTypes = await repository.RequestTypes.Items.Include(x => x.RequestTypeFields)
                                              .ToListAsync();

            var requestTypesVm = requestTypes.Select(x => new RequestTypeVm(x));

            var requestStatuses = Enum.GetValues(typeof(RequestStatus)).Cast<RequestStatus>()
                                  .Select(x => new { value = x.ToString(), displayName = x.GetRuName() });

            return new ApiResponse
            {
                Response = new
                {
                    requestTypes = requestTypesVm,
                    requestStatuses
                }
            };
        }

        [HttpPost]
        [Authorize(Roles = "Admin, CanViewRequest")]
        public async Task<ApiResponse> GetRequests([FromBody]FilterOptions filterOptions)
        {
            var requests = await repository.AppUsers.IsAdmin()
                           ? repository.Requests.Items
                             .Include(x => x.RequestType)
                             .Include(x => x.AppUser)
                           : repository.Requests.Items
                             .Include(x => x.RequestType)
                             .Include(x => x.AppUser)
                             .Where(x => x.AppUserId == repository.CurrentUser.Id);

            var filtredRequests = Filter.FilterItems(requests, filterOptions);

            var totalCount = filtredRequests.Count();

            var requestList = await filtredRequests.Skip((filterOptions.CurrentPage - 1) * filterOptions.PageSize).Take(filterOptions.PageSize).ToListAsync();

            var requestsPageList = new PageList<RequestListVm>(requestList
                                                               .Select(x => new RequestListVm(x))
                                                               .ToList(), totalCount, filterOptions);



            return new ApiResponse
            {
                Response = new
                {
                    Requests = requestsPageList,

                    requestsPageList.TotalPages,
                    requestsPageList.TotalCount,
                    requestsPageList.HasPreviousPage,
                    requestsPageList.HasNextPage,
                }
            };
        }


        [HttpPost]
        public async Task<ApiResponse<RequestVm>> GetRequest(long id)
        {
            var request = await repository.Requests.Items
                          .Include(x => x.RequestValues)
                          .FirstOrDefaultAsync(x => x.Id == id);

            if (request == null)
            {
                return new ApiResponse<RequestVm>
                {
                    ErrorMessage = "Заявка не существует или была удалена"
                };
            }

            var requestType = await repository.RequestTypes.Items
                              .Include(x => x.RequestTypeFields)
                              .FirstOrDefaultAsync(x => x.Id == request.RequestTypeId);

            if (requestType == null)
            {
                return new ApiResponse<RequestVm>
                {
                    ErrorMessage = "Возникла ошибка при обработке вашего запроса"
                };
            }

            var requestVm = new RequestVm(request)
            {
                RequestType = new RequestTypeVm(requestType)
            };

            foreach (var fieldType in requestVm.RequestType.RequestTypeFields)
            {
                var requestValue = request.RequestValues.FirstOrDefault(x => x.RequestTypeFieldId == fieldType.Id);

                if (requestValue != null)
                {
                    fieldType.RequestValue = new RequestValueVm(requestValue);
                }
            }

            return new ApiResponse<RequestVm>
            {
                Response = requestVm
            };
        }

        [HttpPost]
        public async Task<ApiResponse<RequestTypeVm>> GetFieldsData(long reqId, long typeId)
        {
            var requestType = await repository.RequestTypes.Items
                                    .Include(x => x.RequestTypeFields)
                                    .FirstOrDefaultAsync(x => x.Id == typeId);

            var defaultErrorResponse = new ApiResponse<RequestTypeVm>
            {
                ErrorMessage = "Произошла ошибка при обработке вашего запроса"
            };

            if (requestType == null)
            {
                return defaultErrorResponse;
            }

            var dbResponse = await repository.Requests.GetRequestValuesById(reqId);

            if (dbResponse.Success)
            {
                var requestTypeVm = new RequestTypeVm(requestType);

                foreach (var fieldType in requestTypeVm.RequestTypeFields)
                {
                    var requestValue = dbResponse.Response.FirstOrDefault(x => x.RequestTypeFieldId == fieldType.Id);

                    if (requestValue != null)
                    {
                        fieldType.RequestValue = new RequestValueVm(requestValue);
                    }
                }

                return new ApiResponse<RequestTypeVm>
                {
                    Response = requestTypeVm
                };
            }

            return defaultErrorResponse;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, CanApplyRequest")]
        public async Task<ApiResponse<RequestVm>> AddRequest([FromBody]RequestVm requestVm)
        {
            var dbResponse = await repository.Requests.AddAsync(requestVm);

            if (dbResponse.Success)
            {
                await requestNotificationHub.Clients.All.SendAsync("NotifyAdmins", $"Заявка с номером {dbResponse.Response.Id} только что была создана");

                var requestType = await repository.RequestTypes.Items
                                        .Include(x => x.RequestTypeFields)
                                        .FirstOrDefaultAsync(x => x.Id == dbResponse.Response.RequestTypeId);

                var reqVm = new RequestVm(dbResponse.Response)
                {
                    RequestType = new RequestTypeVm(requestType)
                };

                foreach (var fieldType in reqVm.RequestType.RequestTypeFields)
                {
                    var requestValue = dbResponse.Response.RequestValues.FirstOrDefault(x => x.RequestTypeFieldId == fieldType.Id);

                    if (requestValue != null)
                    {
                        fieldType.RequestValue = new RequestValueVm(requestValue);
                    }
                }

                return new ApiResponse<RequestVm>
                {
                    Response = reqVm,
                    Message = dbResponse.Message
                };
            }

            return new ApiResponse<RequestVm>
            {
                ErrorMessage = dbResponse.ErrorMessage
            };
        }

        [HttpPost]
        [Authorize(Roles = "Admin, CanEditRequest")]
        public async Task<ApiResponse<RequestVm>> UpdateRequest([FromBody]RequestVm requestVm)
        {
            var dbResponse = await repository.Requests.UpdateAsync(requestVm);

            if (dbResponse.Success)
            {
                var requestType = await repository.RequestTypes.Items
                                        .Include(x => x.RequestTypeFields)
                                        .FirstOrDefaultAsync(x => x.Id == dbResponse.Response.RequestTypeId);

                var reqVm = new RequestVm(dbResponse.Response)
                {
                    RequestType = new RequestTypeVm(requestType)
                };

                foreach (var fieldType in reqVm.RequestType.RequestTypeFields)
                {
                    var requestValue = dbResponse.Response.RequestValues.FirstOrDefault(x => x.RequestTypeFieldId == fieldType.Id);

                    if (requestValue != null)
                    {
                        fieldType.RequestValue = new RequestValueVm(requestValue);
                    }
                }

                return new ApiResponse<RequestVm>
                {
                    Response = reqVm,
                    Message = dbResponse.Message
                };
            }

            return new ApiResponse<RequestVm>
            {
                ErrorMessage = dbResponse.ErrorMessage
            };
        }
        #endregion

        #region private helpers


        #endregion


        public IActionResult Seed()
        {
            repository.Requests.Seed();

            return Content("Seeded");
        }
    }
}
