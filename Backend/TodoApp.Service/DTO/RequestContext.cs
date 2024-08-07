namespace TodoApp.Service.DTO
{
    public class RequestContext
    {
        public User User { get; set; }

        public RequestContext(User user) {
            User = user;
        }
    }
}