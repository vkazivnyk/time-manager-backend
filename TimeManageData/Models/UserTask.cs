using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeManageData.Models
{
    public class UserTask
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        [Required]
        public int Importance { get; set; }

        [Required]
        public int Difficulty { get; set; }

        public ApplicationUser User { get; set; }

        [NotMapped]
        public double TimeEvaluation { get; set; }
        
        [NotMapped]
        public double PriorityEvaluation { get; set; }

        [NotMapped]
        public int DeadlineMissEvaluation { get; set; }
    }
}
