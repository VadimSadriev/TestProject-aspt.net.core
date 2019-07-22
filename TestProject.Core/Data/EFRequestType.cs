using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Core
{
    public class EFRequestType
    {
        private readonly DataContext context;

        public EFRequestType(DataContext context)
        {
            this.context = context;
        }

        public IQueryable<RequestType> Items => context.RequestTypes.Where(x => !x.IsDeleted);

        public async Task<DbResponse<RequestType>> AddAsync(RequestTypeVm reqTypeVm)
        {
            try
            {
                if (context.RequestTypes.Any(x => x.Name == reqTypeVm.Name && !x.IsDeleted))
                {
                    return new DbResponse<RequestType>
                    {
                        ErrorMessage = $"Тип заявки с именем {reqTypeVm.Name} уже существует"
                    };
                }

                var requestType = new RequestType
                {
                    Name = reqTypeVm.Name
                };

                requestType.RequestTypeFields = reqTypeVm.RequestTypeFields
                                                .Where(x => !string.IsNullOrEmpty(x.Name))
                                                .Select(x => new RequestTypeField
                                                {
                                                    Name = x.Name,
                                                    Type = Enum.Parse<RequestFieldType>(x.Type),
                                                    RequestTypeId = requestType.Id
                                                }).ToList();

                context.RequestTypes.Add(requestType);

                await context.SaveChangesAsync();

                return new DbResponse<RequestType>
                {
                    Message = "Тип заявки успешно создан",
                    Response = requestType
                };

            }
            catch (Exception ex)
            {
                // log..
                return new DbResponse<RequestType>
                {
                    ErrorMessage = "Возникла ошибка во время создания типа заявки"
                };
            }
        }

        public async Task<DbResponse<RequestType>> UpdateAsync(RequestTypeVm requestTypeVm)
        {
            try
            {
                var requestType = context.RequestTypes.Include(x => x.RequestTypeFields).FirstOrDefault(x => x.Id == requestTypeVm.Id);

                if (requestType == null)
                {
                    return new DbResponse<RequestType>
                    {
                        ErrorMessage = "Тип заявки не существует или была удалена"
                    };
                }

                if (context.RequestTypes.Any(x => x.Name == requestTypeVm.Name && x.Id != requestTypeVm.Id && !x.IsDeleted))
                {
                    return new DbResponse<RequestType>
                    {
                        ErrorMessage = $"Типа заявки с названием {requestTypeVm.Name} уже существует"
                    };
                }

                requestType.Name = requestTypeVm.Name;

                var currentFieldTypesDict = requestType.RequestTypeFields.ToDictionary(x => x.Id);

                foreach (var fieldTypeVm in requestTypeVm.RequestTypeFields)
                {
                    if (currentFieldTypesDict.TryGetValue(fieldTypeVm.Id, out var fieldType))
                    {
                        if (string.IsNullOrEmpty(fieldTypeVm.Name))
                        {
                            return new DbResponse<RequestType>
                            {
                                ErrorMessage = "Название типа не может быть пустым"
                            };
                        }

                        fieldType.Name = fieldTypeVm.Name;
                        fieldType.Type = Enum.Parse<RequestFieldType>(fieldTypeVm.Type);
                        fieldType.IsDeleted = fieldTypeVm.IsDeleted;

                    }
                    else
                    {
                        if (string.IsNullOrEmpty(fieldTypeVm.Name))
                        {
                            return new DbResponse<RequestType>
                            {
                                ErrorMessage = "Название типа не может быть пустым"
                            };
                        }

                        requestType.RequestTypeFields.Add(new RequestTypeField
                        {
                            Name = fieldTypeVm.Name,
                            Type = Enum.Parse<RequestFieldType>(fieldTypeVm.Type),
                            IsDeleted = fieldTypeVm.IsDeleted

                        });
                    }
                }

                await context.SaveChangesAsync();

                return new DbResponse<RequestType>
                {
                    Message = "Тип заявки успешно изменён",
                    Response = requestType
                };

            }
            catch (Exception ex)
            {
                // log..
                return new DbResponse<RequestType>
                {
                    ErrorMessage = "Произошла ошибка при обновлении типа заявки"
                };
            }
        }

        public async Task<DbResponse> DeleteAsync(long id)
        {
            try
            {
                var requestType = context.RequestTypes.FirstOrDefault(x => x.Id == id);

                if (requestType == null)
                {
                    return new DbResponse
                    {
                        ErrorMessage = "Типа заявки не существует или был удалён"
                    };
                }

                requestType.IsDeleted = true;

                await context.SaveChangesAsync();

                return new DbResponse
                {
                    Message = "Тип заявки успешно удалён"
                };
            }
            catch (Exception ex)
            {
                return new DbResponse
                {
                    ErrorMessage = "Тип заявки успешно удалён"
                };
            }
        }
    }
}
