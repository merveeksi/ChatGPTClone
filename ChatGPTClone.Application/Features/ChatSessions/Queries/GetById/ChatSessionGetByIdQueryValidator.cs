using ChatGPTClone.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ChatGPTClone.Application.Features.ChatSessions.Queries.GetById;

public class ChatSessionGetByIdQueryValidator:AbstractValidator<ChatSessionGetByIdQuery>
{
    private readonly IApplicationDbContext _dbcontext;
    
    public ChatSessionGetByIdQueryValidator(IApplicationDbContext dbcontext)
    {
        _dbcontext = dbcontext;
             RuleFor(p => p.Id)
            .NotEmpty()
            .NotNull()
            .MustAsync(BeValidIdAsync)
            .WithMessage("The selected Chat was not found.");
    }
    
    private Task<bool> BeValidIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _dbcontext
            .ChatSessions
            .AnyAsync(x => x.Id == id, cancellationToken);
    }
}