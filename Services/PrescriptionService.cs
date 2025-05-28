using APBD_CW9.Data;
using APBD_CW9.DTOs;
using APBD_CW9.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_CW9.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly AppDbContext _context;

    public PrescriptionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddPrescriptionAsync(PrescriptionRequestDto request)
    {
        if (request.Medicaments.Count > 10)
            throw new Exception("Cannot add more than 10 medicaments");

        if (request.DueDate < request.Date)
            throw new Exception("DueDate cannot be earlier than Date");

        var doctor = await _context.Doctors.FindAsync(request.DoctorId);
        if (doctor == null)
            throw new Exception("Doctor not found");

        var patient = await _context.Patients
            .FirstOrDefaultAsync(p =>
                p.FirstName == request.PatientFirstName &&
                p.LastName == request.PatientLastName &&
                p.BirthDate == request.PatientBirthDate);

        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = request.PatientFirstName,
                LastName = request.PatientLastName,
                BirthDate = request.PatientBirthDate
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        var prescription = new Prescription
        {
            Date = request.Date,
            DueDate = request.DueDate,
            Doctor = doctor,
            Patient = patient,
            PrescriptionMedicaments = new List<PrescriptionMedicament>()
        };

        foreach (var med in request.Medicaments)
        {
            var medicament = await _context.Medicaments.FindAsync(med.MedicamentId);
            if (medicament == null)
                throw new Exception($"Medicament with ID {med.MedicamentId} not found");

            prescription.PrescriptionMedicaments.Add(new PrescriptionMedicament
            {
                Medicament = medicament,
                Dose = med.Dose,
                Details = med.Details
            });
        }

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();
    }

    public async Task<GetPatientDto> GetPatientWithPrescriptionsAsync(int idPatient)
    {
        var patient = await _context.Patients
            .Include(p => p.Prescriptions)
                .ThenInclude(pr => pr.PrescriptionMedicaments)
                    .ThenInclude(pm => pm.Medicament)
            .Include(p => p.Prescriptions)
                .ThenInclude(pr => pr.Doctor)
            .FirstOrDefaultAsync(p => p.Id == idPatient);

        if (patient == null)
            throw new Exception("Patient not found");

        return new GetPatientDto
        {
            IdPatient = patient.Id,
            FirstName = patient.FirstName,
            Prescriptions = patient.Prescriptions
                .OrderBy(p => p.DueDate)
                .Select(p => new GetPrescriptionDto
                {
                    IdPrescription = p.Id,
                    Date = p.Date,
                    DueDate = p.DueDate,
                    Doctor = new GetDoctorDto
                    {
                        IdDoctor = p.Doctor.Id,
                        FirstName = p.Doctor.FirstName
                    },
                    Medicaments = p.PrescriptionMedicaments.Select(pm => new GetMedicamentDto
                    {
                        IdMedicament = pm.Medicament.Id,
                        Name = pm.Medicament.Name,
                        Description = pm.Medicament.Description,
                        Dose = pm.Dose
                    }).ToList()
                }).ToList()
        };
    }
}
