using RepSuckCore.UnitedStates.Congress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Runtime.Serialization.Json;

namespace SuckData
{
  public class GovTrackWrapper : IDownloadBills
  {
    private const string endpoint = "https://www.govtrack.us/api/v2/bill";
    public IEnumerable<Bill> DownloadBills(BillCriteria criteria)
    {
      var govTrackCriteria = new GovTrackBillCriteria(criteria);
      HttpWebRequest request = WebRequest.Create(endpoint + govTrackCriteria.MakeQueryString()) as HttpWebRequest;

      using(HttpWebResponse response = request.GetResponse() as HttpWebResponse)
      {
        if (response.StatusCode != HttpStatusCode.OK)
          throw new Exception(String.Format(
          "Server error (HTTP {0}: {1}).",
          response.StatusCode,
          response.StatusDescription));
        DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(GovTrackResponse));
        object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
        GovTrackResponse jsonResponse = objResponse as GovTrackResponse;

        return jsonResponse.Bills.Select(gtBill => new Bill {
          Title = gtBill.Title,
          DisplayNumber = gtBill.DisplayNumber
        });
      }
    }
  }
}
