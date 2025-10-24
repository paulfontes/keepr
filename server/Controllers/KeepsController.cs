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
    public ActionResult<Keep> GetKeeps()
    {
        try
        {
            Keep keep = _keepsService.GetKeeps();
            return Ok(keep);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}