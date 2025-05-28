using APBD_CW9.DTOs;
using APBD_CW9.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_CW9.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionsController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] PrescriptionRequestDto request)
    {
        try
        {
            await _prescriptionService.AddPrescriptionAsync(request);
            return Ok("Prescription added successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}