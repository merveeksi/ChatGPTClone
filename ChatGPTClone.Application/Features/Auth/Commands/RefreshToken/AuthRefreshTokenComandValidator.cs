using ChatGPTClone.Application.Common.Interfaces;
using FluentValidation;

namespace ChatGPTClone.Application.Features.Auth.Commands.RefreshToken;

public class AuthRefreshTokenComandValidator : AbstractValidator<AuthRefreshTokenCommand>
{
    private readonly  IApplicationDbContext _context;
        
    public AuthRefreshTokenComandValidator()
    {
        RuleFor(x => x.AccessToken)
            .NotEmpty().WithMessage("Access token is required."); 

        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Refresh token is required.");
    }
    
    private Task<bool> IsRefreshTokenValid(string refreshToken, CancellationToken cancellationToken)
    {
        return _context.RefreshTokens.AnyAsync(x => x.Token == refreshToken && x.IsActive, cancellationToken);
    }
}