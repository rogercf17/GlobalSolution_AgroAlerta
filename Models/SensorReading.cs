namespace AgroAlerta.Models
{
    public class SensorReading
    {
        public int Id { get; set; }

        public int FarmId { get; set; }

        public double Temperature { get; set; }

        public double AirHumidity { get; set; }

        public double SoilHumidity { get; set; }

        public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

        public Farm? Farm { get; set; }
    }
}
