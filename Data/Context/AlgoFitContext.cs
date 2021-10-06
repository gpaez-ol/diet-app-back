using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using AlgoFit.Data.Configurations;
using AlgoFit.Data.Models;

namespace AlgoFit.Data.Context
{
    public class AlgoFitContext :DbContext
    {
        public readonly IServiceProvider ServiceProvider;
        private readonly IHttpContextAccessor httpContextAccessor;
        private IDbContextTransaction transaction;
        public AlgoFitContext(DbContextOptions<AlgoFitContext> opt, IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider) : base(opt)
        {
            this.httpContextAccessor = httpContextAccessor;
            ServiceProvider = serviceProvider;
            if (httpContextAccessor.HttpContext != null)
            {
                BeginTransaction();
            }
        }

        
        public DbSet<Biometric> Biometrics { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Diet> Diets { get; set; }
        public DbSet<DietCategory> DietCategories { get; set; }
        public DbSet<DietMeal> DietMeals { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserCredential> UserCredentials { get; set; }
        public DbSet<UserDiet> UserDiets { get; set; }
        
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BiometricConfiguration());
            modelBuilder.ApplyConfiguration(new DietCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new DietMealConfiguration());
            modelBuilder.ApplyConfiguration(new MealIngredientConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserDietConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        private void BeginTransaction()
        {
            transaction = Database.CurrentTransaction ?? Database.BeginTransaction();
        }

        private void CommitChanges()
        {
            try
            {
                transaction?.Commit();
            }
            catch (Exception)
            {
                transaction?.Rollback();
                throw new Exception("Error while saving all transactions to the database");
            }
        }

        public override int SaveChanges()
        {
            var numberOfEntries = base.SaveChanges();
            CommitChanges();
            return numberOfEntries;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            int result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }

    }
}