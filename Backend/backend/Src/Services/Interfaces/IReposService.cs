using backend.Src.DTO.Commits;
using backend.Src.DTO.Repos;
namespace backend.Src.Services.Interfaces
{
    public interface IReposService
    {
        public Task<IReadOnlyList<ReposDto>?> GetAllRepositories();
        public Task<IReadOnlyList<CommitDto>?> GetCommitsByRepositories(string repoName);
    }
}