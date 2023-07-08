namespace SeaBirdProject.Entities;
    public class Staff
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public string AdminId { get; set; }
        public User User { get; set; }
        public Admin Admin { get; set; }
     }

