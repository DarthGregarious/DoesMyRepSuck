using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepSuckCore.UnitedStates.Congress
{
  public class Bill
  {
    public int BillId { get; set; }
    public string DisplayNumber { get; set; }
    public string Title { get; set; }
  }

  public class CongressContext : DbContext
  {
    public DbSet<Bill> Bills { get; set; }
  }
}
