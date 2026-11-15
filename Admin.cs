using System;

namespace ProjectManagement
{
    /// <summary>
    /// Admin class representing an administrator.
    /// Inherits common properties from Person.
    /// </summary>
    public class Admin : Person
    {
        /// <summary>
        /// Initializes a new instance of Admin.
        /// </summary>
        /// <param name="name">Full name.</param>
        /// <param name="username">Login username.</param>
        /// <param name="password">Login password.</param>
        public Admin(string name, string username, string password)
            : base(name, username, password)
        {
        }
    }
}
