﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class U07lyXEntities : DbContext
    {
        public U07lyXEntities()
            : base("name=U07lyXEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<address> addresses { get; set; }
        public virtual DbSet<appointment> appointments { get; set; }
        public virtual DbSet<city> cities { get; set; }
        public virtual DbSet<country> countries { get; set; }
        public virtual DbSet<customer> customers { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<PET> PETs { get; set; }
    
        public virtual int clean_database()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("clean_database");
        }
    
        public virtual int unit_test_1()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("unit_test_1");
        }
    }
}
