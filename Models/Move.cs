using System;
using System.Collections.Generic;
using System.Text;

namespace FrameDataApp.Models
{
    public class Move
    {
        public string MoveName { get; set; } = String.Empty;
        public string CommandInput { get; set; } = String.Empty;
        public List<CancelType> Cancelable { get; set; } = new();
        public FrameData Stats { get; set; } = new FrameData();
        public int CharacterId { get; set; }
    }
}
