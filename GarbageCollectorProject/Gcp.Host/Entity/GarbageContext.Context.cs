﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gcp.Host.Entity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GarbageCollectorsEntities : DbContext
    {
        public GarbageCollectorsEntities()
            : base("name=GarbageCollectorsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Araclar> Araclar { get; set; }
        public virtual DbSet<AraclarDetay> AraclarDetay { get; set; }
        public virtual DbSet<Egitim> Egitim { get; set; }
        public virtual DbSet<Kurumlar> Kurumlar { get; set; }
        public virtual DbSet<Marka> Marka { get; set; }
        public virtual DbSet<Model> Model { get; set; }
        public virtual DbSet<Personel> Personel { get; set; }
        public virtual DbSet<PersonelDetay> PersonelDetay { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Vardiya> Vardiya { get; set; }
        public virtual DbSet<Vergi> Vergi { get; set; }
    }
}