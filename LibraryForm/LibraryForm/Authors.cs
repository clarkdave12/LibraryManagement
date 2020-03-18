using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryForm
{
    class Authors
    {
        public string lastName { get; set; }

        public string firstName { get; set; }

        public string middleName { get; set; }


        public string FullName()
        {
            string fullName = this.lastName + ", " + this.firstName + " " + this.middleName[0] + ".";
            return fullName;
        }
    }
}
