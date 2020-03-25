﻿using backend.data.Infrastructure;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend.Entity.backend.api.Data
{
    public class SystemIdentityDbContext : IdentityDbContext<SystemUser, SystemRole, int>
    {
        public SystemIdentityDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigurationEntity(typeof(SystemIdentityDbContext));
        }

    }
}