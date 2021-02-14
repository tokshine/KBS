using KO.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace KO.Data
{
    public class KnowledgeDbContext : IdentityDbContext<KoUser>
    {
        public KnowledgeDbContext(DbContextOptions<KnowledgeDbContext> dbContext) : base(dbContext)
        {

        }
        public DbSet<FieldText> FieldText { get; set; }
    }

}
