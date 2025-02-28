using ICR_WEB_API.Service.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ICR_WEB_API.Service.BLL.Services
{
    public class ICRSurveyDBContext : DbContext
    {
        private readonly IConfiguration _configuration;


        public ICRSurveyDBContext(DbContextOptions<ICRSurveyDBContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<RatingScaleItem> RatingScaleItems { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnectionString");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}