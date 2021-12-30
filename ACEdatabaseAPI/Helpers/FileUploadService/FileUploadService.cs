using ACEdatabaseAPI.Model;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.Helpers.FileUploadService
{
    public class FileUploadService : IFileUploadService
    {
        private readonly Cloudinary _cloudinary;
        public FileUploadService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account
            (
                "ghost-axis-technology",
                "137937544362995",
                "eIjp_RIsjwEB3GaXaeIsBqpb7Z0"
            );

            _cloudinary = new Cloudinary(acc);
        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file, string folderId = null)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                await using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(System.IO.Path.GetRandomFileName(), stream),

                    // Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                if (!string.IsNullOrEmpty(folderId))
                {
                    uploadParams.Folder = $"{folderId}/";
                }
                uploadResult = await _cloudinary.UploadAsync(uploadParams).ConfigureAwait(false);
            }

            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            var result = await _cloudinary.DestroyAsync(deleteParams).ConfigureAwait(false);

            return result;
        }
    }
}
