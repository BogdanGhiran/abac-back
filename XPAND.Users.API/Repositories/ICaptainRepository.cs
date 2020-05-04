using System.Threading.Tasks;
using Common.GenericRepository;
using XPAND.Captains.API.Models;

namespace XPAND.Captains.API.Repositories
{
    public interface ICaptainRepository:IGenericRepository<Captain>
    {
        Task<Captain> LoadByUsername(string userName);
        Task<bool> CheckIfUsernameIsTaken(string userName);
    }
}
