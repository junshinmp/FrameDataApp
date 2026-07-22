using System;
using System.Collections.Generic;
using System.Text;

namespace FrameDataApp.Models
{
    public enum CancelType
    {
        None = 0,
        SpecialCancelable = 1,
        SuperCancelable = 2,
        DashCancelable = 3,
        JumpCancelable = 4,
    }
}
