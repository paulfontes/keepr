-- Active: 1760459935767@@mysql.codeworksacademy.com@3306@intuitive_elephant_ef9f_db
CREATE TABLE IF NOT EXISTS accounts (
    id VARCHAR(255) NOT NULL PRIMARY KEY COMMENT 'primary key',
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name VARCHAR(255) COMMENT 'User Name',
    email VARCHAR(255) UNIQUE COMMENT 'User Email',
    picture VARCHAR(255) COMMENT 'User Picture',
    cover_img VARCHAR(1000) NOT NULL DEFAULT 'https://images.unsplash.com/photo-1506744038136-46273834b3fb?ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8Y292ZXIlMjBpbWFnZXN8ZW58MHx8MHx8fDA%3D&auto=format&fit=crop&q=60&w=500'
) default charset utf8mb4 COMMENT '';

CREATE TABLE keeps (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name VARCHAR(255) NOT NULL,
    description VARCHAR(1000) NOT NULL,
    img VARCHAR(1000) NOT NULL,
    views INT NOT NULL,
    creator_id VARCHAR(255) NOT NULL,
    Foreign Key (creator_id) REFERENCES accounts (id)
)

CREATE TABLE vaults (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name VARCHAR(255) NOT NULL,
    description VARCHAR(1000) NOT NULL,
    img VARCHAR(1000) NOT NULL,
    is_private BOOLEAN NOT NULL,
    creator_id VARCHAR(255) NOT NULL,
    Foreign Key (creator_id) REFERENCES accounts (id)
)

CREATE TABLE vaultkeeps (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    keep_id INT NOT NULL,
    vault_id INT NOT NULL,
    creator_id VARCHAR(255) NOT NULL,
    Foreign Key (keep_id) REFERENCES keeps (id),
    Foreign Key (vault_id) REFERENCES vaults (id),
    Foreign Key (creator_id) REFERENCES accounts (id)
)

INSERT INTO
    keeps (
        name,
        description,
        img,
        views,
        creator_id
    )
VALUES (
        "Manhattan Beach",
        "This beach is known for its white, super soft sand! This is a beach everyone must go to!",
        "https://images.unsplash.com/photo-1530131272429-0ad33e79aeb8?ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8M3x8bWFuaGF0dGFuJTIwYmVhY2h8ZW58MHx8MHx8fDA%3D&auto=format&fit=crop&q=60&w=500",
        50,
        "68f856fe03bda77bdf77448e"
    )

INSERT INTO
    vaults (
        name,
        description,
        img,
        is_private,
        creator_id
    )
VALUES (
        "Favorite Games",
        "My favorite games I like to play!",
        "https://images.unsplash.com/photo-1530131272429-0ad33e79aeb8?ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8M3x8bWFuaGF0dGFuJTIwYmVhY2h8ZW58MHx8MHx8fDA%3D&auto=format&fit=crop&q=60&w=500",
        true,
        "68f856fe03bda77bdf77448e"
    )

DROP TABLE keeps

DROP TABLE vaults

DROP TABLE accounts

DROP TABLE vaultkeeps

SELECT * FROM keeps

SELECT * FROM vaults

SELECT * FROM accounts

SELECT * FROM vaultkeeps

UPDATE keeps SET name = 'Cool Beach' WHERE id = 1