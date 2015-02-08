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
    private List<Person> people;
    private bool shouldExit;

    public IEnumerable<Person> People { get { return people; } }

    private Dictionary<string, IApplicationDelegate> registeredDelegates;

    public Application()
    {
      registeredDelegates = new Dictionary<string, IApplicationDelegate>();
      shouldExit = false;
      people = new List<Person>();
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
