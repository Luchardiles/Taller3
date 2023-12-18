using AutoMapper;
using backend.Src.DTO;
using backend.Src.DTO.Commits;
using backend.Src.DTO.Repos;
using backend.Src.Models;
using backend.Src.Services.Interfaces;



namespace backend.Src.Services
{
    public class MappingService : IMappingService
    {
        private readonly IMapper _mapper;

        public MappingService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public User CreateClientDtoToUser(CreateUserDto createUserDto)
        {
            return _mapper.Map<User>(createUserDto);
        }

        public CreateUserDto MapUserToCreateUserDto(User user)
        {
            return _mapper.Map<CreateUserDto>(user);
        }

        public GetUserDto MapUserToGetUserDto(User user)
        {
            return _mapper.Map<GetUserDto>(user);
        }

        public User UpdateUserDtoToUser(User user, UpdateUserDto updateUserDto)
        {
            return _mapper.Map(updateUserDto, user);
        }

        public UpdateUserDto CreateUpdateUserDto(User user)
        {
            return _mapper.Map<UpdateUserDto>(user);
        }

        public ReposDto MapRepositoryToReposDto(Octokit.Repository repository)
        {
            return _mapper.Map<ReposDto>(repository);
        }

        public CommitDto MapCommitToCommitDto(Octokit.GitHubCommit commit)
        {
            return _mapper.Map<CommitDto>(commit);
        }
        

    }
}