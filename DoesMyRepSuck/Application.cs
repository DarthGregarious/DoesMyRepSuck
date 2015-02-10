using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepSuckCore;

namespace RepSuckCLI
{
  public class Application : IApplication
  {
    public IApplicationDelegate CurrentDelegate { get; private set; }
    private List<IPerson> people;
    private List<Issue> issues;
    private bool shouldExit;

    public IEnumerable<IPerson> People { get { return people; } }
    public IEnumerable<Issue> Issues { get { return issues; } }

    private Dictionary<string, IApplicationDelegate> registeredDelegates;

    public Application()
    {
      registeredDelegates = new Dictionary<string, IApplicationDelegate>();

      RegisterDelegate("np", new NewPersonAppDelegate(this));
      RegisterDelegate("ni", new NewIssueAppDelegate(this));
      RegisterDelegate("rv", new VoteRecorderAppDelegate(this));

      shouldExit = false;
      people = new List<IPerson>();
      issues = new List<Issue>();
    }

    public string GetPrompt()
    {
      if(CurrentDelegate == null)
      {
        return "Welcome to 'Does My Rep Suck?' Enter 'np' to create a new person; 'ni' to enter a new issue'; 'rv' to record a vote for a person; or 'cp' to compare voting histories."; ;
      }
      else
      {
        return CurrentDelegate.GetPrompt();
      }
    }

    public void HandleCommand(string command)
    {
      if(CurrentDelegate != null)
      {
        CurrentDelegate.HandleInput(command);
      }
      else if (registeredDelegates.Keys.Contains(command))
      {
        CurrentDelegate = registeredDelegates[command];
      }
      else
      {
        throw new InvalidOperationException();
      }
    }

    public void AddPerson(Person p)
    {
      people.Add(p);
    }

    public void AddIssue(Issue i)
    {
      issues.Add(i);
    }

    public void DelegateComplete()
    {
      CurrentDelegate = null;
    }

    public bool ShouldExit { get { return shouldExit; } }

    public void RegisterDelegate(string command, IApplicationDelegate del)
    {
      registeredDelegates.Add(command, del);
    }
  }
}
