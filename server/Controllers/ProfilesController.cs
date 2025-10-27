namespace keepr.Controllers;

[ApiController]
[Route("api/profiles")]
public class ProfilesController : ControllerBase
{
    private readonly ProfilesService _profilesService;
    private readonly Auth0Provider _auth;

    public ProfilesController(ProfilesService profilesService, Auth0Provider auth)
    {
        _profilesService = profilesService;
        _auth = auth;
    }

    [HttpGet("{profileId}")]
    public ActionResult<Creator> GetProfile(string profileId)
    {
        try
        {
            Creator profile = _profilesService.GetProfile(profileId);
            return Ok(profile);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{profileId}/keeps")]
    public ActionResult<List<Keep>> GetProfileKeeps(string profileId)
    {
        try
        {
            List<Keep> keeps = _profilesService.GetProfileKeeps(profileId);
            return Ok(keeps);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{profileId}/vaults")]
    public ActionResult<List<Vault>> GetProfileVaults(string profileId)
    {
        try
        {
            List<Vault> vaults = _profilesService.GetProfileVaults(profileId);
            return Ok(vaults);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}