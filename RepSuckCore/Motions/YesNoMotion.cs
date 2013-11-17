using RepSuckCore.Ballots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepSuckCore
{
  public class YesNoMotion : Motion
  {
    public override Ballot GetBallot()
    {
      return (Ballot)new YesNoBallot();
    }
  }
}
