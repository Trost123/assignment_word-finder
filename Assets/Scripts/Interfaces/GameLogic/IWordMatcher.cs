using GameLogic;

namespace Interfaces.GameLogic
{
    public interface IWordMatcher
    {
        Word MatchAndRemoveWord(string word);
    }
}