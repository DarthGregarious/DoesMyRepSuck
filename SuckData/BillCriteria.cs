using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuckData
{
  public class BillCriteria
  {
    public enum BillStatus { Introduced }

    public DateTime? NewerThan { get; set; }
    public DateTime OlderThan { get; set; }
    public IList<BillStatus> HavingStatus { get; set; }

    public BillCriteria()
    {
      OlderThan = CurrentTime.Now;
      HavingStatus = new List<BillStatus>();
    }
  }
}
