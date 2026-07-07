using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace MiniLibrary.Models;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
}
