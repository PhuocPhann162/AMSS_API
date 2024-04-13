using AMSS.Data;
using AMSS.Models;
using AMSS.Repositories.IRepository;

namespace AMSS.Repositories
{
    public class FieldRepository : Repository<Field>, IFieldRepository
    {
        private readonly ApplicationDbContext _db;
        public FieldRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Field> Update(Field field)
        {
            field.UpdatedAt = DateTime.Now;
            _db.Fields.Update(field);
            await _db.SaveChangesAsync();
            return null;
        }
    }
}
