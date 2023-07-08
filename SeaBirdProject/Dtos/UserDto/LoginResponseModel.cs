namespace SeaBirdProject.Dtos.UserDto
{
    public class LoginResponseModel<T>
    {
        public string Token { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
    }
}
