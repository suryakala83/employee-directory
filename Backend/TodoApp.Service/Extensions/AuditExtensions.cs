using TodoApp.Data.Entities;

namespace TodoApp.Service.Extensions
{
    public static class AuditExtensions
    {
        public static void SetAuditFieldsOnCreate(this Entity item, string userId)
        {
            item.CreatedBy = userId;
            item.CreatedAt = DateTime.UtcNow;
        }

        public static void SetAuditFieldsOnUpdate(this Entity item, string userId)
        {
            item.ModifiedBy = userId;
            item.ModifiedAt = DateTime.UtcNow;
        }
    }
}
