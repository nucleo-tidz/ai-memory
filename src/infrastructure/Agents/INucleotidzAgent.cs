namespace infrastructure.Agents
{
    using System.Threading.Tasks;

    public interface INucleotidzAgent
    {
        Task<string> Execute(string input);
    }
}
