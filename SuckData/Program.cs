using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuckData
{
  class Program
  {
    private static IContainer Container { get; set; }

    static void Main(string[] args)
    {
      var builder = new ContainerBuilder();
      builder.RegisterType<GovTrackWrapper>().As<IDownloadBills>();
      Container = builder.Build();

      IDownloadBills govTrack = Container.Resolve<IDownloadBills>();
      var criteria = new BillCriteria
      {
        NewerThan = DataCacheMeta.LastDownload,
      };
      criteria.HavingStatus.Add(BillCriteria.BillStatus.Introduced);

      var bills = govTrack.DownloadBills(criteria);
    }
  }
}
