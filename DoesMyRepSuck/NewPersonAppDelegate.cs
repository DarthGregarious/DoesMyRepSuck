using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepSuckCore;

namespace RepSuckCLI
{
  public class NewPersonAppDelegate : IApplicationDelegate
  {
    private IApplication app;
    private string prompt;

    public NewPersonAppDelegate(IApplication app)
    {
      this.app = app;
      prompt = @"Creating a new person. Enter Last Name (ESC to cancel): ";
    }
    public string GetPrompt()
    {
      return prompt;
    }

    public void HandleInput(string input)
    {
      if(String.IsNullOrEmpty(LastName))
      {
        LastName = input;
        prompt = "Enter First Name (ESC to cancel): ";
      }
      else
      {
        app.AddPerson(new Person(input, LastName));
        app.DelegateComplete();
      }
    }

    public string LastName { get; private set; }
  }
}
