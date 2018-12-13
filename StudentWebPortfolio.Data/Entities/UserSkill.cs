using System;
using System.Collections.Generic;
using System.Text;

namespace StudentWebPortfolio.Data.Entities
{
    public class UserSkill
    {
        // Key.
        public long UserSkillId { get; set; }

        // Properties.
        public DateTime UpdatedOnUtc { get; set; }

        // Foreign keys.
        public long UserId { get; set; }
        public long SkillId { get; set; }
        public long? ValidatedByUserId { get; set; }

        // Navigation Properties.
        public virtual User User { get; set; }
        public virtual Skill Skill { get; set; }
        public virtual User ValidatedByUser { get; set; }
    }
}
