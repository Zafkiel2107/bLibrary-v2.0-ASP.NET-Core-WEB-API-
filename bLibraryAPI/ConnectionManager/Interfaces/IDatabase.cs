using bLibraryAPI.Models.BaseModel;

namespace bLibraryAPI.ConnectionManager.Interfaces
{
    internal interface IDatabase
    {
        IEnumerable<TEntity> GetElements<TEntity>() where TEntity : BaseEntity;
        TEntity GetElementById<TEntity>(Guid id) where TEntity : BaseEntity;
        void Create<TEntity>(TEntity item) where TEntity : BaseEntity;
        void Update<TEntity>(TEntity item) where TEntity : BaseEntity;
        void Delete<TEntity>(TEntity item) where TEntity : BaseEntity;
    }
}
