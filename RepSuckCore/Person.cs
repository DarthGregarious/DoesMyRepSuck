using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepSuckCore
{
  public class Person : IPerson
  {
    string FirstName { get; set; }
    public virtual string LastName { get; private set; }


    List<Position> positions;
    public IEnumerable<Position> Positions { get { return positions; } }

    public Person(string firstName, string lastName)
    {
      // TODO: Complete member initialization
      FirstName = firstName;
      LastName = lastName;
      positions = new List<Position>();
    }

    public virtual string FirstLastName
    {
      get
      {
        return String.Format("{0} {1}", FirstName, LastName);
      }
    }

    public virtual void AddPosition(Position position)
    {
      positions.Add(position);
    }
  }
}
