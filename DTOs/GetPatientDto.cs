﻿namespace APBD_CW9.DTOs;

public class GetPatientDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public List<GetPrescriptionDto> Prescriptions { get; set; }
}

public class GetPrescriptionDto
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<GetMedicamentDto> Medicaments { get; set; }
    public GetDoctorDto Doctor { get; set; }
}

public class GetMedicamentDto
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public int? Dose { get; set; }
    public string Description { get; set; }
}

public class GetDoctorDto
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; }
}
