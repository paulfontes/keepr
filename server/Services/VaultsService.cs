



namespace keepr.Services;

public class VaultsService
{
    private readonly VaultsRepository _repository;

    public VaultsService(VaultsRepository repository)
    {
        _repository = repository;
    }

    internal Vault CreateVault(Vault vaultData)
    {
        Vault vault = _repository.CreateVault(vaultData);
        return vault;
    }


    public Vault GetVaultById(int vaultId)
    {
        Vault vault = _repository.GetVaultById(vaultId);
        if (vault == null)
        {
            throw new Exception("There is no vault at the Id of" + vaultId);
        }
        return vault;

    }

    internal Vault GetVaultById(int vaultId, string userId)
    {
        Vault vault = _repository.GetVaultById(vaultId);
        if (vault == null)
        {
            throw new Exception("There is no vault at the Id of" + vaultId);
        }
        if (vault.IsPrivate == true && vault.CreatorId != userId)
        {
            throw new Exception("You do not have access to this Vault");
        }
        return vault;
    }

    internal Vault UpdateVault(Vault vaultData, int vaultId, string userId)
    {
        Vault vault = GetVaultById(vaultId);
        if (vault.CreatorId != userId)
        {
            throw new Exception("You are allowed to access this vault!");
        }

        vault.Name = vaultData.Name ?? vault.Name;
        vault.IsPrivate = vaultData.IsPrivate ?? vault.IsPrivate;



        _repository.UpdateVault(vault);
        return vault;
    }
    internal void DeleteVault(int vaultId, string userId)
    {
        Vault vault = GetVaultById(vaultId);
        if (vault.CreatorId != userId)
        {
            throw new Exception("You can't delete other peoples Vaults!");
        }
        _repository.DeleteVault(vault.Id);
    }

    internal List<Vault> GetVaults(string id)
    {
        List<Vault> vaults = _repository.GetVaultsByUserId(id);
        return vaults;
    }
}