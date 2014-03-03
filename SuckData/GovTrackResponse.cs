using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SuckData
{
  [DataContract]
  public class GovTrackResponse
  {
    [DataMember(Name="objects")]
    public GovTrackBill[] Bills { get; set; }
  }

  [DataContract]
  public class GovTrackBill
  {
    [DataMember(Name="display_number")]
    public string DisplayNumber { get; set; }

    [DataMember(Name = "title")]
    public string Title { get; set; }

    [DataMember(Name = "title_without_number")]
    public string TitleWithoutNumber { get; set; }
  }
}
