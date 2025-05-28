namespace APBD_CW9.DTOs;

public class PrescriptionRequestDto
{
    public string PatientFirstName { get; set; } = null!;
    public string PatientLastName { get; set; } = null!;
    public DateTime PatientBirthDate { get; set; }

    public int DoctorId { get; set; }

    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }

    public List<MedicamentDto> Medicaments { get; set; } = new();
}