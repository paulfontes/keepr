
namespace keepr.Services;

public class VaultKeepsService
{
    private readonly VaultKeepsRepository _repository;

    public VaultKeepsService(VaultKeepsRepository repository)
    {
        _repository = repository;
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
}