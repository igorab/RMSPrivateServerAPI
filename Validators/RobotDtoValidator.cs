using RMSPrivateServerAPI.DTOs;
using FluentValidation;
using RMSPrivateServerAPI.Enums;
#pragma warning disable CS1591
namespace RMSPrivateServerAPI.Validators;

public class RobotDtoValidator : AbstractValidator<RobotInfoDto>
{
    public RobotDtoValidator()
    {
        RuleFor(robot => robot.RobotState).
        Must(state => state == RobotState.online).
        WithSeverity(Severity.Warning).
        WithMessage("Robot must have value zero");
    }    
}
