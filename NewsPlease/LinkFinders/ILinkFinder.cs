using System.Collections.Generic;

namespace NewsPlease.LinkFinders
{
    public interface ILinkFinder
    {
        IEnumerable<string> GetLinks();
    }
}