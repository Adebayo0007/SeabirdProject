namespace SeaBirdProject.Entities;
    public class Expenses
    {
       public string Id { get; set; } = Guid.NewGuid().ToString();
       public string BranchId { get; set; }
       public string Name { get; set; }
       public string? Description { get; set; }
       public decimal Amount { get; set; }
       public DateTime DateCreated { get; set; }
       public DateTime DateModified { get; set; }
       public Branch Branch { get; set; }
    }

