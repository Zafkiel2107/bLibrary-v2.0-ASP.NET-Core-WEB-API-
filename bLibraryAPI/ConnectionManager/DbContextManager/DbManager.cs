using bLibraryAPI.ConnectionManager.Interfaces;
using bLibraryAPI.Models.BaseModel;
using bLibraryAPI.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace bLibraryAPI.ConnectionManager.DbContextManager
{
    internal class DbManager : IDatabase, IDisposable //TODO: разобраться с асинхронностью
    {
        private bLibraryDbContext Database { get; set; }
        private MemoryCache Cache { get; set; }
        private const int CachingTime = 20;
        public DbManager()
        {
            var options = new DbContextOptions<bLibraryDbContext>();
            this.Database = new bLibraryDbContext(options);
            this.Cache = new MemoryCache(new MemoryCacheOptions());
        }
        #region CRUD
        public void Create<TEntity>(TEntity item) where TEntity : BaseEntity
        {
            var entity = Database.Set<TEntity>();
            entity.Add(item);
            var IsSuccessAdded = Database.SaveChanges();
            if (IsSuccessAdded > 0)
            {
                Cache.Set(item.Id, item, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CachingTime)
                });
            }
        }
        public void Delete<TEntity>(TEntity item) where TEntity : BaseEntity
        {
            Database.Remove<TEntity>(item);
            var IsSuccessDeleted = Database.SaveChanges();
            if (IsSuccessDeleted > 0)
            {
                Cache.Remove(item.Id);
            }
        }
        public TEntity GetElementById<TEntity>(Guid id) where TEntity : BaseEntity
        {
            if (Cache.TryGetValue(id, out TEntity entity))
                return entity;
            else
                return Database.Set<TEntity>().Find(id);
        }
        public IEnumerable<TEntity> GetElements<TEntity>() where TEntity : BaseEntity
        {
            return Database.Set<TEntity>().ToList();
        }
        public void Update<TEntity>(TEntity item) where TEntity : BaseEntity
        {
            Database.Entry<TEntity>(item).State = EntityState.Modified;
            var IsSuccessUpdated = Database.SaveChanges();
            if (IsSuccessUpdated > 0)
            {
                Cache.Set(item.Id, item, new MemoryCacheEntryOptions //TODO: проверить, работает ли кеш на перезапись
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CachingTime)
                });
            }
        }
        #endregion
        #region Users
        public IEnumerable<User> GetUsers()
        {
            return Database.Users.ToList();
        }
        public User GetUserById(string id)
        {
            if (Cache.TryGetValue(id, out User entity))
                return entity;
            else
                return Database.Users.Find(id);
        }
        public void UpdateUser(User user)
        {
            Database.Entry<User>(user).State = EntityState.Modified;
            var IsSuccessUpdated = Database.SaveChanges();
            if (IsSuccessUpdated > 0)
            {
                Cache.Set(user.Id, user, new MemoryCacheEntryOptions //TODO: проверить, работает ли кеш на перезапись
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CachingTime)
                });
            }
        }
        #endregion
        public void Dispose() => Database.Dispose();
    }
}
