using Microsoft.AspNetCore.Identity;

namespace StoryToVideo.Core.Entities;

public class ApplicationUser : IdentityUser
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    // Navigation property
    public virtual ICollection<Story> Stories { get; set; } = new List<Story>();
} 