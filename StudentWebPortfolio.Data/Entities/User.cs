using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentWebPortfolio.Data.Entities
{
    public class User : IdentityUser<long>
    {
        // Properties.
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }

        // Foreign keys.
        public long? ValidatedByUserId { get; set; }

        // Navigation Properties.
        public virtual User ValidatedByUser { get; set; }
        public virtual Portfolio Portfolio { get; set; }
        public virtual ICollection<User> ValidatedUsers { get; set; }
        public virtual ICollection<Skill> ValidatedSkills { get; set; }
        public virtual ICollection<UserSkill> UserSkills { get; set; }
        public virtual ICollection<UserSkill> ValidatedUserSkills { get; set; }
    }
}
