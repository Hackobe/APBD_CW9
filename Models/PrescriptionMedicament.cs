namespace APBD_CW9.Models;

public class PrescriptionMedicament
{
    public int PrescriptionId { get; set; }
    public Prescription Prescription { get; set; } = null!;

    public int MedicamentId { get; set; }
    public Medicament Medicament { get; set; } = null!;

    public int Dose { get; set; }
    public string Details { get; set; } = null!;
}
