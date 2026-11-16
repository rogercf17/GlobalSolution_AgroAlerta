using AgroAlerta.Models;
using AgroAlerta.Services;

namespace AgroAlerta.Tests;

public class UnitTest1
{
    private readonly AlertService _alertService;

    public UnitTest1()
    {
        _alertService = new AlertService();
    }

    [Fact]
    public void AnalyzeReading_TemperaturaElevada_DeveGerarAlertaDeTemperatura()
    {
        // Arrange
        var reading = new SensorReading
        {
            FarmId = 1,
            Temperature = 36.0,   // Acima do limite de 35
            AirHumidity = 60.0,   // Normal
            SoilHumidity = 50.0   // Normal
        };

        // Act
        var alerts = _alertService.AnalyzeReading(reading);

        // Assert
        Assert.Single(alerts);
        Assert.Equal("Temperatura Elevada", alerts[0].Type);
        Assert.Equal("Temperatura acima do limite configurado", alerts[0].Description);
        Assert.Equal("Alta", alerts[0].Severity);
        Assert.Equal(1, alerts[0].FarmId);
    }

    [Fact]
    public void AnalyzeReading_UmidadeSoloBaixa_DeveGerarAlertaDeUmidade()
    {
        // Arrange
        var reading = new SensorReading
        {
            FarmId = 1,
            Temperature = 25.0,   // Normal
            AirHumidity = 60.0,   // Normal
            SoilHumidity = 18.0   // Abaixo do limite de 25
        };

        // Act
        var alerts = _alertService.AnalyzeReading(reading);

        // Assert
        Assert.Single(alerts);
        Assert.Equal("Baixa umidade do solo", alerts[0].Type);
        Assert.Equal("A umidade do solo está abaixo do limite configurado.", alerts[0].Description);
        Assert.Equal("Alta", alerts[0].Severity);
        Assert.Equal(1, alerts[0].FarmId);
    }

    [Fact]
    public void AnalyzeReading_CondicoesNormais_NaoDeveGerarAlertas()
    {
        // Arrange
        var reading = new SensorReading
        {
            FarmId = 1,
            Temperature = 25.0,   // Normal
            AirHumidity = 60.0,   // Normal
            SoilHumidity = 60.0   // Normal
        };

        // Act
        var alerts = _alertService.AnalyzeReading(reading);

        // Assert
        Assert.Empty(alerts);
    }
}
