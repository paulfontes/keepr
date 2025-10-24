

using System.Security.Cryptography.X509Certificates;

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
        ;";
        List<Keep> keeps = _db.Query(sql, (Keep keep, Creator creator) =>
        {
            keep.Creator = creator;
            return keep;
        }).ToList();
        return keeps;
    }
}