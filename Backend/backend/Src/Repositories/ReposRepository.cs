using Octokit;
using backend.Src.Repositories.Interfaces;

namespace backend.Src.Repositories
{
    public class ReposRepository : IReposRepository
    {

        public ReposRepository()
        {
        }


        public async Task<IReadOnlyList<Repository>?> GetAllRepositories(GitHubClient client)
        {
            var repos = await client.Repository.GetAllForUser("Dizkm8");
            repos = repos.OrderByDescending(x => x.UpdatedAt).ToList();
            return repos;
        }

        public async Task<IReadOnlyList<GitHubCommit>?> GetCommitsByRepositories(GitHubClient client, string repoName)
        {
            var commits = (await client.Repository.Commit.GetAll("Dizkm8", repoName)).ToList();
            return commits;
        }

        public async Task<int> GetCommitsCountByRepositories(GitHubClient client, string repoName)
        {
            var commits = (await client.Repository.Commit.GetAll("Dizkm8", repoName)).ToList();
            return commits.Count;
        }


    }
}