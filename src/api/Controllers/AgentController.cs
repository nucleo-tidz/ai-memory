namespace api.Controllers
{
    using infrastructure.Agents;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class AgentController(INucleotidzAgent nucleotidz) : ControllerBase
    {
        [HttpGet("chat/{message}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Chat(string message)
        {
            //A container scheduled for dispatch has a gross weight of 9500 kg and a volume of 8.3 CBM. It is designated for a FuelSensitive route and requires special handling due to the presence of hazardous materials. Please calculate the total shipping cost using a base rate of ₹1001 per CBM.
            var response = await nucleotidz.Execute(message);
            return Ok(response);
        }
    }
}
