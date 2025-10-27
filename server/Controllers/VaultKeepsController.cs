namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
    private readonly VaultKeepsService _vaultKeepsService;
    private readonly Auth0Provider _auth;

    public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth)
    {
        _vaultKeepsService = vaultKeepsService;
        _auth = auth;
    }

    [HttpPost, Authorize]
    public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultKeepData)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            vaultKeepData.CreatorId = userInfo.Id;
            VaultKeep vaultKeep = _vaultKeepsService.CreateVaultKeep(vaultKeepData, userInfo.Id);
            return Ok(vaultKeep);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpDelete("{vaultKeepId}"), Authorize]
    public async Task<ActionResult<string>> RemoveKeepFromVault(string vaultKeepId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            _vaultKeepsService.RemoveKeepFromVault(vaultKeepId, userInfo.Id);
            return Ok($"Vault {vaultKeepId} has been removed!");
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}