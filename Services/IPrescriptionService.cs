using APBD_CW9.DTOs;

namespace APBD_CW9.Services;

public interface IPrescriptionService
{
    Task AddPrescriptionAsync(PrescriptionRequestDto request);
    Task<GetPatientDto> GetPatientWithPrescriptionsAsync(int idPatient);
}