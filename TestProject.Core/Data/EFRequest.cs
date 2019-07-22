using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Core
{
    public class EFRequest
    {
        private DataContext context;
        private AppUser currentUser;

        public EFRequest(DataContext context, AppUser currentUser)
        {
            this.context = context;
            this.currentUser = currentUser;
        }

        public IQueryable<Request> Items => context.Requests.Where(x => !x.IsDeleted);

        public async Task<DbResponse<Request>> AddAsync(RequestVm requestVm)
        {
            try
            {
                var existingRequest = await context.Requests.FirstOrDefaultAsync(x => x.Id == requestVm.Id);

                if (existingRequest != null)
                {
                    return new DbResponse<Request>
                    {
                        ErrorMessage = "Заявка уже отправлена"
                    };
                }

                if (!context.RequestTypes.Any(x => x.Id == requestVm.RequestType.Id && !x.IsDeleted))
                {
                    return new DbResponse<Request>
                    {
                        ErrorMessage = "Данный тип заявки не существует или был удалён"
                    };
                }

                var request = new Request
                {
                    DateCreated = DateTime.Now,
                    RequestTypeId = requestVm.RequestType.Id,
                    AppUserId = currentUser.Id
                };

                foreach (var requestTypeField in requestVm.RequestType.RequestTypeFields)
                {
                    if (requestTypeField.RequestValue != null)
                    {
                        request.RequestValues.Add(new RequestValue
                        {
                            StringValue = requestTypeField.RequestValue.StringValue,
                            IntValue = requestTypeField.RequestValue.IntValue,
                            DateValue = requestTypeField.RequestValue.DateValue.ToDateTime(),
                            TimeValue = requestTypeField.RequestValue.TimeValue.ToTimeSpan(),
                            FileValue = requestTypeField.RequestValue.FileValue,
                            FileName = requestTypeField.RequestValue.FileName,
                            RequestTypeFieldId = requestTypeField.Id,
                            RequestId = request.Id
                        });

                        if (requestTypeField.Type.ToEnumFieldType() == RequestFieldType.File)
                        {
                            var env = DI.Environment;

                            var temporaryFilePath = Path.Combine(env.WebRootPath, $"Content/TemporaryFiles/{requestTypeField.RequestValue.FileValue}");

                            if (File.Exists(temporaryFilePath))
                            {
                               var directoryUserFiles = Directory.CreateDirectory(Path.Combine(env.WebRootPath, "Content/UserFiles"));

                                File.Move(temporaryFilePath, Path.Combine(directoryUserFiles.FullName, requestTypeField.RequestValue.FileValue));
                            }
                        }
                    }
                }

                context.Requests.Add(request);

                await context.SaveChangesAsync();

                return new DbResponse<Request>
                {
                    Response = request,
                    Message = $"Заявка с номером {request.Id} успешно подана"
                };

            }
            catch (Exception ex)
            {
                // log..
                return new DbResponse<Request>
                {
                    ErrorMessage = "Произошла ошибка при подачи заявки"
                };
            }
        }

        public async Task<DbResponse<Request>> UpdateAsync(RequestVm requestVm)
        {
            try
            {
                var request = await context.Requests.Include(x => x.RequestValues).FirstOrDefaultAsync(x => x.Id == requestVm.Id);

                if (request == null)
                {
                    return new DbResponse<Request>
                    {
                        ErrorMessage = "Заявки не существует или была удалена"
                    };
                }

                if (!context.RequestTypes.Any(x => !x.IsDeleted && x.Id == requestVm.RequestType.Id))
                {
                    return new DbResponse<Request>
                    {
                        ErrorMessage = "Данный тип заявки не существует или был удалён"
                    };
                }

                request.Name = requestVm.Name;
                request.Status = requestVm.Status.ToEnumStatus();
                request.RequestTypeId = requestVm.RequestType.Id;

                var requestValues = request.RequestValues.ToDictionary(x => x.Id);

                foreach (var requestTypeField in requestVm.RequestType.RequestTypeFields)
                {
                    if (requestValues.TryGetValue(requestTypeField.RequestValue.Id, out var requestValue))
                    {
                        requestValue.StringValue = requestTypeField.RequestValue.StringValue;
                        requestValue.IntValue = requestTypeField.RequestValue.IntValue;
                        requestValue.DateValue = requestTypeField.RequestValue.DateValue.ToDateTime();
                        requestValue.TimeValue = requestTypeField.RequestValue.TimeValue.ToTimeSpan();
                        requestValue.FileValue = requestTypeField.RequestValue.FileValue;
                        requestValue.FileName = requestTypeField.RequestValue.FileName;
                    }
                    else
                    {
                        request.RequestValues.Add(new RequestValue
                        {
                            StringValue = requestTypeField.RequestValue.StringValue,
                            IntValue = requestTypeField.RequestValue.IntValue,
                            DateValue = requestTypeField.RequestValue.DateValue.ToDateTime(),
                            TimeValue = requestTypeField.RequestValue.TimeValue.ToTimeSpan(),
                            FileValue = requestTypeField.RequestValue.FileValue,
                            FileName = requestTypeField.RequestValue.FileName,
                            RequestTypeFieldId = requestTypeField.Id,
                            RequestId = request.Id
                        });
                    }

                    if (requestTypeField.Type.ToEnumFieldType() == RequestFieldType.File)
                    {
                        var env = DI.Environment;

                        var temporaryFilePath = Path.Combine(env.WebRootPath, $"Content/TemporaryFiles/{requestTypeField.RequestValue.FileValue}");

                        if (File.Exists(temporaryFilePath))
                        {
                            var directoryUserFiles = Directory.CreateDirectory(Path.Combine(env.WebRootPath, "Content/UserFiles"));

                            File.Move(temporaryFilePath, Path.Combine(directoryUserFiles.FullName, requestTypeField.RequestValue.FileValue));
                        }
                    }
                }

                await context.SaveChangesAsync();

                return new DbResponse<Request>
                {
                    Response = request,
                    Message = "Заявка успешно обновлена"
                };

            }
            catch (Exception ex)
            {
                return new DbResponse<Request>
                {
                    ErrorMessage = "Произошла ошибка при обновлении заявки"
                };
            }
        }

        public async Task<DbResponse<List<RequestValue>>> GetRequestValuesById(long id)
        {
            try
            {
                var requestValues = await context.RequestValues.Where(x => x.RequestId == id && !x.RequestTypeField.IsDeleted).ToListAsync();

                return new DbResponse<List<RequestValue>>
                {
                    Response = requestValues
                };
            }
            catch (Exception ex)
            {
                return new DbResponse<List<RequestValue>>
                {
                    ErrorMessage = "Произошла ошибка при обработке вашего запроса"
                };
            }
        }

        public void Seed()
        {
            try
            {
                var requestType = new RequestType
                {
                    Name = "Base req type",
                    AppUserId = currentUser?.Id
                };
                context.Add(requestType);
                for (var i = 0; i < 100; i++)
                {
                    context.Requests.Add(new Request
                    {
                        Name = $"Request number - {i}",
                        AppUserId = currentUser?.Id,
                        DateCreated = DateTime.Now,
                        RequestTypeId = requestType.Id
                    });
                }

                context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
