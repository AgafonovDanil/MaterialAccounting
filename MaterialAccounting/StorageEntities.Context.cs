﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MaterialAccounting
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class StorageEntities : DbContext
    {
        private static StorageEntities _context;

        public static StorageEntities GetContext()
        {
            if (_context == null)
            {
                _context = new StorageEntities();
            }
            return _context;
        }
        public StorageEntities()
            : base("name=StorageEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Storage> Storage { get; set; }
        public virtual DbSet<StorageRecords> StorageRecords { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
