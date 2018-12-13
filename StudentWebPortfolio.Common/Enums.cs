using System;
using System.Collections.Generic;
using System.Text;

namespace StudentWebPortfolio.Common
{
    public enum English
    {
        [StringValue("Elementary")] A2 = 1,
        [StringValue("Intermediate")] B1 = 2,
        [StringValue("Upper intermediate")] B2 = 3,
        [StringValue("Advanced")] C1 = 4,
        [StringValue("Proficient")] C2 = 5,
    }
}
