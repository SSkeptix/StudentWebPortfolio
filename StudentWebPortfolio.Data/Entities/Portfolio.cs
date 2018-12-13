using StudentWebPortfolio.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentWebPortfolio.Data.Entities
{
    public class Portfolio
    {
        // Key, foreign key.
        public long UserId { get; set; }

        // Properties.
        public string GitHubUrl { get; set; }
        public string Group { get; set; }
        public English English { get; set; }
        public string Description { get; set; }

        // Navigation properties.
        public virtual User User { get; set; }
    }
}
