namespace SeaBirdProject.Entities;
    public class Branch
    {
       public string Id { get; set; } = Guid.NewGuid().ToString();
       public string BranchName { get; set; }   //The name of the Branch
       public string BranchType { get; set; }   //What they do e.g petrol station,engineering
       public string BranchAddress { get; set; }  //The address
       public Expenses Expenses { get; set; }
       public Sale Sale { get; set; }
       public User User { get; set; }

    }

