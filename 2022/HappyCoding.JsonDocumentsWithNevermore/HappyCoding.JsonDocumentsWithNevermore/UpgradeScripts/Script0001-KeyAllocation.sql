CREATE TABLE [TestingDocuments] (
  [Id] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [PK_TestingDocument_Id] PRIMARY KEY CLUSTERED, 

  -- Spalten für Queries
  [Value1] NVARCHAR(30) NOT NULL, 
  [Value2] NVARCHAR(30) NOT NULL, 
  [Value3] INT NOT NULL,

  -- Spalte für die komprimierten Json-Dokumente
  [JSONBlob] VARBINARY(MAX) NOT NULL
)