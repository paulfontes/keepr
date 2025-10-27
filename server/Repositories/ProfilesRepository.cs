


namespace keepr.Repositories;

public class ProfilesRepository
{
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Creator GetProfile(string profileId)
    {
        string sql = "SELECT * FROM accounts WHERE id = @profileId";
        Creator profile = _db.Query<Creator>(sql, new { profileId }).SingleOrDefault();
        return profile;
    }

    internal List<Keep> GetProfileKeeps(string profileId)
    {
        string sql = @"
        SELECT
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON accounts.id = keeps.creator_id
        WHERE keeps.creator_id = @profileId
        ;";
        List<Keep> keeps = _db.Query(sql, (Keep keep, Creator creator) =>
        {
            keep.Creator = creator;
            return keep;
        }, new { profileId }).ToList();
        return keeps;
    }

    internal List<Vault> GetProfileVaults(string profileId)
    {
        string sql = @"
        SELECT
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON accounts.id = vaults.creator_id
        WHERE vaults.is_private = false AND vaults.creator_id = @profileId
        ;";
        List<Vault> vaults = _db.Query(sql, (Vault vault, Creator creator) =>
        {
            vault.Creator = creator;
            return vault;
        }, new { profileId }).ToList();
        return vaults;
    }
}
