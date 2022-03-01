using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SupBlog.Data.Models.Base;
using SupBlog.Data.Repositories.Base;

namespace SupBlog.Data.Repositories
{
    public class DbRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly ApplicationDbContext _db;

        public DbRepository(ApplicationDbContext db)
        {
            _db = db;
            Set = _db.Set<T>();
        }

        protected DbSet<T> Set { get; }
        protected virtual IQueryable<T> Items => Set;

        public async Task<bool> ExistId(int id)
        {
            return await Set.AnyAsync(item => item.Id == id).ConfigureAwait(false);
        }

        public async Task<bool> Exist(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            return await Set.AnyAsync(i => i.Id == item.Id).ConfigureAwait(false);
        }

        public async Task<int> GetCount()
        {
            return await Items.CountAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Items.ToArrayAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> Get(int skip, int count)
        {
            return await Items
                .Skip(skip)
                .Take(count)
                .ToArrayAsync()
                .ConfigureAwait(false);
        }

        public async Task<IPage<T>> GetPage(int pageIndex, int pageSize)
        {
            if (pageSize <= 0) return new Page(Enumerable.Empty<T>(), pageSize, pageIndex, pageSize);

            var query = Items;
            var total_count = await query.CountAsync().ConfigureAwait(false);

            if (total_count == 0)
                return new Page(Enumerable.Empty<T>(), 0, pageIndex, pageSize);

            if (pageIndex > 0)
                query = query
                    .Skip(pageIndex * pageSize);
            query = query.Take(pageSize);

            var items = await query.ToArrayAsync().ConfigureAwait(false);

            return new Page(items, total_count, pageIndex, pageSize);
        }

        public async Task<T> GetById(int id)
        {
            return await Items.SingleOrDefaultAsync(item => item.Id == id).ConfigureAwait(false);
        }

        public async Task<T> Add(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            //_db.Entry(item).State = EntityState.Added;

            await _db.AddAsync(item).ConfigureAwait(false);
            await _db.SaveChangesAsync().ConfigureAwait(false);

            return item;
        }

        public async Task<T> Update(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            // _db.Entry(item).State = EntityState.Modified;
            // Set.Update(item);

            _db.Update(item);
            await _db.SaveChangesAsync().ConfigureAwait(false);

            return item;
        }

        public async Task<T> Delete(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            if (!await ExistId(item.Id))
                return null;

            _db.Remove(item);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return item;
        }

        public async Task<T> DeleteById(int id)
        {
            var item = Set.Local.FirstOrDefault(i => i.Id == id);
            if (item is null)
                item = await Set
                    .Select(i => new T { Id = i.Id })
                    .FirstOrDefaultAsync(i => i.Id == id)
                    .ConfigureAwait(false);
            if (item is null)
                return null;

            return await Delete(item).ConfigureAwait(false);
        }

        protected record Page(IEnumerable<T> Items, int TotalCount, int PageIndex, int PageSize) : IPage<T>;
    }
}