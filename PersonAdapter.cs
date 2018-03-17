using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_2
{
    static class PersonAdapter
    {
        internal static Person CreateUser(string firstName, string lastName, string email, DateTime dateOfBirth)
        {

            return new Person(firstName, lastName, email, dateOfBirth);
        }
    }
}



