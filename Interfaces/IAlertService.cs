using AgroAlerta.Models;

namespace AgroAlerta.Interfaces
{
    public interface IAlertService
    {
        List<Alert> AnalyzeReading(SensorReading reading);
    }
}
