using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SnackisWebbAPI.Models
{
    public class SnackisUser : IdentityUser
    {
  [Required]
            public string ProfileImageUrl { get; set; }
    }
}
