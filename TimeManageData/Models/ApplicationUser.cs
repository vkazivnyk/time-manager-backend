using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace TimeManageData.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<UserTask> Tasks { get; set; } = new ();

        public DateTime ActiveTimeStart { get; set; }
        public DateTime ActiveTimeEnd { get; set; }
    }
}
