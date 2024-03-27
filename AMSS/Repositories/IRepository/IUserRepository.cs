using AMSS.Models;
using AMSS.Models.Dto.User;
using AMSS.Repositories.IRepository;

namespace AMSS.Repository.IRepository
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser> Update(ApplicationUser user);
    }
}
