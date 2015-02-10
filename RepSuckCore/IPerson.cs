using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepSuckCore
{
  public interface IPerson
  {
    string LastName { get; }
    string FirstLastName { get; }
    void AddPosition(Position p);
  }
}
