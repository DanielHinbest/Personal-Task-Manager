using Microsoft.EntityFrameworkCore;
using Personal_Task_Manager.Data;
using Personal_Task_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Task = Personal_Task_Manager.Models.Task;

namespace Personal_Task_Manager.Services
{
    public class CategoryService
    {
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            using var context = new AppDbContext();
            return await context.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            using var context = new AppDbContext();
            return await context.Categories.FindAsync(id);
        }

        public async Task<Category?> GetCategoryByNameAsync(string name)
        {
            using var context = new AppDbContext();
            return await context.Categories.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<Category> AddCategoryAsync(string categoryName)
        {
            using var context = new AppDbContext();
            var category = new Category
            {
                Name = categoryName,
                CreatedAt = DateTime.Now,
                Tasks = new List<Task>()
            };
            context.Categories.Add(category);
            await context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            using var context = new AppDbContext();
            context.Categories.Update(category);
            await context.SaveChangesAsync();
            return category;
        }

        public async System.Threading.Tasks.Task DeleteCategoryAsync(int id)
        {
            using var context = new AppDbContext();
            var category = await context.Categories.FindAsync(id);
            if (category != null)
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
            }
        }
    }
}
