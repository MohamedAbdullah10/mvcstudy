﻿using DAL.Entities.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Persistance.Data.Configurations
{
    internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(d => d.Id).UseIdentityColumn(10, 10);
            builder.Property(d => d.Name).HasColumnType("varchar(20)").IsRequired();
            builder.Property(d => d.Code).HasColumnType("varchar(20)").IsRequired();
            builder.Property(d => d.CreationOn).HasDefaultValueSql("GETDATE()");
            builder.Property(d => d.LastModifiedOn).HasComputedColumnSql("GETDATE()");
        }
    }
}
