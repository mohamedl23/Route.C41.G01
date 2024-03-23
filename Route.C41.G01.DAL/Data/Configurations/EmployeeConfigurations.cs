﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.C41.G01.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G01.DAL.Data.Configurations
{
    internal class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(E => E.Address).IsRequired();
            builder.Property(E => E.Salary).HasColumnType("decimal(12,2)");
            builder.Property(E => E.gender)
                .HasConversion(

                (Gender) => Gender.ToString(),
                (genderAsString) => (Gender)Enum.Parse(typeof(Gender), genderAsString, true)
                );

            builder.Property(E => E.employeeType)
               .HasConversion(

               (EmployeeType) => EmployeeType.ToString(),
               (EmployeeTypeAsstring) => (EmpType)Enum.Parse(typeof(EmpType), EmployeeTypeAsstring, true)
               );
        }
    }
}
