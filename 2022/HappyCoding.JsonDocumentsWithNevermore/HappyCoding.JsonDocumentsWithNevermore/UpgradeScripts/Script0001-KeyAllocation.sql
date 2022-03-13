CREATE TABLE [Person] (
  [Id] NVARCHAR(50) NOT NULL CONSTRAINT [PK_Person_Id] PRIMARY KEY CLUSTERED, 

  -- These are columns we might need to query against
  [FirstName] NVARCHAR(20) NOT NULL, 
  [LastName] NVARCHAR(200) NULL, 
  [Email] NVARCHAR(200) NOT NULL, 

  -- This is where we store everything else
  [JSON] NVARCHAR(MAX) NOT NULL
)
ALTER TABLE [Person] ADD CONSTRAINT [UQ_UniquePersonEmail] UNIQUE([Email])