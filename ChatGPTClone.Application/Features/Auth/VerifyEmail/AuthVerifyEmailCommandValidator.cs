using ChatGPTClone.Application.Common.Interfaces;
using ChatGPTClone.Application.Common.Models.Identity;
using ChatGPTClone.Application.Features.Auth.Commands.VerifyEmail;
using FluentValidation;

namespace ChatGPTClone.Application.Features.Auth.VerifyEmail;

public class AuthVerifyEmailCommandValidator : AbstractValidator<AuthVerifyEmailCommand>
{
    private readonly IIdentityService _identityService;
    
    public AuthVerifyEmailCommandValidator(IIdentityService identityService)
    {
        _identityService = identityService;
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MustAsync(EmalExists)
            .WithMessage("Email is not found.");
        
        RuleFor(x => x.Token)
            .NotEmpty()
            .MinimumLength(20)
            .WithMessage("Token is invalid.");
    }
    
    private Task<bool> EmalExists(string email, CancellationToken cancellationToken)
    {
        return _identityService.CheckEmailExistsAsync(email, cancellationToken);
    }
    
    
}