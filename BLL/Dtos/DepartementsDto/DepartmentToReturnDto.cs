﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos.DepartementsDto
{
    public class DepartmentToReturnDto
    {
        public int Id { get; set; }
        //public int CreationBy { get; set; }
        //public DateTime CreationOn { get; set; }
        //public int LastModifiedBy { get; set; }
        //public DateTime LastModifiedOn { get; set; }
        //public bool IsDeleted { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        //public string? Description { get; set; } = null!;
        public DateOnly CreationDate { get; set; }
    }
}
