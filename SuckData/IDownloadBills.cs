using RepSuckCore.UnitedStates.Congress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuckData
{
  public interface IDownloadBills
  {
    IEnumerable<Bill> DownloadBills(BillCriteria criteria);
  }
}
