#region old code
//using AutoMapper;
//using BLL.Dtos.DepartementsDto;
//using DAL.Entities.Departments;
//using DAL.Persistance.Repositories;
//using DAL.Persistance.UnitOfWork;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BLL.Services.Departments
//{
//    public class DepartementService : IDepartmentService
//    {
//        //private readonly IGenericRepository<Department> _deptrepo;
//        private readonly IUnitOfWork unitOfWork;
//        private readonly IGenericRepository<Department> _deptrepo;
//        private readonly IMapper _mapper;
//        public DepartementService(/*IGenericRepository<Department> deptrepo*/IUnitOfWork unitOfWork, IMapper mapper)
//        {
//            this.unitOfWork = unitOfWork;
//            _deptrepo = unitOfWork.Repository<Department>();
//            _mapper = mapper;
//            //_deptrepo = deptrepo;
//        }

//        //public   IEnumerable<DepartmentToReturnDto> GetAll()
//        //{
//        //    //var dept = _deptrepo.GetAll();
//        //    //foreach (var d in dept)
//        //    //{
//        //    //   yield return new DepartmentToReturnDto
//        //    //   {
//        //    //       Id = d.Id,
//        //    //       Name = d.Name,
//        //    //       Code = d.Code,
//        //    //       Description = d.Description,
//        //    //       CreationDate = d.CreationDate
//        //    //   };

//        //    //}
//        //    //var _deptrepo = unitOfWork.Repository<Department>();
//        //    var dept = _deptrepo.GetAllQueryable().Select(x => new DepartmentToReturnDto
//        //    {

//        //        Id = x.Id,
//        //        Name = x.Name,
//        //        Code = x.Code,
//        //        CreationDate = x.CreationDate
//        //    }).AsNoTracking();

//        //    return dept.ToList();
//        //}
//        public async Task<IEnumerable<DepartmentToReturnDto>> GetAllAsync()
//        {
//            var departmentsQuery = _deptrepo.GetAllQueryable().AsNoTracking();

//            // Use AutoMapper's ProjectTo for a very efficient query
//            return await _mapper.ProjectTo<DepartmentToReturnDto>(departmentsQuery).ToListAsync();
//        }
//        public async Task<DepartmentDetailsToReturnDto>? Get(int Id)
//        {
//            //var _deptrepo = unitOfWork.Repository<Department>();
//            var dept = await _deptrepo.Get(Id);
//            if (dept is { })
//            {
//                return new DepartmentDetailsToReturnDto
//                {

//                    Code = dept.Code,
//                    CreationBy = dept.CreationBy,
//                    CreationDate = dept.CreationDate,
//                    CreationOn = dept.CreationOn,
//                    Description = dept.Description,
//                    Id = dept.Id,
//                    IsDeleted = dept.IsDeleted,
//                    LastModifiedBy = dept.LastModifiedBy,
//                    LastModifiedOn = dept.LastModifiedOn,
//                    Name = dept.Name
//                };
//            }

//            return null;


//        }
//        public async Task<int> CreateDepartment(CreateDepartmentDto createDepartmentDto)
//        {
//            var dept = new Department
//            {
//                Name = createDepartmentDto.Name,
//                Code = createDepartmentDto.Code,
//                Description = createDepartmentDto.Description,
//                CreationDate = createDepartmentDto.CreationDate,
//                CreationBy = 1,
//                CreationOn = DateTime.Now
//            };

//            //unitOfWork.Repository<Department>().Add(dept);
//            _deptrepo.Add(dept);
//            return await unitOfWork.Complete();
//        }

//        public async Task<bool> DeleteDepartment(int id)
//        {
//            //var _deptrepo = unitOfWork.Repository<Department>();
//            var dept =await _deptrepo.Get(id);
//            if (dept == null)
//                return false;

//            _deptrepo.Delete(dept);
//            return await unitOfWork.Complete() > 0;

//        }



//        public async Task<int> UpdateDepartment(UpdateDepartmentDto updateDepartmentDto)
//        {
//            //var _deptrepo = unitOfWork.Repository<Department>();
//            var dept = await _deptrepo.Get(updateDepartmentDto.Id);
//            if (dept == null)
//                return 0;
//            dept.Name = updateDepartmentDto.Name;
//            dept.Code = updateDepartmentDto.Code;
//            dept.Description = updateDepartmentDto.Description;
//            dept.CreationDate = updateDepartmentDto.CreationDate;
//            dept.LastModifiedBy = 1;
//            dept.LastModifiedOn = DateTime.Now;
//            //dept.Id = updateDepartmentDto.Id;


//             _deptrepo.Update(dept);
//            return await unitOfWork.Complete();
//        }



//    }
//}
#endregion
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BLL.Dtos.DepartementsDto;
using DAL.Entities.Departments;
using DAL.Persistance.Repositories;
using DAL.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services.Departments
{
    public class DepartementService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Department> _deptRepo;

        public DepartementService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _deptRepo = _unitOfWork.Repository<Department>();
        }

        public async Task<IEnumerable<DepartmentToReturnDto>> GetAllAsync()
        {
            var query = _deptRepo.GetAllQueryable().AsNoTracking();
            return await _mapper.ProjectTo<DepartmentToReturnDto>(query).ToListAsync();
        }

        public async Task<DepartmentDetailsToReturnDto?> GetAsync(int Id)
        {
            var dept = await _deptRepo.Get(Id);
            return _mapper.Map<DepartmentDetailsToReturnDto>(dept);
        }

        public async Task<int> CreateDepartmentAsync(CreateDepartmentDto createDepartmentDto)
        {
            var dept = _mapper.Map<Department>(createDepartmentDto);
            _deptRepo.Add(dept);
            return await _unitOfWork.CompleteAsync(); // Use CompleteAsync for convention
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var dept = await _deptRepo.Get(id);
            if (dept == null)
                return false;

            _deptRepo.Delete(dept);
            return await _unitOfWork.CompleteAsync() > 0; // Use CompleteAsync
        }

        public async Task<int> UpdateDepartmentAsync(UpdateDepartmentDto updateDepartmentDto)
        {
            var dept = await _deptRepo.Get(updateDepartmentDto.Id);
            if (dept == null)
                return 0;

            _mapper.Map(updateDepartmentDto, dept);
            _deptRepo.Update(dept);
            return await _unitOfWork.CompleteAsync(); // Use CompleteAsync
        }
    }
}