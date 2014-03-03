using RepSuckCore.Ballots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepSuckCore
{
  public abstract class Motion
  {
    public abstract Ballot GetBallot();
  }
}
