using System.Collections.Generic;

namespace RepSuckCore
{
  public class Issue
  {
    public string Title { get; private set; }
    public IEnumerable<string> Options { get; private set; }

    public Issue(string title, IEnumerable<string> options)
    {
      Title = title;
      Options = options;
    }
  }
}
