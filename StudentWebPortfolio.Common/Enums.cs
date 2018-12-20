using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace StudentWebPortfolio.Common
{
    public static class UserRole
    {   
        public const string Admin = "Admin";
        public const string Teacher = "Teacher";
        public const string Employer = "Employer";
        public const string Student = "Student";

        public static readonly string[] UserRoles = { Student, Teacher, Employer };
        public static readonly string[] AllRoles = { Admin, Teacher, Employer, Student };
    }

    public enum English
    {
        [StringValue("Elementary")] A2 = 1,
        [StringValue("Intermediate")] B1 = 2,
        [StringValue("Upper intermediate")] B2 = 3,
        [StringValue("Advanced")] C1 = 4,
        [StringValue("Proficient")] C2 = 5,
    }
}
