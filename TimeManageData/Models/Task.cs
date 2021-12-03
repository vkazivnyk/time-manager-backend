using System;
using System.ComponentModel.DataAnnotations;
using TimeManageData.Enums;

namespace TimeManageData.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        [Required]
        public long TotalSeconds { get; set; }

        [Required]
        public Difficulty Difficulty { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
