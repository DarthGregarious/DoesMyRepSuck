using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuckData
{
  public class GovTrackBillCriteria
  {
    private const string SEPARATOR = "|";

    private string[] currentStatuses;
    private Tuple<DateTime?, DateTime?> currentStatusDates;
 
    private Dictionary<BillCriteria.BillStatus, string> statusMap = new Dictionary<BillCriteria.BillStatus, string> 
    { 
      {BillCriteria.BillStatus.Introduced, "introduced"}
    };

    private string FormatDate(DateTime date)
    {
      return date.ToString("yyyy-MM-dd");
    }

    public GovTrackBillCriteria(BillCriteria domainCriteria)
    {
      currentStatuses = domainCriteria.HavingStatus.Select(s => statusMap[s]).ToArray();
      currentStatusDates = new Tuple<DateTime?, DateTime?>(
        domainCriteria.NewerThan,
        domainCriteria.OlderThan
      );
    }

    private KeyValuePair<string,string> MakeKeyValuePair(string key, string[] values)
    {
      if(values.Length > 1)
      {
        key += "__in";
      }

      return new KeyValuePair<string, string>(key, String.Join(SEPARATOR, values));
    }

    private KeyValuePair<string, string> MakeKeyValuePair(string key, Tuple<DateTime?,DateTime?> range)
    {
      var value = new StringBuilder();

      if(range.Item1.HasValue && range.Item2.HasValue)
      {
        key += "__range";
      }

      if(range.Item1.HasValue) value.Append(FormatDate(range.Item1.Value));
      if(range.Item2.HasValue) value.Append(SEPARATOR + FormatDate(range.Item2.Value));

      return new KeyValuePair<string, string>(key, value.ToString());
    }

    private string ConsolidateKeyValuePair(KeyValuePair<string,string> kvp)
    {
      return String.Format("{0}={1}", kvp.Key, kvp.Value);
    }

    public string MakeQueryString()
    {
      var criteria = new List<KeyValuePair<string,string>>();
      criteria.Add(MakeKeyValuePair("current_status_date", currentStatusDates));
      criteria.Add(MakeKeyValuePair("current_status", currentStatuses));
      
      var kvps = criteria.Select(c => ConsolidateKeyValuePair(c));

      return "?" + String.Join("&", kvps);
    }
  }
}
