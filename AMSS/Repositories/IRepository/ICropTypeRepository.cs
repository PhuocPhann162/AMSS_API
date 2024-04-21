using AMSS.Models;

namespace AMSS.Repositories.IRepository
{
    public interface ICropTypeRepository : IRepository<CropType>
    {
        Task<CropType> Update(CropType cropType);
    }
}
