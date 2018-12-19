using System;
using System.Collections.Generic;
using System.Text;

namespace StudentWebPortfolio.Data.Entities
{
    public class Skill : IUpdatableEntity
    {        
        // Key.
        public long SkillId { get; set; }

        // Properties.
        public string Name { get; set; }
        public DateTime UpdatedOnUtc { get; set; }

        // Foreign keys.
        public long? ValidatedByUserId { get; set; }

        // Navigation Properties.
        public virtual User ValidatedByUser { get; set; }
        public virtual ICollection<UserSkill> UserSkills { get; set; }
    }
}
