using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepSuckCore;

namespace RepSuckCLI
{
  public interface IApplication
  {
    void AddPerson(Person p);
    void DelegateComplete();
  }
}
