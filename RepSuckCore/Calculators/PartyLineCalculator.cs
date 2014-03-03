using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepSuckCore.Calculators
{
  public class PartyLineCalculator : ISuckageCalculator
  {
    IAdvocate advocate, party;

    public PartyLineCalculator(IAdvocate advocate, IAdvocate party)
    {
      this.advocate = advocate;
      this.party = party;
    }
    public float Calculate()
    {
      throw new NotImplementedException();
    }
  }
}
