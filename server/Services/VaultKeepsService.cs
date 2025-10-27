
namespace keepr.Services;

public class VaultKeepsService
{
    private readonly VaultKeepsRepository _repository;
    private readonly VaultsService _vaultsService;

    public VaultKeepsService(VaultKeepsRepository repository, VaultsService vaultsService)
    {
        _repository = repository;
        _vaultsService = vaultsService;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData, string userId)
    {
        if (vaultKeepData.CreatorId != userId)
        {
            throw new Exception("You can't create another VaultKeep for a different user!");
        }
        VaultKeep vaultKeep = _repository.CreateVaultKeep(vaultKeepData);
        return vaultKeep;
    }

    private VaultKeep GetVaultKeepById(string vaultKeepId)
    {
        VaultKeep vaultKeep = _repository.GetVaultKeepById(vaultKeepId);
        return vaultKeep;
    }


    internal void RemoveKeepFromVault(string vaultKeepId, int keepId, string userId)
    {
        VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);
        if (vaultKeep == null)
        {
            throw new Exception("VaultKeep not found");
        }
        if (vaultKeep.CreatorId != userId)
        {
            throw new Exception("You can't remove a keep from a vault you don't own!");
        }
        _repository.RemoveKeepFromVault(vaultKeepId, keepId, userId);

    }

}