namespace infrastructure.Agents
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;
    using Microsoft.SemanticKernel;
    using Microsoft.SemanticKernel.Agents;
    using Microsoft.SemanticKernel.Agents.AzureAI;
    using Microsoft.SemanticKernel.ChatCompletion;
    using Microsoft.SemanticKernel.Memory;

    internal class NucleotidzAgent(Kernel _kernel, IConfiguration configuration, IHttpClientFactory httpClientFactory) : AgentBase(_kernel, configuration), INucleotidzAgent
    {
        public object responseFormat = new
        {
            type = "json_schema",
            json_schema = new
            {
                name = "<Name>",
                schema = new
                {
                    type = "object",
                    properties = new
                    {
                        ToplevelProp = new
                        {
                            type = "array",
                            items = new
                            {
                                type = "object",
                                properties = new
                                {
                                    Prop1 = new { type = "string" },
                                    Prop2 = new { type = "string" },
                                    Prop3 = new { type = "array", items = new { type = "string" } },
                                    Prop4 = new { type = "array", items = new { type = "string" } }
                                },
                                required = new[] { "Prop1", "Prop2" }
                            }
                        }
                    },
                    required = new[] { "ToplevelProp" }
                }
            }
        };


        public async Task<string> Execute(string input,string userName)
        {
            var httpClient=  httpClientFactory.CreateClient("MemoryClient");
            var mem0Provider = new Mem0Provider(httpClient, options: new()
            {
                UserId = userName,
                
            });
            #region Update response schema if neeeded
            //await base.UpdateAgent(configuration["AgentId"], responseFormat);
            #endregion
            var agent = base.GetAzureAgent(configuration["AgentId"]);
            AgentThread thread = new AzureAIAgentThread(agent.Item2);          


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
