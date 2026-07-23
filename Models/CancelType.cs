using System;
using System.Collections.Generic;
using System.Text;

namespace FrameDataApp.Models
{
    public enum CancelType
    {
        None = 0,
        StringCancelable=1,
        SpecialCancelable = 2,
        SuperCancelable = 3,
        DashCancelable = 4,
        JumpCancelable = 5
    }
}
