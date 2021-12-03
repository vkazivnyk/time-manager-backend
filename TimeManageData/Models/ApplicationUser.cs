using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace TimeManageData.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<UserTask> Tasks { get; set; } = new ();

        public int ActiveTimeStartSeconds { get; set; }
        public int ActiveTimeEndSeconds { get; set; }
    }
}
