using System;
namespace rms.Models
{
    public class ApplicationActivity
    {
        public int Id { get; set; }
        public string Date {get; set; }
        public bool IsValid { get; set; }
        public bool Canceled { get; set; }
        public string Source { get; set; }
        public string EndingHour { get; set; }
        public string StartingHour { get; set; }
        public string Owner { get; set; }

        public ApplicationActivity()
        {
        }
    }
}
