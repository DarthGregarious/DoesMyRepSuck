using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuckData
{
  public class DataCacheMeta
  {
    public static Lazy<DateTime> GetLastDownload { get; set; }
    public static DateTime LastDownload
    {
      get
      {
        return GetLastDownload.Value;
      }
    }

    static DataCacheMeta()
    {
      GetLastDownload = new Lazy<DateTime>(() => new DateTime(2014,1,1));
    }
  }
}
