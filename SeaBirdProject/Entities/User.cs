using System.Net;
using System;

namespace SeaBirdProject.Entities; 
public class User 
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? BranchId { get; set; }
    public string UserName { get; set; }
    public string? ProfilePicture { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string Gender { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public bool IsActive { get; set; }
    public bool IsRegistered { get; set; }
    public Branch Branch { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateModified { get; set; }
    public SuperAdmin SuperAdmin { get; set; }
    public Admin Admin { get; set; }
    public Staff Staff { get; set; }
}
