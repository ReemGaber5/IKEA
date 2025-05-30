﻿using IKEA.DAL.Common.Enums;
using IKEA.DAL.Models.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Data.Configurations.EmployeeConfigurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(E => E.Address).HasColumnType("varchar(100)");
            builder.Property(E => E.salary).HasColumnType("decimal(8,2)");


            builder.Property(E => E.Gender).HasConversion
                (
                  (gender) => gender.ToString(),
                  (gender) => (Gender)Enum.Parse(typeof(Gender), gender)
                );

            builder.Property(E => E.EmployeeType).HasConversion
                (
                  (employeetype) => employeetype.ToString(),
                  (employeetype) => (EmployeeType)Enum.Parse(typeof(EmployeeType), employeetype)
                );

            builder.Property(D => D.Createdon).HasDefaultValueSql("Getdate()");
            builder.Property(D => D.LastModifiedon).HasComputedColumnSql("Getdate()");


        }
    }
}
