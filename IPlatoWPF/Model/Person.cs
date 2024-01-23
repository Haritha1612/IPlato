using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPlatoWPF.Model
{
    internal class Person
    {
        public int UniqueId { get; set; }
        public string LastName
        {
            get;
            set;
        }
        public string FirstName
        {
            get;
            set;
        }

        public DateTime? DateOfBirth { get; set; }

        public string Profession { get; set; }

    }
}
