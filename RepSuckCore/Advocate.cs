using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepSuckCore
{
  public interface IAdvocate
  {
    IEnumerable<AdvocateVote> Votes { get; set; }
  }
}
