using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task = Personal_Task_Manager.Models.Task;

namespace Personal_Task_Manager.Models
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required List<Task> Tasks { get; set; }
    }
}
