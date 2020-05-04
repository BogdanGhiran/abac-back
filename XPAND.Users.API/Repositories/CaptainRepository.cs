using System.Threading.Tasks;
using Common.GenericRepository;
using Microsoft.EntityFrameworkCore;
using XPAND.Captains.API.Models;

namespace XPAND.Captains.API.Repositories
{
    public class CaptainRepository: GenericRepository<Captain>, ICaptainRepository
    {
        public CaptainRepository(AbacTestCaptainsContext dbContext) : base(dbContext)
        {
            
        }
        public async Task<Captain> LoadByUsername(string userName)
        {
            return await _dbContext.Set<Captain>().FirstOrDefaultAsync(x => x.Username == userName);
        }

        public async Task<bool> CheckIfUsernameIsTaken(string userName)
        {
            return await _dbContext.Set<Captain>().AnyAsync(x => x.Username == userName);
        }
    }
}
