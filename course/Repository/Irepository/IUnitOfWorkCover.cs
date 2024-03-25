namespace course.Repository.Irepository
{
    public interface IUnitOfWorkCover
    {
   
        ICoverRepository CoverType { get; }

        void Save();
    }
}
