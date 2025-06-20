using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Personal_Task_Manager.Models
{
    public class Task
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required int Priority { get; set; }
        public required int Status { get; set; }
        public required DateTime DueDate { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required DateTime UpdatedAt { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}
