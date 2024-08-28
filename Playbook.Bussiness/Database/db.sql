CREATE TABLE ObjectType
(
    ObjectTypeId Int32,            -- Integer ID, could also be Int64 depending on your needs
    Name String,                   -- String column for the name
    Label String,                  -- String column for the label
    IsDeleted Bool,               -- Boolean-like column (0 for false, 1 for true)
    CreatedAt DateTime,            -- DateTime column for creation timestamp
    UpdatedAt DateTime,            -- DateTime column for last update timestamp
    PRIMARY KEY (ObjectTypeId)     -- Primary key constraint on ObjectTypeId
) 
ENGINE = MergeTree()
ORDER BY (ObjectTypeId);


INSERT INTO ObjectType (ObjectTypeId, Name, Label, IsDeleted, CreatedAt, UpdatedAt) 
VALUES 
(
    1,                      -- Replace with your desired integer ID
    'Leads',          -- Replace with your desired name
    'Leads',         -- Replace with your desired label
    0,                      -- Boolean value (0 for false, 1 for true)
    now(),                  -- Current DateTime for CreatedAt
    now()                   -- Current DateTime for UpdatedAt
);

INSERT INTO ObjectType (ObjectTypeId, Name, Label, IsDeleted, CreatedAt, UpdatedAt) 
VALUES 
(
    2,                      -- Replace with your desired integer ID
    'Opportunities',          -- Replace with your desired name
    'Opportunities',         -- Replace with your desired label
    0,                      -- Boolean value (0 for false, 1 for true)
    now(),                  -- Current DateTime for CreatedAt
    now()                   -- Current DateTime for UpdatedAt
);


INSERT INTO ObjectType (ObjectTypeId, Name, Label, IsDeleted, CreatedAt, UpdatedAt) 
VALUES 
(
    3,                      -- Replace with your desired integer ID
    'CustomerSuccess',          -- Replace with your desired name
    'Customer Success',         -- Replace with your desired label
    0,                      -- Boolean value (0 for false, 1 for true)
    now(),                  -- Current DateTime for CreatedAt
    now()                   -- Current DateTime for UpdatedAt
);

INSERT INTO ObjectType (ObjectTypeId, Name, Label, IsDeleted, CreatedAt, UpdatedAt) 
VALUES 
(
    4,                      -- Replace with your desired integer ID
    'Accounts',          -- Replace with your desired name
    'Accounts',         -- Replace with your desired label
    0,                      -- Boolean value (0 for false, 1 for true)
    now(),                  -- Current DateTime for CreatedAt
    now()                   -- Current DateTime for UpdatedAt
);

CREATE TABLE data_app.PlaybookObject
(
    `Id` UUID,
    `ObjectTypeId` Int32,
    `Name` String,
    `Description` String,
    `Category` String,
    `IsDeleted` Bool,
    `CreatedAt` DateTime,
    `UpdatedAt` DateTime
)
ENGINE = ReplicatedMergeTree('/clickhouse/tables/{uuid}/{shard}', '{replica}')
ORDER BY tuple()
SETTINGS index_granularity = 8192

