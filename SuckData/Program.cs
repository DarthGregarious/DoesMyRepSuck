using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuckData
{
  class Program
  {
    static void Main(string[] args)
    {
      var govTrack = new GovTrackWrapper();
      var criteria = new BillCriteria
      {
        NewerThan = DataCacheMeta.LastDownload,
      };
      criteria.HavingStatus.Add(BillCriteria.BillStatus.Introduced);

      var bills = govTrack.DownloadBills(criteria);
    }
  }
}
