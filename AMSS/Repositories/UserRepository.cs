using AMSS.Data;
using AMSS.Models;
using AMSS.Models.Dto.User;
using AMSS.Repository.IRepository;

namespace AMSS.Repositories
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ApplicationUser> Update(ApplicationUser user)
        {
            user.UpdatedAt = DateTime.Now;
            _db.ApplicationUsers.Update(user);
            await _db.SaveChangesAsync();
            return user;
        }
    }
}
