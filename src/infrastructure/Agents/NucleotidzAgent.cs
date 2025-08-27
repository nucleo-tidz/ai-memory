namespace infrastructure.Agents
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Azure.AI.Agents.Persistent;

    using Microsoft.Extensions.Configuration;
    using Microsoft.SemanticKernel;
    using Microsoft.SemanticKernel.Agents;
    using Microsoft.SemanticKernel.Agents.AzureAI;
    using Microsoft.SemanticKernel.ChatCompletion;
    using Microsoft.SemanticKernel.Memory;

    internal class NucleotidzAgent(Kernel _kernel, IConfiguration configuration, IHttpClientFactory httpClientFactory, PersistentAgentsClient agentsClient) : AgentBase(_kernel, agentsClient), INucleotidzAgent
    {
        public async Task<string> Execute(string input, string userName, string threadId)
        {
            var httpClient = httpClientFactory.CreateClient("MemoryClient");
            var mem0Provider = new Mem0Provider(httpClient, options: new()
            {
                UserId = userName,
            });       
            #region Update response schema if neeeded
            //await base.UpdateAgent(configuration["AgentId"], responseFormat);
            #endregion
            var agent = base.GetAzureAgent(configuration["AgentId"]);
            AgentThread thread = string.IsNullOrEmpty(threadId) ? new AzureAIAgentThread(agent.Item2):new AzureAIAgentThread(agent.Item2, threadId);
            #region Add Context Provider
          
            thread.AIContextProviders.Add(mem0Provider);
            #endregion
            ChatMessageContent chatMessageContent = new(AuthorRole.User, input);
            string agentReply = string.Empty;
            await foreach (ChatMessageContent response in agent.Item1.InvokeAsync(chatMessageContent, thread))
            {
                agentReply = agentReply + response.Content;
            }
            return agentReply;
        }
    }
}
