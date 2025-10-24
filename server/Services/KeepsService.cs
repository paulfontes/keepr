



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
}