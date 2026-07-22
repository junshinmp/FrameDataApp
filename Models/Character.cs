using System;
using System.Collections.Generic;
using System.Text;

namespace FrameDataApp.Models
{
    public class Character
    {
        public string Name { get; set; } = string.Empty;

        public double WalkSpeed { get; set; }

        public double DashSpeed { get; set; }

        // 1 : Many relationship
        public List<Game> Games { get; set; } = new();

        // 1 : Many relationship
        public List<Move> Moves { get; set; } = new();
    }
}
