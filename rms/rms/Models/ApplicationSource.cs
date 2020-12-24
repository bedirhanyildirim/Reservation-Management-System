using System;
namespace rms.Models
{
    public class ApplicationSource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
        public string OwnerId { get; set; }

        public ApplicationSource() {

        }
    }
}
