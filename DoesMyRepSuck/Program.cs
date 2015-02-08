using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepSuckCLI
{
  class Program
  {
    static void Main(string[] args)
    {
      var app = new Application();
      app.RegisterDelegate("np", new NewPersonAppDelegate(app));

      while(!app.ShouldExit)
      {
        Console.WriteLine(app.GetPrompt());
        var userCommand = Console.ReadLine();
        app.HandleCommand(userCommand);
      }
    }
  }
}
