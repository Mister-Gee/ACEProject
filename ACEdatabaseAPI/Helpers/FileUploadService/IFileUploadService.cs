using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.Helpers.FileUploadService
{
    public interface IFileUploadService
    {
        string AddPhoto(IFormFile file, string folderId = null);
        //Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
