namespace backend.Src.DTO.Commits
{
    public class CommitDto
    {
        public string? Message { get; set; }
        public string? Name { get; set; }

        public DateTimeOffset? Date { get; set; }
        public string? User { get; set; }

        public string? AvatarUrl { get; set; }
    }

}