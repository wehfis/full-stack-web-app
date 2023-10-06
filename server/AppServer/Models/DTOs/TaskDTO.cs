namespace AppServer.Models.DTOs
{
    public class HeavyTaskDTO
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string? result { get; set; }
        public DateTime startedAt { get; set; }
        public DateTime? finishedAt { get; set; }
        public uint percentageDone { get; set; }
    }
}
