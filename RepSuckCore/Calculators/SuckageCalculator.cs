using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepSuckCore.Calculators
{
  public class SuckageCalculator : ISuckageCalculator
  {
    protected List<AdvocateVote> realVotes;
    protected List<AdvocateVote> idealVotes;

    public SuckageCalculator(IEnumerable<AdvocateVote> realVotes, IEnumerable<AdvocateVote> idealVotes)
    {
      if (realVotes.Count() < 1 || idealVotes.Count() < 1) throw new ArgumentException("You must supply at least one actual vote and one ideal vote.");

      this.realVotes = realVotes.ToList();
      this.idealVotes = idealVotes.ToList();
    }

    public float Calculate()
    {
      int totalVotes = 0;
      int voteScore = 0;
      bool setAlignment = false;

      foreach (var realVote in realVotes)
      {
        var motion = realVote.Motion;
        var idealVote = idealVotes.SingleOrDefault(v => v.Motion.Equals(motion));
        if (idealVote != null)
        {
          setAlignment = true;
          totalVotes++;
          if (realVote.Approval == idealVote.Approval) voteScore++;
        }
      }

      if (!setAlignment) throw new ArgumentException("You must supply at least one real/ideal vote pair for an accurate calculation.");
      return voteScore / totalVotes;
    }
  }
}
