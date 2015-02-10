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
  public class NewIssueAppDelegateShould
  {
    [TestMethod]
    public void PromptForIssueName_WhenNoIssueIsInProgress()
    {
      var appMock = new Mock<IApplication>();
      var del = new NewIssueAppDelegate(appMock.Object);
      Assert.AreEqual("Enter a title for the issue: ", del.GetPrompt());
    }

    [TestMethod]
    public void PromptForOptionOne_AfterTitleInput()
    {
      var appMock = new Mock<IApplication>();
      var del = new NewIssueAppDelegate(appMock.Object);
      del.HandleInput("My First Issue");
      Assert.AreEqual("Enter voting option 1 (blank to end): ", del.GetPrompt());
    }

    [TestMethod]
    public void PromptForOption100_AfterNinetyNineOptions()
    {
      var appMock = new Mock<IApplication>();
      var del = new NewIssueAppDelegate(appMock.Object);
      del.HandleInput("My First Issue");
      for (int i = 0; i < 99; i++)
        del.HandleInput("Option " + i.ToString());
      Assert.AreEqual("Enter voting option 100 (blank to end): ", del.GetPrompt());
    }

    [TestMethod]
    public void AddIssueToApplication_AfterBlankOptionEntry()
    {
      var appMock = new Mock<IApplication>();
      var del = new NewIssueAppDelegate(appMock.Object);
      del.HandleInput("My First Issue");
      for (int i = 0; i < 99; i++)
        del.HandleInput("Option " + i.ToString());
      del.HandleInput("");
      appMock.Verify(app => app.AddIssue(It.Is<Issue>(i => i.Title == "My First Issue" && i.Options.Count() == 99)), Times.Once);
    }

    [TestMethod]
    public void RelinquishControl_AfterBlankOptionEntry()
    {
      var appMock = new Mock<IApplication>();
      var del = new NewIssueAppDelegate(appMock.Object);
      del.HandleInput("My First Issue");
      for (int i = 0; i < 99; i++)
        del.HandleInput("Option " + i.ToString());
      del.HandleInput("");
      appMock.Verify(app => app.DelegateComplete(), Times.Once);
    }
  }
}
