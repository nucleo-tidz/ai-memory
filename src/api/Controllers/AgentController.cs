namespace api.Controllers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Http.Headers;

    using infrastructure.Agents;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.SemanticKernel.Memory;

    [Route("api/[controller]")]
    [ApiController]
    [Experimental("Memory")]
    public class AgentController(INucleotidzAgent nucleotidz) : ControllerBase
    {
        [HttpGet("chat/{message}/{username}/thread/{threadId}")]
        [HttpGet("chat/{message}/{username}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Chat(string message, string username, string? threadId)
        {
            
            
            var response = await nucleotidz.Execute(message,username,threadId);
            return Ok(response);
        }        
    }
}
