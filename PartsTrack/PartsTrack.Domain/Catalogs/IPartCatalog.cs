using PartsTrack.Domain.Entities;

namespace PartsTrack.Domain.Catalogs
{
    public interface IPartCatalog
    {
        Part GetById(int partId);
        void Save(Part part);
        void Update(Part part);
        void Delete(int partId);
        IEnumerable<Part> GetAll();
    }
}
