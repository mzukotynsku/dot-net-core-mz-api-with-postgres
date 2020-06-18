using System;
using System.Collections.Generic;
using System.Text;
using DotNetCoreMZ.API.Domain;
using DotNetCoreMZ.Data.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreMZ.API.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        public DbSet<TodoDTO> Todos { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

    }
}
