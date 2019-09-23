using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using database2.Models;
using database2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace database2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Book> Books { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        internal Task ServeChngesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
