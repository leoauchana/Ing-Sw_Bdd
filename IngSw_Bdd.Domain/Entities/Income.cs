namespace IngSw_Bdd.Domain.Entities;

public class Income
{
    public Patient? Patient { get; set; }
    public Nurse? Nurse { get; set; }
    public double Temperature { get; set; }
    public string? Report {get; set;}
    public double FrequencyCardiac { get; set; }
    public double FrequencyRespiratory { get; set; }
    public StateIncome? StateIncome { get; set; }
    public EmergencyLevel? EmergencyLevel { get; set; }
}
