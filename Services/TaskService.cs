using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personal_Task_Manager.Data;
using Microsoft.EntityFrameworkCore;

namespace Personal_Task_Manager.Services
{
    public class TaskService
    {
        public async System.Threading.Tasks.Task<List<Personal_Task_Manager.Models.Task>> GetAllTasksAsync()
        {
            using var context = new AppDbContext();
            return await context.Tasks.ToListAsync();
        }

        public async System.Threading.Tasks.Task<Personal_Task_Manager.Models.Task?> GetTaskByIdAsync(int id)
        {
            using var context = new AppDbContext();
            return await context.Tasks.FindAsync(id);
        }

        public async System.Threading.Tasks.Task<Personal_Task_Manager.Models.Task> AddTaskAsync(Personal_Task_Manager.Models.Task task)
        {
            using var context = new AppDbContext();
            context.Tasks.Add(task);
            await context.SaveChangesAsync();
            return task;
        }

        public async System.Threading.Tasks.Task<Personal_Task_Manager.Models.Task> UpdateTaskAsync(Personal_Task_Manager.Models.Task task)
        {
            using var context = new AppDbContext();
            context.Tasks.Update(task);
            await context.SaveChangesAsync();
            return task;
        }

        public async System.Threading.Tasks.Task DeleteTaskAsync(int id)
        {
            using var context = new AppDbContext();
            var user = await context.Tasks.FindAsync(id);
            if (user != null)
            {
                context.Tasks.Remove(user);
                await context.SaveChangesAsync();
            }
        }

        public async System.Threading.Tasks.Task<List<Personal_Task_Manager.Models.Task>> SearchTaskAsync(string str)
        {
            using var context = new AppDbContext();

            return await context.Tasks.Where(t => t.Title.Contains(str) || t.Description.Contains(str)).ToListAsync();
        }
    }
}
