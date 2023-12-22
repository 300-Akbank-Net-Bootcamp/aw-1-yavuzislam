using Microsoft.AspNetCore.Mvc;
using VbApi.Validations;

namespace VbApi.Controllers;

public class Staff
{
    // [Required]
    // [StringLength(maximumLength: 250, MinimumLength = 10)]
    public string? Name { get; set; }

    // [EmailAddress(ErrorMessage = "Email address is not valid.")]
    public string? Email { get; set; }

    // [Phone(ErrorMessage = "Phone is not valid.")]
    public string? Phone { get; set; }

    // [Range(minimum: 30, maximum: 400, ErrorMessage = "Hourly salary does not fall within allowed range.")]
    public decimal? HourlySalary { get; set; }
}

[Route("api/[controller]")]
[ApiController]
public class StaffController : ControllerBase
{
    public StaffController()
    {
    }

    [HttpPost]
    public IActionResult Post([FromBody] Staff staff)
    {
        var validator = new StaffValidator();
        var validationResult = validator.Validate(staff);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
        }

        return Ok(staff);
    }
}