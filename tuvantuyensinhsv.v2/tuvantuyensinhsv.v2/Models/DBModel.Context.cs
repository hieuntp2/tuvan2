﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace tuvantuyensinhsv.v2.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ProjectHEntities : DbContext
    {
        public ProjectHEntities()
            : base("name=ProjectHEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<BaiViet> BaiViets { get; set; }
        public virtual DbSet<InternalMessage> InternalMessages { get; set; }
        public virtual DbSet<KhoiThi> KhoiThis { get; set; }
        public virtual DbSet<MBTI> MBTIs { get; set; }
        public virtual DbSet<Nganh> Nganhs { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<RatePost> RatePosts { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<SystemInformation> SystemInformations { get; set; }
        public virtual DbSet<SystemLog> SystemLogs { get; set; }
        public virtual DbSet<ThanhPho> ThanhPhoes { get; set; }
        public virtual DbSet<TruongNganh> TruongNganhs { get; set; }
        public virtual DbSet<Truong> Truongs { get; set; }
    }
}
