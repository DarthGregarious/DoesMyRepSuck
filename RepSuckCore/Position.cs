using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepSuckCore
{
  public class Position
  {
    public Issue Issue { get; private set; }
    public string OptionChosen { get; private set; }

    public Position(Issue i, string option)
    {
      Issue = i;
      OptionChosen = option;
    }
  }
}
