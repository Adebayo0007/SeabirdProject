﻿namespace SeaBirdProject.Entities; 
public class SuperAdmin 
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; }
    public User User { get; set; }
}
