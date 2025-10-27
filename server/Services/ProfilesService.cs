

namespace keepr.Services;

public class ProfilesService
{
    private readonly ProfilesRepository _profilesRepository;

    public ProfilesService(ProfilesRepository profilesRepository)
    {
        _profilesRepository = profilesRepository;
    }

    internal Creator GetProfile(string profileId)
    {
        Creator profile = _profilesRepository.GetProfile(profileId);
        if (profile == null)
        {
            throw new Exception("Profile not found");
        }
        return profile;
    }

    internal List<Keep> GetProfileKeeps(string profileId)
    {
        if (GetProfile(profileId) == null)
        {
            throw new Exception("Profile not found");
        }
        List<Keep> keeps = _profilesRepository.GetProfileKeeps(profileId);
        return keeps;
    }

    internal List<Vault> GetProfileVaults(string profileId)
    {
        if (GetProfile(profileId) == null)
        {
            throw new Exception("Profile not found");
        }
        List<Vault> vaults = _profilesRepository.GetProfileVaults(profileId);
        return vaults;
    }
}