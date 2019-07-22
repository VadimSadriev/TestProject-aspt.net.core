using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Core;

namespace TestProject.WebServer.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        private readonly IHostingEnvironment appEnvironment;

        public FileController(IHostingEnvironment appEnvironment)
        {
            this.appEnvironment = appEnvironment;
        }

        public async Task<ApiResponse<FileResultVm>> Upload(IFormFile file1)
        {
            IFormFile file;
            try
            {
                file = HttpContext.Request.Form.Files[0];
            }
            catch (Exception)
            {
                file = null;
            }

            if (file != null)
            {
               var directory = Directory.CreateDirectory(appEnvironment.WebRootPath + $"\\Content\\TemporaryFiles");

                var fileResultVm = new FileResultVm
                {
                    Name = file.FileName,
                    FileGuid = $"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}",
                };

                using (var fileStream = new FileStream(Path.Combine(directory.FullName, fileResultVm.FileGuid), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return new ApiResponse<FileResultVm>
                {
                    Message = "Файл загружен на сервер",
                    Response = fileResultVm
                };
            }

            return new ApiResponse<FileResultVm>
            {
                ErrorMessage = "Произошла ошибка при загрузке файла"
            };
        }
    }
}
