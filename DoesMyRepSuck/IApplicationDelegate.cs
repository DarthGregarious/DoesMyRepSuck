using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepSuckCLI
{
  public interface IApplicationDelegate
  {
    string GetPrompt();
    void HandleInput(string input);
  }
}
