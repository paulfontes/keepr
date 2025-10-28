using System.Threading.Tasks;

namespace keepr.Controllers;

[ApiController]
[Route("api/keeps")]
public class KeepsController : ControllerBase
{
    private readonly KeepsService _keepsService;
    private readonly Auth0Provider _auth;

    public KeepsController(KeepsService keepsService, Auth0Provider auth)
    {
        _keepsService = keepsService;
        _auth = auth;
    }


    [HttpPost, Authorize]
    public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
    {
        try
        {
            Account account = await _auth.GetUserInfoAsync<Account>(HttpContext);
            keepData.CreatorId = account.Id;
            Keep keep = _keepsService.CreateKeep(keepData);
            return Ok(keep);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }


    [HttpGet]
    public ActionResult<List<Keep>> GetKeeps()
    {
        try
        {
            List<Keep> keeps = _keepsService.GetKeeps();
            return Ok(keeps);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{keepId}")]
    public async Task<ActionResult<Keep>> GetKeepById(int keepId)
    {
        try
        {
            // Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            Keep keep = _keepsService.GetKeepById(keepId);
            _keepsService.increaseKeepViews(keep);
            return Ok(keep);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut("{keepId}"), Authorize]
    public async Task<ActionResult<Keep>> UpdateKeep([FromBody] Keep keepData, int keepId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            Keep UpdatedKeep = _keepsService.UpdateKeep(keepData, keepId, userInfo);
            return Ok(UpdatedKeep);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
    [HttpDelete("{keepId}"), Authorize]
    public async Task<ActionResult<string>> DeleteKeep(int keepId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            _keepsService.DeleteKeep(keepId, userInfo.Id);
            return Ok("Keep has been deleted!");
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}