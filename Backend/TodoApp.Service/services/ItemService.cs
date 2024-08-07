using AutoMapper;
using System.Linq.Expressions;
using TodoApp.Data;
using TodoApp.Data.Entities;
using TodoApp.Data.Interfaces;
using TodoApp.Service.DTO;
using TodoApp.Service.Extensions;
using TodoApp.Service.Interfaces;

namespace TodoApp.Service.services
{
    public class ItemService : BaseService<Item>, IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository, IMapper mapper, RequestContext requestContext) : base(requestContext, mapper)
        {
            _itemRepository = itemRepository;
        }

        public async Task<Item> Add(TaskItem addItem)
        {
            var mappedItem = _mapper.Map<Item>(addItem);
            mappedItem.SetAuditFieldsOnCreate(_requestContext.User.Id);
            mappedItem.AssignedTo = _requestContext.User.Id;
            return await _itemRepository.Add(mappedItem);
        }

        public async Task<Item> Update(int id, TaskItem updateItem)
        {
            var mappedItem = _mapper.Map<Item>(updateItem);
            mappedItem.Id = id;
            mappedItem.SetAuditFieldsOnUpdate(_requestContext.User.Id);
            mappedItem.AssignedTo = _requestContext.User.Id;
            return await _itemRepository.Update(mappedItem, new List<string>() { "Id", "CreatedBy", "CreatedAt", "UserId" });
        }

        public async Task<Item> UpdateTaskCompleteStatus(int id, bool isCompleted)
        {
            var item = await _itemRepository.Get(id, _requestContext.User.Id);
            item.SetAuditFieldsOnUpdate(_requestContext.User.Id);
            item.IsCompleted = isCompleted;
            item.AssignedTo = _requestContext.User.Id;
            return await _itemRepository.UpdateFewProperties(item, new List<string>() { "IsCompleted", "ModifiedBy", "ModifiedAt" });
        }

        public async Task<bool> Delete(int id)
        {
            return await _itemRepository.Delete(id, _requestContext.User.Id);
        }

        public async Task<Item?> Get(int id)
        {
            return await _itemRepository.Get(id, _requestContext.User.Id);
        }

        public async Task<List<Item>> GetItems(TaskFilter filter)
        {
            return await _itemRepository.GetTasks(_requestContext.User.Id, filter);
        }

        public async Task<bool> DeleteAll()
        {
            Expression<Func<Item, bool>> expression = item =>
            item.AssignedTo == _requestContext.User.Id && (item.IsCompleted && item.ModifiedAt == DateTime.UtcNow.Date || item.DueDate >= DateTime.UtcNow.Date);
            return await _itemRepository.DeleteAll(expression);
        }
    }
}