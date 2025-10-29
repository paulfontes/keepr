

namespace keepr.Repositories;

public class KeepsRepository
{
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Keep CreateKeep(Keep keepData)
    {
        string sql = @"
        INSERT INTO
    keeps (
        name,
        description,
        img,
        views,
        creator_id
    )
    VALUES (
        @Name,
        @Description,
        @Img,
        @Views,
        @CreatorId
    );
    SELECT
         keeps.*,
         accounts.*
         FROM keeps
         JOIN accounts ON accounts.id = keeps.creator_id
         WHERE keeps.id = LAST_INSERT_ID()
              ;";

        Keep keep = _db.Query(sql, (Keep keep, Creator creator) =>
        {
            keep.Creator = creator;
            return keep;
        }, keepData).SingleOrDefault();
        return keep;
    }


    internal List<Keep> GetKeeps()
    {
        string sql = @"
        SELECT
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON accounts.id = keeps.creator_id
        ORDER BY keeps.created_at ASC
        ;";
        List<Keep> keeps = _db.Query(sql, (Keep keep, Creator creator) =>
        {
            keep.Creator = creator;
            return keep;
        }).ToList();
        return keeps;
    }
    internal Keep GetKeepById(int keepId)
    {
        string sql = @"
        SELECT
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON accounts.id = keeps.creator_id
        WHERE keeps.id = @KeepId;";

        Keep keep = _db.Query(sql, (Keep keep, Creator creator) =>
        {
            keep.Creator = creator;
            return keep;
        }, new { keepId }).SingleOrDefault();
        return keep;
    }

    internal void UpdateKeep(Keep newKeepData)
    {
        string sql = @"
        UPDATE 
        keeps
        SET
        name = @Name,
        description = @Description,
        views = @Views
        WHERE id = @Id LIMIT 1
        ;";

        int rowsAffected = _db.Execute(sql, newKeepData);

        if (rowsAffected != 1)
        {
            throw new Exception("Multiple rows of data may have been messed with!");
        }

    }

    internal void DeleteKeep(int keepId)
    {
        string sql = @"
        DELETE FROM vaultkeeps WHERE keep_id = @keepId;
        DELETE FROM keeps WHERE id = @keepId LIMIT 1
        ;";
        int rowsAffected = _db.Execute(sql, new { keepId });

        if (rowsAffected != 1)
        {
            throw new Exception("Multiple rows of data may have been messed with!");
        }

    }

    internal List<Keep> GetKeepsInPublicVault(int vaultId)
    {
        string sql = @"
        SELECT
        keeps.*,
        accounts.*,
        vaultkeeps.*
        FROM vaultkeeps
        JOIN keeps ON keeps.id = vaultkeeps.keep_id
        JOIN accounts ON accounts.id = keeps.creator_id
        WHERE vaultkeeps.vault_id = @vaultId;";

        List<Keep> keeps = _db.Query<Keep, Creator, VaultKeep, Keep>(sql, (keep, creator, vaultKeep) =>
        {
            keep.VaultKeepId = vaultKeep.Id;
            keep.Creator = creator;
            keep.Kept = vaultKeep.Id;
            return keep;
        }, new { vaultId }, splitOn: "Id,Id").ToList();

        return keeps;
    }
}