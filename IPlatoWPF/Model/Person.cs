using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPlatoWPF.Model
{
    internal class Person
    {
        /// <summary>
        /// Property to hold the unique ID
        /// </summary>
        public int UniqueId
        {
            get; set;
        }

        /// <summary>
        /// Property to hold Person's last name
        /// </summary>
        public string LastName
        {
            get; set;
        }

        /// <summary>
        /// Property to hold Person's first name
        /// </summary>
        public string FirstName
        {
            get; set;
        }

        /// <summary>
        /// Property to hold Person's date of birth
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Property to hold Person's profession
        /// </summary>
        public string Profession { get; set; }

    }
}
