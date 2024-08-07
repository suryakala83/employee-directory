using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TodoApp.Data.Entities;
using TodoApp.Data.Interfaces;

namespace TodoApp.Data.Repositories
{
    public class ItemRepository(TodoAppDb contextdb) : IItemRepository
    {
        private readonly TodoAppDb _contextdb = contextdb;

        public async Task<Item> Add(Item item)
        {
            var entity = await _contextdb.AddAsync(item);
            await _contextdb.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<Item> Update(Item item)
        {
            var entity = _contextdb.Item.Update(item);
            await _contextdb.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<Item> Update(Item item, List<string> ColumnsToBeIgnored)
        {
            var entity = _contextdb.Item.Attach(item);
            var entry = _contextdb.Entry(item);
            entry.State = EntityState.Modified;
            foreach (var column in ColumnsToBeIgnored)
            {
                entry.Property(column).IsModified = false;
            }
            await _contextdb.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<Item> UpdateFewProperties(Item item, List<string> ColumnsToBeUpdated)
        {
            var record = _contextdb.Item.Attach(item);
            var entry = _contextdb.Entry(item);
            entry.State = EntityState.Modified;
            foreach (var column in ColumnsToBeUpdated)
            {
                entry.Property(column).IsModified = true;
            }
            await _contextdb.SaveChangesAsync();
            return record.Entity;
        }

        public async Task<bool> Delete(int id, string userId)
        {
            await _contextdb.Item.Where(t => t.Id == id && t.AssignedTo == userId).ExecuteDeleteAsync();
            return await _contextdb.SaveChangesAsync() > 0;
        }

        public async Task<Item?> Get(int? id, string userId)
        {
            return await _contextdb.Item.SingleOrDefaultAsync(i => i.Id == id && i.AssignedTo == userId);
        }

        public async Task<List<Item>> GetTasks(string userId, TaskFilter filter)
        {
            var query = _contextdb.Item.AsQueryable();

            switch (filter)
            {
                case TaskFilter.Completed:
                    query = query.Where(i => i.AssignedTo == userId && i.IsCompleted).OrderByDescending(i => i.ModifiedAt);
                    break;
                case TaskFilter.TodayCompleted:
                    query = query.Where(i => i.AssignedTo == userId && i.IsCompleted && i.ModifiedAt == DateTime.UtcNow.Date);
                    break;
                case TaskFilter.Pending:
                    query = query.Where(i => i.AssignedTo == userId && !i.IsCompleted && i.DueDate >= DateTime.UtcNow.Date);
                    break;
                case TaskFilter.TodayTasks:
                    query = query.Where(i => i.AssignedTo == userId && ((i.IsCompleted && i.ModifiedAt == DateTime.UtcNow.Date) || (i.DueDate >= DateTime.UtcNow.Date))).OrderByDescending(i => i.IsCompleted == false).ThenBy(i => i.DueDate);
                    break;
                case TaskFilter.All:
                default:
                    query = query.Where(i => i.AssignedTo == userId).OrderBy(i => i.DueDate).ThenBy(i => i.ModifiedAt);
                    break;
            }
            return await query.ToListAsync();
        }

        public async Task<bool> DeleteAll(Expression<Func<Item, bool>> expression)
        {
            var items = _contextdb.Item.Where(expression);
            _contextdb.Item.RemoveRange(items);
            return await _contextdb.SaveChangesAsync() > 0;
        }
    }
}
