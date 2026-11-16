namespace AgroAlerta.Models
{
    public class Alert
    {
        public int Id { get; set; }

        public int FarmId { get; set; }

        public string Type { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Severity { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Farm? Farm { get; set; }
    }
}
