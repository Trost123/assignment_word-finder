using System;

namespace Interfaces.Input
{
    public interface IUserInputHandler
    {
        event Action<string> WordSubmitted;
        
        void ShakeInputField();
    }
}