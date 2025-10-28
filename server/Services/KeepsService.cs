
namespace keepr.Services;

public class KeepsService
{
    private readonly KeepsRepository _repository;

    public KeepsService(KeepsRepository repository)
    {
        _repository = repository;
    }

    internal Keep CreateKeep(Keep keepData)
    {
        Keep keep = _repository.CreateKeep(keepData);
        return keep;
    }

    internal List<Keep> GetKeeps()
    {
        List<Keep> keep = _repository.GetKeeps();
        return keep;
    }
    internal Keep GetKeepById(int keepId)
    {
        Keep keep = _repository.GetKeepById(keepId);
        if (keep == null)
        {
            throw new Exception("There is no keep at that Id!");
        }
        return keep;
    }

    internal Keep UpdateKeep(Keep newKeepData, int keepId, Account userInfo)
    {
        Keep oldKeepData = GetKeepById(keepId);
        if (oldKeepData.CreatorId != userInfo.Id)
        {
            throw new Exception("You can't update other peoples Keeps!!");
        }

        oldKeepData.Name = newKeepData.Name ?? oldKeepData.Name;
        oldKeepData.Description = newKeepData.Description ?? oldKeepData.Description;

        _repository.UpdateKeep(oldKeepData);

        return oldKeepData;
    }

    internal void DeleteKeep(int keepId, string userId)
    {
        Keep keep = GetKeepById(keepId);
        if (keep.CreatorId != userId)
        {
            throw new Exception("You Can't Delete a keep that's not yours!");
        }
        _repository.DeleteKeep(keep.Id);

    }

    internal List<Keep> GetKeepsInVault(int vaultId, Vault vault, string userId)
    {
        List<Keep> keeps = _repository.GetKeepsInPublicVault(vaultId);
        if (vault.IsPrivate == true)
        {
            if (vault.IsPrivate == true && vault.CreatorId == userId)
            {
                return keeps;
            }
            throw new Exception("You do not have access to this Vault's Keeps");
        }
        return keeps;
    }

    internal Keep increaseKeepViews(Keep keep)
    {
        keep.Views++;
        _repository.UpdateKeep(keep);
        return keep;
    }

    // internal List<Keep> GetKeepsInPrivateVault(int privateVaultId, Vault vault, string userId)
    // {
    //     if (vault.IsPrivate == false && vault.CreatorId != userId)
    //     {
    //         throw new Exception("You do not have access to this Vault's Keeps");
    //     }
    //     List<Keep> keeps = _repository.GetKeepsInPrivateVault(privateVaultId);
    //     return keeps;
    // }
}