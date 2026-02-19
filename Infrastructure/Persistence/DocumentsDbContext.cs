using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class DocumentsDbContext : DbContext
    {

      public DocumentsDbContext(DbContextOptions<DocumentsDbContext> options) : base(options)
        {
        }

        public DbSet<DocumentAsset> documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DocumentAsset>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Filename).IsRequired();

                entity.Property(x => x.ContentType).IsRequired();
                entity.Property(x => x.DocumentType).IsRequired();
                entity.Property(x => x.Channel).IsRequired();
                entity.Property(x => x.CustomerId);
                entity.Property(x => x.Status).IsRequired();
                entity.Property(x => x.Size).IsRequired();
                entity.Property(x => x.UploadDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(x => x.CorrelationId);
                entity.Property(x => x.Url).IsRequired();



            });


        }


    }
}
