using backend.Src.Repositories.Interfaces;
using backend.Src.Services.Interfaces;
using DotNetEnv;
using Octokit;
using backend.Src.DTO.Repos;
using backend.Src.DTO.Commits;


namespace backend.Src.Services
{
    public class ReposService : IReposService
    {
        private readonly string _githubToken;
        private readonly IReposRepository _reposRepository;
        private readonly IMappingService _mappingService;
        private readonly GitHubClient _client;
        public ReposService(IReposRepository reposRepository, IMappingService mappingService)
        {
            Env.Load();
            _githubToken = Env.GetString("GITHUB_TOKEN") ?? throw new ArgumentNullException("GITHUB_TOKEN is null");
            _reposRepository = reposRepository ?? throw new ArgumentNullException(nameof(reposRepository));
            _mappingService = mappingService ?? throw new ArgumentNullException(nameof(mappingService));
            _client = ClientProvider();
        }

        private GitHubClient ClientProvider()
        {
            var client = new GitHubClient(new ProductHeaderValue("MobileHub"));
            var tokenCred = new Credentials(_githubToken);
            client.Credentials = tokenCred;
            return client;
        }

        public async Task<IReadOnlyList<ReposDto>?> GetAllRepositories()
        {
            var repos = await _reposRepository.GetAllRepositories(_client);
            if (repos == null) return null;
            repos = repos.OrderByDescending(x => x.UpdatedAt).ToList();
            var reposDto = new List<ReposDto>();

            var getCommitsTasks = repos.Select(repo =>
            {
                if (repo.Size == 0)
                {
                    return _reposRepository.GetCommitsCountByRepositories(_client, "");
                }
                return _reposRepository.GetCommitsCountByRepositories(_client, repo.Name);
            }).ToList();

            var commitsResults = await Task.WhenAll(getCommitsTasks);

            for (int i = 0; i < repos.Count; i++)
            {
                var repoDto = _mappingService.MapRepositoryToReposDto(repos[i]);
                repoDto.CommitCount = commitsResults[i];
                reposDto.Add(repoDto);
            }

            return reposDto;
        }

        public async Task<IReadOnlyList<CommitDto>?> GetCommitsByRepositories(string repoName)
        {
            var commits = await _reposRepository.GetCommitsByRepositories(_client, repoName);
            if (commits == null) return null;
            commits = commits.OrderByDescending(x => x.Commit.Committer.Date).ToList();
            var commitsDto = new List<CommitDto>();
            foreach (var commit in commits)
            {
                var commitDto = _mappingService.MapCommitToCommitDto(commit);
                commitsDto.Add(commitDto);
            }
            return commitsDto;
        }

    }
}