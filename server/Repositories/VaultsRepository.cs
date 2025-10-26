

namespace keepr.Repositories;

public class VaultsRepository
{
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Vault CreateVault(Vault vaultData)
    {
        string sql = @"
          INSERT INTO
    vaults (
        name,
        description,
        img,
        is_private,
        creator_id
    )
    VALUES (
        @Name,
        @Description,
        @Img,
        @IsPrivate,
        @CreatorId
    );
    SELECT
         vaults.*,
         accounts.*
         FROM vaults
         JOIN accounts ON accounts.id = vaults.creator_id
         WHERE vaults.id = LAST_INSERT_ID()
        ;";

        Vault vault = _db.Query(sql, (Vault vault, Creator creator) =>
        {
            vault.Creator = creator;
            return vault;
        }, vaultData).SingleOrDefault();
        return vault;
    }

    // internal Vault GetVaultById(int vaultId)
    // {
    //     string sql = @"

    //     ;";
    // }
}