﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BYSProje.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ProjeErdoEntities1 : DbContext
    {
        public ProjeErdoEntities1()
            : base("name=ProjeErdoEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AkademisyenTablosu> AkademisyenTablosu { get; set; }
        public virtual DbSet<Dersler> Dersler { get; set; }
        public virtual DbSet<OgrenciDersSecimTablosu> OgrenciDersSecimTablosu { get; set; }
        public virtual DbSet<Ogrenciler> Ogrenciler { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
