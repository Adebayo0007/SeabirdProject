namespace SeaBirdProject.Entities;
    public class Admin
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<Staff> Staffs { get; set; } = new HashSet<Staff>();
}

