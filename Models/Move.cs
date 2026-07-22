using System;
using System.Collections.Generic;
using System.Text;

namespace FrameDataApp.Models
{
    public class Move
    {
        public string MoveName { get; set; } = String.Empty;
        public string CommandInput { get; set; } = String.Empty;
        public CancelType Cancelable { get; set; }
        public FrameData Stats { get; set; } = new FrameData();
    }
}
