using System.Security.Claims;
using TodoApp.Service.DTO;

namespace TodoApp.Infrastructure
{
    public class RequestContextBuilder
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RequestContextBuilder(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public RequestContext Build()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var userId = httpContext!.User.FindFirstValue("UserId");
            if (httpContext.User.Identity.IsAuthenticated)
            {
                return new RequestContext(
                    new User
                    {
                        Id = userId,
                    }
                    );
            }
            else
            {
                return new RequestContext(new User {});
            }
        }
    }
}
