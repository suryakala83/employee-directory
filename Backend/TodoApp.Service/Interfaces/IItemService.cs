using TodoApp.Data;
using TodoApp.Data.Entities;
using TodoApp.Service.DTO;

namespace TodoApp.Service.Interfaces
{
    public interface IItemService
    {
        Task<Item> Add(TaskItem addItem);
        Task<Item> Update(int id, TaskItem updateItem);
        Task<Item> UpdateTaskCompleteStatus(int id, bool isCompleted);
        Task<bool> Delete(int id);
        Task<Item?> Get(int id);
        Task<List<Item>> GetItems(TaskFilter filter);
        Task<bool> DeleteAll();
    }
}
