using courseModels;

namespace course.Repository.Irepository
{
    public interface IComphanyRepository : IRepository<ComphanyPep>
    {
        void Update(ComphanyPep obj);
        void Save();

    }
}
