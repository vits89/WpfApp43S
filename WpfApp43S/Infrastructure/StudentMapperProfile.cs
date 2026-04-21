using AutoMapper;
using WpfApp43S.Models;
using WpfApp43S.ViewModels;

namespace WpfApp43S.Infrastructure;

public class StudentMapperProfile : Profile
{
    public StudentMapperProfile()
    {
        CreateMap<Student, StudentViewModel>()
            .ReverseMap();

        CreateMap<StudentViewModel, StudentViewModel>()
            .ForMember(dst => dst.Id, opts => opts.Ignore());
    }
}
