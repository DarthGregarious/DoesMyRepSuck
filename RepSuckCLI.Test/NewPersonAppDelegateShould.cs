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
  public class NewPersonAppDelegateShould
  {
    [TestMethod]
    public void PromptForLastName_WhenStarted()
    {
      var appMock = new Mock<IApplication>();
      var del = new NewPersonAppDelegate(appMock.Object);
      Assert.AreEqual(del.GetPrompt(), @"Creating a new person. Enter Last Name (ESC to cancel): ");
    }

    [TestMethod]
    public void HandleInput_ByRecordingLastNamePrivately_WhenNoPersonWasInProgress()
    {
      var appMock = new Mock<IApplication>();
      var del = new NewPersonAppDelegate(appMock.Object);
      del.HandleInput("Kvochick");
      Assert.AreEqual(del.LastName, "Kvochick");
    }

    [TestMethod]
    public void HandleInput_ByAddingPersonToApplication_WhenPersonIsInProgress()
    {
      var appMock = new Mock<IApplication>();
      var del = new NewPersonAppDelegate(appMock.Object);
      del.HandleInput("Kvochick");
      del.HandleInput("Andrew");
      appMock.Verify(app => app.AddPerson(It.Is<Person>(p => p.FirstLastName == "Andrew Kvochick")), Times.Once());
    }

    [TestMethod]
    public void HandleInput_ByReturningControlToApplication_WhenPersonIsInProgress()
    {
      var appMock = new Mock<IApplication>();
      var del = new NewPersonAppDelegate(appMock.Object);
      del.HandleInput("Kvochick");
      del.HandleInput("Andrew");
      appMock.Verify(app => app.DelegateComplete(), Times.Once());
    }
  }
}
