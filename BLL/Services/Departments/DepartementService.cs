using BLL.Dtos.DepartementsDto;
using DAL.Entities.Departments;
using DAL.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Departments
{
    public class DepartementService : IDepartmentService
    {
        private readonly IGenericRepository<Department> _deptrepo;

        public DepartementService(IGenericRepository<Department> deptrepo)
        {
            _deptrepo = deptrepo;
        }

        public IEnumerable<DepartmentToReturnDto> GetAll()
        {
            //var dept = _deptrepo.GetAll();
            //foreach (var d in dept)
            //{
            //   yield return new DepartmentToReturnDto
            //   {
            //       Id = d.Id,
            //       Name = d.Name,
            //       Code = d.Code,
            //       Description = d.Description,
            //       CreationDate = d.CreationDate
            //   };

            //}

            var dept = _deptrepo.GetAllQueryable().Select(x => new DepartmentToReturnDto
            {

                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                CreationDate = x.CreationDate
            }).AsNoTracking();

            return dept.ToList();
        }

        public DepartmentDetailsToReturnDto? Get(int Id)
        {
            var dept = _deptrepo.Get(Id);
            if (dept is { })
            {
                return new DepartmentDetailsToReturnDto
                {

                    Code = dept.Code,
                    CreationBy = dept.CreationBy,
                    CreationDate = dept.CreationDate,
                    CreationOn = dept.CreationOn,
                    Description = dept.Description,
                    Id = dept.Id,
                    IsDeleted = dept.IsDeleted,
                    LastModifiedBy = dept.LastModifiedBy,
                    LastModifiedOn = dept.LastModifiedOn,
                    Name = dept.Name
                };
            }

            return null;


        }
        public int CreateDepartment(CreateDepartmentDto createDepartmentDto)
        {
            var dept = new Department
            {
                Name = createDepartmentDto.Name,
                Code = createDepartmentDto.Code,
                Description = createDepartmentDto.Description,
                CreationDate = createDepartmentDto.CreationDate,
                CreationBy = 1,
                CreationOn = DateTime.Now
            };

            return _deptrepo.Add(dept);
        }

        public bool DeleteDepartment(int id)
        {
            var dept = _deptrepo.Get(id);
            if (dept == null)
                return false;

            return _deptrepo.Delete(dept) > 0;

        }



        public int UpdateDepartment(UpdateDepartmentDto updateDepartmentDto)
        {
            var dept = _deptrepo.Get(updateDepartmentDto.Id);
            if (dept == null)
                return 0;
            dept.Name = updateDepartmentDto.Name;
            dept.Code = updateDepartmentDto.Code;
            dept.Description = updateDepartmentDto.Description;
            dept.CreationDate = updateDepartmentDto.CreationDate;
            dept.LastModifiedBy = 1;
            dept.LastModifiedOn = DateTime.Now;
            dept.Id = updateDepartmentDto.Id;


            return _deptrepo.Update(dept);
        }



    }
}
