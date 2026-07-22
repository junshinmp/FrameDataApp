using System;
using System.Collections.Generic;
using System.Text;

namespace FrameDataApp.Models
{
    public class FrameData
    {
        public int StartUp {  get; set; }
        public int Active { get; set; }
        public int Recovery { get; set; 

        public int OnBlock { get; set; }
        public int OnHit { get; set; }

        public int TotalFrames => StartUp + Active + Recovery;

        // Can be minus, depends on game (just can't be too minus)
        public bool IsSafeOnBlock => OnBlock >= -3;
    }
}
