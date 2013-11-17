using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepSuckCore
{
  public class AdvocateVote
  {
    public IAdvocate Advocate { get; set; }
    public Motion Motion { get; set; }
    public bool Approval { get; set; }

    public AdvocateVote() { }

    public AdvocateVote(Motion motion, IAdvocate advocate, bool approval)
    {
      Motion = motion;
      Approval = approval;
      Advocate = advocate;
    }
  }
}
