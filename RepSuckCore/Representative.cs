using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepSuckCore
{
  public class Representative : IAdvocate
  {
    public IEnumerable<AdvocateVote> Votes { get; set; }

    public Representative()
    {
      Votes = new List<AdvocateVote>();
    }
  }
}
