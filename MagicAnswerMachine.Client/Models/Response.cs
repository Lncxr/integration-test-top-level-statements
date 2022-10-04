using Microsoft.Extensions.Configuration;

namespace MagicAnswerMachine.Client.Models
{
    public class Response : IResponse
    {
        // Private members
        private string[] m_answers =
            { "MagicAnswerMachine has no responses! Please troubleshoot appsettings.json..." };

        // Publicly-accessible IResponse properties
        public string[] Answers { get => m_answers; }

        // Constructor taking configuration source,
        // assign Response.Answers from supplied JSON array
        public Response(IConfiguration config)
        {
            m_answers = config.GetSection("Answers")
                .Get<string[]>();
        }

        // Publicly-accessible IResponse methods
        public string ReturnRandomAnswer()
        {
            // Guard case
            if (Answers is null)
                return "";

            return Answers[new Random().Next(Answers.Length - 1)];
        }
    }
}
