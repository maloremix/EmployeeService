using AutoMapper;
using Employees.DAL.Models;
using Employees.DTO;

namespace Employees.Mappings
{
    /// <summary>
    /// Профиль отображения для AutoMapper, определяющий маппинг между сущностями и DTO.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="MappingProfile"/>.
        /// </summary>
        public MappingProfile()
        {
            // Создание отображения сотрудника на DTO и обратно
            CreateMap<EmployeeDTO, Employee>()
                .ForMember(dest => dest.Passport, opt => opt.MapFrom(src => src.Passport))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department));

            CreateMap<PassportDTO, Passport>();
            CreateMap<DepartmentDTO, Department>();

            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.Passport, opt => opt.MapFrom(src => src.Passport))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department));

            CreateMap<Passport, PassportDTO>();
            CreateMap<Department, DepartmentDTO>();

            // Отображение для обновления сотрудника
            CreateMap<EmployeeEditDTO, Employee>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => srcMember != null));
        }
    }
}
