using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RepSuckCore;

namespace RepSuckCLI.Test
{
  [TestClass]
  public class VoteRecorderAppDelegateShould
  {
    [TestMethod]
    public void WarnUserThatThereAreNoPeopleAndNoIssues_WhenApplicationHasNoPeopleAndNoIssues()
    {
      var app = new Mock<IApplication>();
      var del = new VoteRecorderAppDelegate(app.Object);
      Assert.AreEqual("You must enter at least one person and one issue before attempting to record a vote or opinion. Press enter to continue."
        , del.GetPrompt());
    }

    [TestMethod]
    public void RelinquishControl_WhenApplicationHasNoPeopleAndNoIssues()
    {
      var app = new Mock<IApplication>();
      var del = new VoteRecorderAppDelegate(app.Object);
      del.HandleInput("");
      app.Verify(a => a.DelegateComplete(), Times.Once);
    }

    [TestMethod]
    public void WarnUserThatThereAreNoIssues_WhenApplicationHasAtLeastOnePersonButNoIssues()
    {
      var app = new Mock<IApplication>();
      var person1 = new Person("Andrew", "Kvochick");
      app.Setup(a => a.People).Returns(() => new[] { person1 });
      var del = new VoteRecorderAppDelegate(app.Object);
      Assert.AreEqual("You must enter at least one issue before attempting to record a vote or opinion. Press enter to continue."
        , del.GetPrompt());
    }

    [TestMethod]
    public void RelinquishControl_WhenApplicationHasAtLeastOnePersonButNoIssues()
    {
      var app = new Mock<IApplication>();
      var person1 = new Person("Andrew", "Kvochick");
      app.Setup(a => a.People).Returns(() => new[] { person1 });
      var del = new VoteRecorderAppDelegate(app.Object);
      del.HandleInput("");
      app.Verify(a => a.DelegateComplete(), Times.Once);
    }

    [TestMethod]
    public void WarnUserThatThereAreNoPeople_WhenApplicationHasAtLeastOneIssueButNoPeople()
    {
      var app = new Mock<IApplication>();
      app.Setup(a => a.Issues).Returns(() => new[] { new Issue("Title", Enumerable.Empty<string>()) });
      var del = new VoteRecorderAppDelegate(app.Object);
      Assert.AreEqual("You must enter at least one person before attempting to record a vote or opinion. Press enter to continue."
        , del.GetPrompt());
    }

    [TestMethod]
    public void RelinquishControl_WhenApplicationHasAtLeastOneIssueButNoPeople()
    {
      var app = new Mock<IApplication>();
      app.Setup(a => a.Issues).Returns(() => new[] { new Issue("Title", Enumerable.Empty<string>()) });
      var del = new VoteRecorderAppDelegate(app.Object);
      del.HandleInput("");
      app.Verify(a => a.DelegateComplete(), Times.Once);
    }

    [TestMethod]
    public void PromptUserWithListOfPeople_WithPeopleFromApplicationInAlphabeticalOrder_WhenApplicationHasPeopleAndIssues()
    {
      var app = new Mock<IApplication>();
      var person1 = new Person("Andrew", "Kvochick");
      var person2 = new Person("Jim", "Jordan");
      app.Setup(a => a.People).Returns(() => new[] { person1, person2 });
      app.Setup(a => a.Issues).Returns(() => new[] { new Issue("Title", Enumerable.Empty<string>()) });
      var del = new VoteRecorderAppDelegate(app.Object);
      Assert.AreEqual("Select a person to record a vote or opinion:\r1. Jim Jordan\r2. Andrew Kvochick"
        , del.GetPrompt());
    }

    [TestMethod]
    public void PromptUserWithListOfIssues_InAlphabeticalOrder_WhenApplicationHasPeopleAndIssues_AndPersonIsSelected()
    {
      var app = new Mock<IApplication>();
      var person1 = new Person("Andrew", "Kvochick");
      var person2 = new Person("Jim", "Jordan");
      app.Setup(a => a.People).Returns(() => new[] { person1, person2 });
      app.Setup(a => a.Issues).Returns(() => new[] { new Issue("HB104", Enumerable.Empty<string>()), new Issue("HR200", Enumerable.Empty<string>()) });
      var del = new VoteRecorderAppDelegate(app.Object);
      del.HandleInput("1");
      Assert.AreEqual("Select an issue to record Jim Jordan's vote:\r1. HB104\r2. HR200"
        , del.GetPrompt());
    }

    [TestMethod]
    public void PromptUserWithListOfOptions_InAlphabeticalOrder_WhenPersonAndIssueAreSelected()
    {
      var app = new Mock<IApplication>();
      var person1 = new Person("Andrew", "Kvochick");
      var person2 = new Person("Jim", "Jordan");
      app.Setup(a => a.People).Returns(() => new[] { person1, person2 });
      app.Setup(a => a.Issues).Returns(() => new[] { new Issue("HB104", new[] { "Yea", "Nay" }), new Issue("HR200", new[] { "Yea", "Nay" }) });
      var del = new VoteRecorderAppDelegate(app.Object);
      del.HandleInput("1");
      del.HandleInput("1");
      Assert.AreEqual("Select Jim Jordan's vote on HB104:\r1. Nay\r2. Yea"
        , del.GetPrompt());
    }

    [TestMethod]
    public void RecordPosition_AfterUserSelectsOption()
    {
      var app = new Mock<IApplication>();
      var person1 = new Person("Andrew", "Kvochick");
      var person2 = new Mock<IPerson>();
      person2.Setup(p => p.LastName).Returns("Jordan");
      person2.Setup(p => p.FirstLastName).Returns("Jim Jordan");
      app.Setup(a => a.People).Returns(() => new IPerson[] { person1, person2.Object });
      var issue1 = new Issue("HB104", new[] { "Yea", "Nay" });
      app.Setup(a => a.Issues).Returns(() => new[] { issue1, new Issue("HR200", new[] { "Yea", "Nay" }) });
      var del = new VoteRecorderAppDelegate(app.Object);
      del.HandleInput("1");
      del.HandleInput("1");
      del.HandleInput("1");
      person2.Verify(p => p.AddPosition(It.Is<Position>(pos => pos.Issue == issue1 && pos.OptionChosen == "Nay")), Times.Once);
    }

    [TestMethod]
    public void IndicateThatVoteWasRecorded_AfterUserRecordsVote()
    {
      var app = new Mock<IApplication>();
      var person1 = new Person("Andrew", "Kvochick");
      var person2 = new Person("Jim", "Jordan");
      app.Setup(a => a.People).Returns(() => new[] { person1, person2 });
      app.Setup(a => a.Issues).Returns(() => new[] { new Issue("HB104", new[] { "Yea", "Nay" }), new Issue("HR200", new[] { "Yea", "Nay" }) });
      var del = new VoteRecorderAppDelegate(app.Object);
      del.HandleInput("1");
      del.HandleInput("1");
      del.HandleInput("1");
      Assert.AreEqual("Recorded Jim Jordan's vote on \"HB104\" as \"Nay.\" Press enter to continue."
        , del.GetPrompt());
    }

    [TestMethod]
    public void RelinquishesControl_AfterUserRecordsVote()
    {
      var app = new Mock<IApplication>();
      var person1 = new Person("Andrew", "Kvochick");
      var person2 = new Person("Jim", "Jordan");
      app.Setup(a => a.People).Returns(() => new[] { person1, person2 });
      app.Setup(a => a.Issues).Returns(() => new[] { new Issue("HB104", new[] { "Yea", "Nay" }), new Issue("HR200", new[] { "Yea", "Nay" }) });
      var del = new VoteRecorderAppDelegate(app.Object);
      del.HandleInput("1");
      del.HandleInput("1");
      del.HandleInput("1");
      del.HandleInput("");
      app.Verify(a => a.DelegateComplete(), Times.Once);
    }
  }
}
