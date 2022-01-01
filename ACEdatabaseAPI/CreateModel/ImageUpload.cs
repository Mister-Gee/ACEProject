using ACEdatabaseAPI.DTOModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.CreateModel
{
    public class ImageUpload
    {
        //[FileSignatureValidation]
        //[MaxFileSize(2000)]
        [Required]
        public IFormFile Image { get; set; }
    }
}
