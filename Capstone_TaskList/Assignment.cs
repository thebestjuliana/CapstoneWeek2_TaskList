using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone_TaskList
{
    class Assignment
    {
        public string TeamMember { get; set; }
        public string AssignmentDescription { get; set; }
        public DateTime DueDate { get; set; }
        public bool Complete { get; set; }

        public Assignment(string TeamMember, string AssignmentDescription, DateTime DueDate, bool Complete)
        {
            this.TeamMember = TeamMember;
            this.AssignmentDescription = AssignmentDescription;
            this.DueDate = DueDate;
            this.Complete = Complete;
        }
    }
}
