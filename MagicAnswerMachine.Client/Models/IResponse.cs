using Microsoft.Extensions.Configuration;

namespace MagicAnswerMachine.Client.Models
{
    public interface IResponse
    {
        // Properties
        string[] Answers { get; }

        // Methods
        string ReturnRandomAnswer();
    }
}
