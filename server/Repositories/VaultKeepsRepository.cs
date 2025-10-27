


namespace keepr.Repositories;

public class VaultKeepsRepository
{
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {

        string sql = @"
          INSERT INTO
    vaultkeeps (
        vault_id,
        keep_id,
        creator_id
    )
    VALUES (
        @VaultId,
        @KeepId,
        @CreatorId
    );
    SELECT
         vaultkeeps.*,
         vaults.*,
         keeps.*,
         accounts.*

         FROM vaultkeeps
         JOIN vaults ON vaults.id = vaultkeeps.vault_id
         JOIN keeps ON keeps.id = vaultkeeps.keep_id
         JOIN accounts ON accounts.id = vaultkeeps.creator_id
         WHERE vaultkeeps.id = LAST_INSERT_ID()
        ;";

        VaultKeep vaultkeep = _db.Query(sql, (VaultKeep vaultkeep, Vault vault, Keep keep, Creator creator) =>
        {
            vaultkeep.VaultId = vault.Id;
            vaultkeep.KeepId = keep.Id;
            vaultkeep.CreatorId = creator.Id;
            return vaultkeep;
        }, vaultKeepData).SingleOrDefault();
        return vaultkeep;
    }

    internal VaultKeep GetVaultKeepById(string vaultKeepId)
    {
        string sql = @"
        SELECT
         vaultkeeps.*,
         vaults.*,
         keeps.*,
         accounts.*
         FROM vaultkeeps
         JOIN vaults ON vaults.id = vaultkeeps.vault_id
         JOIN keeps ON keeps.id = vaultkeeps.keep_id
         JOIN accounts ON accounts.id = vaultkeeps.creator_id
         WHERE vaultkeeps.id = @vaultKeepId
        ;";
        VaultKeep vaultkeep = _db.Query<VaultKeep, Vault, Keep, Creator, VaultKeep>(sql, (vaultkeep, vault, keep, creator) =>
        {
            vaultkeep.VaultId = vault.Id;
            vaultkeep.KeepId = keep.Id;
            vaultkeep.CreatorId = creator.Id;
            return vaultkeep;
        }, new { vaultKeepId }).SingleOrDefault();
        return vaultkeep;
    }

    internal void RemoveKeepFromVault(string vaultKeepId, string userId)
    {
        string sql = @"
        DELETE FROM vaultkeeps
        WHERE id = @vaultKeepId AND creator_id = @userId LIMIT 1
        ;";
        int rowsAffected = _db.Execute(sql, new { vaultKeepId, userId });

        if (rowsAffected != 1)
        {
            throw new Exception("Multiple rows of data may have been messed with!");
        }


    }
}
