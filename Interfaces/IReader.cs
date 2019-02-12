using Models;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IReader
    {
        List<string> GetGameSequences();
        GameModel GetGameModel(string sequence);
    }
}
