﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Сerts
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CertsUsersEntities : DbContext
    {
        public CertsUsersEntities()
            : base("name=CertsUsersEntities")
        {
            this.Database.Connection.ConnectionString = @"Data Source=prm-tr01;Initial Catalog=CertsUsers;Persist Security Info=True;User ID=CertsUsersAdmin;Password=AdminOfCertsUsers";
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CertsUser> CertsUsers { get; set; }
    }
}