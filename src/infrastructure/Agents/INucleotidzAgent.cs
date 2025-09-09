namespace infrastructure.Agents
{
    using model;
    using System.Threading.Tasks;

    public interface INucleotidzAgent
    {
        Task<AgentResponse> Execute(string input, string userName, string threadId);
    }
}
