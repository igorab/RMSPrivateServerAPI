using RMSPrivateServerAPI.DTOs;
using FluentValidation;
#pragma warning disable CS1591

namespace RMSPrivateServerAPI.Validators;
public class RobotTaskDtoValidator : AbstractValidator<RobotTaskDto>
{
    public RobotTaskDtoValidator()
    {
        RuleFor(rt => rt.RobotId)
        .NotEmpty()
        .WithSeverity(Severity.Error)
        .WithMessage("RobotId must have value"); 
    }
}
