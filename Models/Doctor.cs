namespace APBD_CW9.Models;

public class Doctor
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;

    public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
