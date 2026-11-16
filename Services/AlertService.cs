using AgroAlerta.Interfaces;
using AgroAlerta.Models;

namespace AgroAlerta.Services
{
    public class AlertService : IAlertService
    {
        public List<Alert> AnalyzeReading(SensorReading reading)
        {
            var alerts = new List<Alert>();

            if (reading.Temperature > 35)
            {
                alerts.Add(new Alert
                {
                    FarmId = reading.FarmId,
                    Type = "Temperatura Elevada",
                    Description = "Temperatura acima do limite configurado",
                    Severity = "Alta"
                });
            }

            if (reading.SoilHumidity < 25)
            {
                alerts.Add(new Alert
                {
                    FarmId= reading.FarmId,
                    Type = "Baixa umidade do solo",
                    Description = "A umidade do solo está abaixo do limite configurado.",
                    Severity = "Alta"
                });
            }

            return alerts;
        }
    }
}
