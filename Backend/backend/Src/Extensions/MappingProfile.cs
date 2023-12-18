using AutoMapper;
using backend.Src.DTO.Users;
using backend.Src.DTO.Repos;
using backend.Src.DTO.Commits;
using backend.Src.DTO;
using backend.Src.Models;


namespace backend.Src.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, CreateUserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<User, GetUserDto>();
            CreateMap<User, UpdateUserDto>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<Octokit.Repository, ReposDto>();
            CreateMap<Octokit.GitHubCommit, CommitDto>();
            CreateMap<Octokit.GitHubCommit, CommitDto>()
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Commit.Message))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Commit.Author.Name))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Commit.Author.Date))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.Author.Login))
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.Author.AvatarUrl));
        }
    }
}