using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using server.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Database
{
    public class LolContext : DbContext
    {
        public LolContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Champion> Champions { get; set; }
        public DbSet<ChampionList> ChampionLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChampionListChampion>()
            .HasKey(t => new { t.ChampionId, t.ChampionListId });

            modelBuilder.Entity<ChampionListChampion>()
                .HasOne(pt => pt.Champion)
                .WithMany(p => p.ChampionListChampions)
                .HasForeignKey(pt => pt.ChampionId);

            modelBuilder.Entity<ChampionListChampion>()
                .HasOne(pt => pt.ChampionList)
                .WithMany(t => t.ChampionListChampions)
                .HasForeignKey(pt => pt.ChampionListId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLoggerFactory(loggerFactory)  //tie-up DbContext with LoggerFactory object
                .EnableSensitiveDataLogging()
                .UseNpgsql("server=127.0.0.1;port=5432;user id=test;password=test;database=lol_characters;pooling=true");
            }
        }

        //static LoggerFactory object
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });
    }
}
