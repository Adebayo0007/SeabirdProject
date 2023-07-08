namespace SeaBirdProject.Dtos.UserDto
{
    public class UpdateUserRequestModel
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string BranchType { get; set; }
    }
}
