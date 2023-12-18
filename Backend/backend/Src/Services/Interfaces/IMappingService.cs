using backend.Src.DTO;
using backend.Src.DTO.Commits;
using backend.Src.DTO.Repos;
using backend.Src.Models;

namespace backend.Src.Services.Interfaces
{
    public interface IMappingService
    {
        public User CreateClientDtoToUser(CreateUserDto createUserDto);

        public CreateUserDto MapUserToCreateUserDto(User user);

        public GetUserDto MapUserToGetUserDto(User user);

        public User UpdateUserDtoToUser(User user, UpdateUserDto updateUserDto);
        public UpdateUserDto CreateUpdateUserDto(User user);
        public ReposDto MapRepositoryToReposDto(Octokit.Repository repository);
        public CommitDto MapCommitToCommitDto(Octokit.GitHubCommit commit);

    }
}