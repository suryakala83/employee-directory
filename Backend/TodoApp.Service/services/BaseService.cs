using AutoMapper;
using TodoApp.Service.DTO;
using TodoApp.Service.Interfaces;

namespace TodoApp.Service.services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        public readonly RequestContext _requestContext;
        public readonly IMapper _mapper;

        public BaseService(RequestContext requestContext, IMapper mapper)
        {
            _requestContext = requestContext;
            _mapper = mapper;
        }
    }
}
