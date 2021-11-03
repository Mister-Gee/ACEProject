using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.DTOModel
{
    public class AssetDTO
    {
    }

    public class FileSignatureValidationAttribute : ValidationAttribute
    {
        private static readonly Dictionary<string, List<byte[]>> _fileSignature =
              new Dictionary<string, List<byte[]>>
            {
                  { "header", new List<byte[]>
                      {
                          new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                          new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                          new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                          new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
                          new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                          new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                          new byte[] { 0x25, 0x50, 0x44, 0x46 },
                          new byte[] { 0x50, 0x4B, 0x03, 0x04 },
                          new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0a },
                          new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 },
                          new byte[] { 0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06, 0x00 },
                          new byte[] { 0x42, 0x4D}
                      }
                  },
               };
        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var memberNames = new string[] { validationContext.MemberName };
            if (value == null)
            { return ValidationResult.Success; }

            if (value is IFormFileCollection)
            {
                var files = value as IFormFileCollection;
                foreach (var item in files)
                {
                    using (var reader = new BinaryReader(item.OpenReadStream()))
                    {
                        var signatures = _fileSignature["header"];
                        var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

                        if (signatures.Any(signature =>
                            headerBytes.Take(signature.Length).SequenceEqual(signature)))
                        {
                            return ValidationResult.Success;
                        }
                    }
                }
            }
            else if (value is IFormFile)
            {
                var files = value as IFormFile;
                using (var reader = new BinaryReader(files.OpenReadStream()))
                {
                    var signatures = _fileSignature["header"];
                    var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

                    if (signatures.Any(signature =>
                        headerBytes.Take(signature.Length).SequenceEqual(signature)))
                    {
                        return ValidationResult.Success;
                    }
                }
            }


            return new ValidationResult(GetErrorMessage(), memberNames);
        }

        string GetErrorMessage()
        {
            return $"File uploaded is not of the required format, allowed format(.JPG, .PDF and .DOCX only).";
        }
    }

    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var memberNames = new string[] { validationContext.MemberName };

            if (!(value is IFormFileCollection files)) return ValidationResult.Success;
            return files.Any(item => item.Length > _maxFileSize) ? new ValidationResult(GetErrorMessage(), memberNames) : ValidationResult.Success;
        }

        string GetErrorMessage() => $"Maximum allowed file size is { _maxFileSize / 1000 }KB.";
    }
}
