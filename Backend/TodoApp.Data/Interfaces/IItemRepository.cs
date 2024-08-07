using System.Linq.Expressions;
using TodoApp.Data.Entities;

namespace TodoApp.Data.Interfaces
{
    public interface IItemRepository
    {
        Task<Item> Add(Item item);
        Task<Item> Update(Item item);
        Task<Item> Update(Item item, List<string> ColumnsToBeIgnored);
        Task<Item> UpdateFewProperties(Item item, List<string> ColumnsToBeUpdated);
        Task<bool> Delete(int id, string userId);
        Task<Item?> Get(int? id, string userId);
        Task<List<Item>> GetTasks(string userId, TaskFilter filter);
        Task<bool> DeleteAll(Expression<Func<Item, bool>> expression);
    }
}
