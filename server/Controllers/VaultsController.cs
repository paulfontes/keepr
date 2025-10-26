namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultsController : ControllerBase
{
    private readonly VaultsService _vaultsService;
    private readonly Auth0Provider _auth;

    public VaultsController(VaultsService vaultsService, Auth0Provider auth)
    {
        _vaultsService = vaultsService;
        _auth = auth;
    }

    [HttpPost, Authorize]
    public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            vaultData.CreatorId = userInfo.Id;
            Vault vault = _vaultsService.CreateVault(vaultData);
            return Ok(vault);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{vaultId}")]
    public ActionResult<Vault> GetVaultById(int vaultId)
    {
        try
        {
            Vault vault = _vaultsService.GetVaultById(vaultId);
            return Ok(vault);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut("{vaultId}"), Authorize]
    public async Task<ActionResult<Vault>> UpdateVault([FromBody] Vault vaultData, int vaultId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            Vault vault = _vaultsService.UpdateVault(vaultData, vaultId, userInfo.Id);
            return vault;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}