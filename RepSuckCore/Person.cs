using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepSuckCore
{
  public class Person
  {
    string FirstName { get; set; }
    string LastName { get; set; }

    public Person(string firstName, string lastName)
    {
      // TODO: Complete member initialization
      FirstName = firstName;
      LastName = lastName;
    }

    public string FirstLastName
    {
      get
      {
        return String.Format("{0} {1}", FirstName, LastName);
      }
    }
  }
}
