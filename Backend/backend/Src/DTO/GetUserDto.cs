namespace backend.Src.DTO
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string Fullname { get; set; } = string.Empty;
        public string Rut { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Birthday { get; set; }

    }
}