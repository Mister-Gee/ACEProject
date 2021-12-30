using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.DTOModel
{
    public class RegProgressDTO
    {
        public int ProgressPercent { get; set; }
        public bool Biometrics { get; set; } = false;
        public bool UserImage { get; set; } = false;
        public bool Biodata { get; set; } = false;
        public bool CourseReg { get; set; } = false;
        public bool Medical { get; set; } = false;
        public int CompletedRegProcess { get; set; }
        public int TotalRegProcess { get; set; }
    }
}
