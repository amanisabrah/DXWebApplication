﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DXWebApplication.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AccountingDbContext : DbContext
    {
        public AccountingDbContext()
            : base("name=AccountingDbContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ACC_EMP_Employee> ACC_EMP_Employee { get; set; }
        public virtual DbSet<HRS_EMC_EmpContract> HRS_EMC_EmpContract { get; set; }
        public virtual DbSet<HRS_SAL_Salaries> HRS_SAL_Salaries { get; set; }
        public virtual DbSet<JOB_JOBS> JOB_JOBS { get; set; }
        public virtual DbSet<WST_WorkStatus> WST_WorkStatus { get; set; }
    }
}
