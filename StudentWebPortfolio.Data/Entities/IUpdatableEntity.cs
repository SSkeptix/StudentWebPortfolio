using System;
using System.Collections.Generic;
using System.Text;

namespace StudentWebPortfolio.Data.Entities
{
    public interface IUpdatableEntity
    {
        DateTime UpdatedOnUtc { get; set; }
    }
}
