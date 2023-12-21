using gcsharpRPC.Models;
using gcsharpRPC.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gcsharpRPC.Services
{
    public class PollService
    {
        private readonly TrungContext dbContext;

        public PollService(TrungContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<Poll> GetPollAsync(int id)
        {
            return await dbContext.Polls
                .Where(poll => poll.Id == id)
                .SingleOrDefaultAsync();
        }

        public async Task<int> CreatePollAsync(Poll poll, DateTime[] pollDateOptions)
        {
            foreach (var date in pollDateOptions) {
                poll.Options.Add(
                    new PollOption { Date = date }
                );
            }

            await dbContext.Polls.AddAsync(poll);

            return await dbContext.SaveChangesAsync();
        }

        public void CreatePoll(Poll poll)
        {
            // foreach (var date in pollOptionDates) {
            //     poll.Options.Add(
            //         new PollOption { Date = date }
            //     );
            // }

            dbContext.Polls.Add(poll);

            dbContext.SaveChanges();
        }
    }
}