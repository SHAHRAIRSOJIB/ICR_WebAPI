using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICR_WEB_API.Service.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ICR_WEB_API.Service.BLL.Services
{
    public class ICRSurveyDBContext: DbContext
    {
        private readonly IConfiguration _configuration;

        
        public ICRSurveyDBContext(DbContextOptions<ICRSurveyDBContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<QuestionCondition> QuestionConditions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerOption> AnswerOptions { get; set; }
        public DbSet<AnswerText> AnswerTexts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnectionString");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>().ToTable("questions");
            modelBuilder.Entity<Option>().ToTable("options");
            modelBuilder.Entity<QuestionCondition>().ToTable("question_conditions");
            modelBuilder.Entity<Answer>().ToTable("answers");
            modelBuilder.Entity<AnswerOption>().ToTable("answer_options");
            modelBuilder.Entity<AnswerText>().ToTable("answer_texts");
            
        }

    }
}
