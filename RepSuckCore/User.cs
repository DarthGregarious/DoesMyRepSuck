using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepSuckCore
{
  public class User : IAdvocate
  {
    public IEnumerable<AdvocateVote> Votes { get; set; }
    public IEnumerable<IAdvocate> TrustedAdvocates { get; set; }

    public User()
    {
      TrustedAdvocates = new List<IAdvocate>();
    }   
  }
}
