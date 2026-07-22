using System;
using System.Collections.Generic;
using System.Text;

namespace FrameDataApp.Models
{
    public class Game
    {
        public string Title { get; set; } = string.Empty;
        public string Developer { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public List<Character> Characters { get; set; } = new();
    }
}
