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
  public class ApplicationShould
  {
    [TestMethod]
    public void PromptUserWithInstructions_AtStartup()
    {
      var app = new Application();
      Assert.AreEqual(app.GetPrompt(), @"Welcome to 'Does My Rep Suck?' Enter 'np' to create a new person; 'ni' to enter a new issue'; 'rv' to record a vote for a person; or 'cp' to compare voting histories.");
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Throw_WhenCommandIsUnregistered()
    {
      var app = new Application();
      app.HandleCommand("moo");
    }

    [TestMethod]
    public void HaveNewPersonDelegate_WhenCommandIsToEnterNewPerson()
    {
      var app = new Application();
      app.HandleCommand("np");
      Assert.AreEqual(typeof(NewPersonAppDelegate), app.CurrentDelegate.GetType());
    }

    [TestMethod]
    public void HaveAnotherPerson_AfterAddPersonIsCalled()
    {
      var app = new Application();
      app.AddPerson(new Person("Test", "Test"));
      Assert.AreEqual(app.People.Count(), 1);
      Assert.AreEqual("Test Test", app.People.First().FirstLastName);
    }

    [TestMethod]
    public void DelegateToDelegate_WhenThereIsOne()
    {
      var app = new Application();
      var mockDelegate = new Mock<IApplicationDelegate>();
      app.RegisterDelegate("np", mockDelegate.Object);
      app.HandleCommand("np");
      app.HandleCommand("delegated");
      mockDelegate.Verify(del => del.HandleInput("delegated"), Times.Once);
    }

    [TestMethod]
    public void SetCurrentDelegateToNull_OnDelegateComplete()
    {
      var app = new Application();
      app.DelegateComplete();
      Assert.AreEqual(null, app.CurrentDelegate);
    }
  }
}
