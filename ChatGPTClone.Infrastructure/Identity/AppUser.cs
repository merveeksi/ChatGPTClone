using ChatGPTClone.Domain.Common;
using ChatGPTClone.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ChatGPTClone.Infrastructure.Identity;

public class AppUser:IdentityUser<Guid>, IEntity, ICreatedByEntity, IModifiedByEntity // guid kullanmanın sebebi id'nin unique olmasını sağlamak //mysql için guid kullanılmaz(problemli yani)
{ //AppUser bir yere bağımlı olmasın diye infrastructure katmanına koyduk
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public DateTimeOffset CreatedOn { get; set; }
    
    public string CreatedByUserId { get; set; }
    
    public DateTimeOffset? ModifiedOn { get; set; }
    
    public string? ModifiedByUserId { get; set; }
}