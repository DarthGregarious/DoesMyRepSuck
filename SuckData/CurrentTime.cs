using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuckData
{
  public class CurrentTime
  {
    public static Func<DateTime> GetTime;

    public static DateTime Now
    {
      get
      {
        return GetTime();
      }
    }

    static CurrentTime()
    {
      GetTime = () => DateTime.Now;
    }
  }
}
