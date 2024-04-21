using AMSS.Data;
using AMSS.Models;
using AMSS.Repositories.IRepository;

namespace AMSS.Repositories
{
    public class CropTypeRepository : Repository<CropType>, ICropTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public CropTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<CropType> Update(CropType cropType)
        {
            cropType.UpdatedAt = DateTime.Now;
            _db.CropTypes.Update(cropType);
            await _db.SaveChangesAsync();
            return null;
        }
    }
}
