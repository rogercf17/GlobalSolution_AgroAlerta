namespace AgroAlerta.Models
{
    public class Farm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public ICollection<SensorReading> Readings { get; set; } 
            = new List<SensorReading>();
        public ICollection<Alert> Alerts { get; set; }
            = new List<Alert>();
    }
}
