using AMSS.Models;

namespace AMSS.Repositories.IRepository
{
    public interface IFieldRepository : IRepository<Field>
    {
        Task<Field> Update(Field field);
    }
}
