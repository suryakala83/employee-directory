using AutoMapper;
using TodoApp.Data.Entities;
using TodoApp.Service.DTO;

namespace TodoApp.Service.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Item, TaskItem>().ReverseMap();
        }
    }
}
