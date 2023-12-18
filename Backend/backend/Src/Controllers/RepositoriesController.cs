using Microsoft.AspNetCore.Mvc;
using Octokit;
using backend.Src.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace backend.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RepositoriesController : ControllerBase
    {
        private readonly IReposService _reposService;

        public RepositoriesController(IReposService reposService)
        {
            _reposService = reposService ?? throw new ArgumentNullException(nameof(reposService));
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<GitHubCommit>?>> GetRepos()
        {
            var repos = await _reposService.GetAllRepositories();
            return Ok(repos);
        }
        [Authorize]
        [HttpGet("commits/{repoName}")]
        public async Task<ActionResult<IReadOnlyList<GitHubCommit>?>> GetCommitsByRepo(string repoName)
        {
            var commits = await _reposService.GetCommitsByRepositories(repoName);
            return Ok(commits);
        }

    }
}
