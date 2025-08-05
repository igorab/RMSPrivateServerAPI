using RMSPrivateServerAPI.DTOs;
using FluentValidation;
#pragma warning disable CS1591
namespace RMSPrivateServerAPI.Validators;

public class RobotDtoValidator : AbstractValidator<RobotInfoDto>
{
    public RobotDtoValidator()
    {
        RuleFor(robot => robot.Is_Deleted).Must(is_deleted => is_deleted == false).WithSeverity(Severity.Warning).WithMessage("Robot must have value zero");
    }    
}
