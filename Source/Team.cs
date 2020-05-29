using System;

namespace Codenation.Challenge
{
    public class Team
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Player Captain { get; set; }
        public DateTime CreateDate { get; set; }
        public string MainShirtColor { get; set; }
        public string SecondaryShirtColor { get; set; }
    }
}