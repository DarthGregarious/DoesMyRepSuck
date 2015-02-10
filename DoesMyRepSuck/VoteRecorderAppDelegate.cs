using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepSuckCore;

namespace RepSuckCLI
{
  public class VoteRecorderAppDelegate : IApplicationDelegate
  {
    private IApplication application;
    private IPerson selectedPerson;
    private Issue selectedIssue;
    private Position recordedVote;

    private IEnumerable<PersonListItem> OrderedNumberedPeople
    {
      get
      {
        int i = 0;
        return application.People
        .OrderBy(p => p.LastName)
        .Select(p => new PersonListItem(++i, p));
      }
    }

    private IEnumerable<IssueListItem> OrderedNumberedIssues
    {
      get
      {
        int x = 0;
        return application.Issues
        .OrderBy(i => i.Title)
        .Select(i => new IssueListItem(++x, i));
      }
    }

    private IEnumerable<IssueOptionListItem> OrderedNumberedIssueOptions
    {
      get
      {
        if (selectedIssue == null)
          return Enumerable.Empty<IssueOptionListItem>();

        int x = 0;
        return selectedIssue.Options
        .OrderBy(o => o)
        .Select(o => new IssueOptionListItem(++x, o));
      }
    }

    public VoteRecorderAppDelegate(IApplication app)
    {
      this.application = app;
    }

    public string GetPrompt()
    {
      if(!application.People.Any() && !application.Issues.Any())
      {
        return "You must enter at least one person and one issue before attempting to record a vote or opinion. Press enter to continue.";
      }
      else if(!application.Issues.Any())
      {
        return "You must enter at least one issue before attempting to record a vote or opinion. Press enter to continue.";
      }
      else if(!application.People.Any())
      {
        return "You must enter at least one person before attempting to record a vote or opinion. Press enter to continue.";
      }

      if (selectedPerson == null)
      {
        return "Select a person to record a vote or opinion:\r" +
          String.Join("\r", OrderedNumberedPeople
          .Select(p => String.Format("{0}. {1}", p.Number, p.Person.FirstLastName)));
      }
      else if (selectedIssue == null)
      {
        return String.Format("Select an issue to record {0}'s vote:\r{1}", selectedPerson.FirstLastName,
          String.Join("\r", OrderedNumberedIssues
          .Select(i => String.Format("{0}. {1}", i.Number, i.Issue.Title))));
      }
      else if (recordedVote == null)
      {
        return String.Format("Select {0}'s vote on {1}:\r{2}", selectedPerson.FirstLastName, selectedIssue.Title,
          String.Join("\r", OrderedNumberedIssueOptions
            .Select(o => String.Format("{0}. {1}", o.Number, o.Option))));
      }
      
      return String.Format("Recorded {0}'s vote on \"{1}\" as \"{2}.\" Press enter to continue.", selectedPerson.FirstLastName, recordedVote.Issue.Title, recordedVote.OptionChosen);
    }

    public void HandleInput(string input)
    {
      if (!application.People.Any() || !application.Issues.Any())
      {
        application.DelegateComplete();
        return;
      }

      int x = -1;
      var numberParsed = Int32.TryParse(input, out x);

      if (selectedPerson == null)
      {
        selectedPerson = OrderedNumberedPeople.Single(p => p.Number == x).Person;
      }
      else if(selectedIssue == null)
      {
        selectedIssue = OrderedNumberedIssues.Single(i => i.Number == x).Issue;
      }
      else if(recordedVote == null)
      {
        var selectedOption = OrderedNumberedIssueOptions.Single(i => i.Number == x).Option;
        recordedVote = new Position(selectedIssue, selectedOption);
        selectedPerson.AddPosition(recordedVote);
      }
      else
      {
        application.DelegateComplete();
      }
    }

    private class PersonListItem
    {
      public int Number {get; private set;}
      public IPerson Person {get; private set;}

      public PersonListItem (int number, IPerson p)
	    {
        Number = number;
        Person = p;
	    }
    }

    private class IssueListItem
    {
      public int Number { get; private set; }
      public Issue Issue { get; private set; }

      public IssueListItem(int number, Issue p)
      {
        Number = number;
        Issue = p;
      }
    }

    private class IssueOptionListItem
    {
      public int Number { get; private set; }
      public string Option { get; private set; }

      public IssueOptionListItem(int number, string option)
      {
        Number = number;
        Option = option;
      }
    }
  }
}
