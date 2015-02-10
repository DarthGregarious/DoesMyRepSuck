using System.Collections;
using System.Collections.Generic;
using RepSuckCore;

namespace RepSuckCLI
{
  public interface IApplication
  {
    IEnumerable<IPerson> People { get; }
    IEnumerable<Issue> Issues { get; }
    void AddPerson(Person p);
    void DelegateComplete();
    void AddIssue(Issue i);
  }
}
