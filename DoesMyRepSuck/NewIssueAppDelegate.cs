using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepSuckCore;

namespace RepSuckCLI
{
  public class NewIssueAppDelegate : IApplicationDelegate
  {
    private IApplication app;
    private string title;
    private List<string> options;

    public NewIssueAppDelegate(IApplication app)
    {
      options = new List<string>();
      this.app = app;
    }

    public string GetPrompt()
    {
      if (string.IsNullOrEmpty(title))
        return "Enter a title for the issue: ";
      else
        return String.Format("Enter voting option {0} (blank to end): ", options.Count + 1);
    }

    public void HandleInput(string input)
    {
      if(string.IsNullOrEmpty(title))
      {
        title = input;
      }
      else if(!string.IsNullOrEmpty(input))
      {
        options.Add(input);
      }
      else
      {
        this.app.AddIssue(new Issue(title, options));
        this.app.DelegateComplete();
      }
    }
  }
}
