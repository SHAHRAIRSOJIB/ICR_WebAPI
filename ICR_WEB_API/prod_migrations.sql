IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250303222112_InitDb'
)
BEGIN
    CREATE TABLE [Questions] (
        [Id] int NOT NULL IDENTITY,
        [Text] nvarchar(max) NOT NULL,
        [IsShowable] bit NOT NULL,
        [SortOrder] real NOT NULL,
        [Type] int NOT NULL,
        CONSTRAINT [PK_Questions] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250303222112_InitDb'
)
BEGIN
    CREATE TABLE [Users] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [Password] nvarchar(max) NOT NULL,
        [UserType] int NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250303222112_InitDb'
)
BEGIN
    CREATE TABLE [Options] (
        [Id] int NOT NULL IDENTITY,
        [OptionText] nvarchar(max) NOT NULL,
        [QuestionId] int NOT NULL,
        CONSTRAINT [PK_Options] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Options_Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [Questions] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250303222112_InitDb'
)
BEGIN
    CREATE TABLE [RatingScaleItems] (
        [Id] int NOT NULL IDENTITY,
        [ItemText] nvarchar(max) NOT NULL,
        [QuestionId] int NOT NULL,
        CONSTRAINT [PK_RatingScaleItems] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_RatingScaleItems_Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [Questions] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250303222112_InitDb'
)
BEGIN
    CREATE TABLE [Responses] (
        [Id] int NOT NULL IDENTITY,
        [SubmissionDate] datetime2 NOT NULL,
        [ShopName] nvarchar(max) NOT NULL,
        [OwnerName] nvarchar(max) NOT NULL,
        [DistrictName] nvarchar(max) NOT NULL,
        [StreetName] nvarchar(max) NOT NULL,
        [UnifiedLicenseNumber] nvarchar(max) NOT NULL,
        [LicenseIssueDateLabel] nvarchar(max) NOT NULL,
        [OwnerIDNumber] nvarchar(max) NOT NULL,
        [AIESECActivity] nvarchar(max) NOT NULL,
        [Municipality] nvarchar(max) NOT NULL,
        [FullAddress] nvarchar(max) NOT NULL,
        [ImageBase64] nvarchar(max) NOT NULL,
        [IsSubmited] bit NOT NULL,
        [UserId] int NOT NULL,
        CONSTRAINT [PK_Responses] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Responses_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250303222112_InitDb'
)
BEGIN
    CREATE TABLE [Answers] (
        [Id] int NOT NULL IDENTITY,
        [ResponseId] int NOT NULL,
        [QuestionId] int NOT NULL,
        [SelectedOptionId] int NULL,
        [RatingItemId] int NULL,
        [RatingValue] int NULL,
        [TextResponse] nvarchar(max) NULL,
        CONSTRAINT [PK_Answers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Answers_Options_SelectedOptionId] FOREIGN KEY ([SelectedOptionId]) REFERENCES [Options] ([Id]),
        CONSTRAINT [FK_Answers_Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [Questions] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Answers_RatingScaleItems_RatingItemId] FOREIGN KEY ([RatingItemId]) REFERENCES [RatingScaleItems] ([Id]),
        CONSTRAINT [FK_Answers_Responses_ResponseId] FOREIGN KEY ([ResponseId]) REFERENCES [Responses] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250303222112_InitDb'
)
BEGIN
    CREATE INDEX [IX_Answers_QuestionId] ON [Answers] ([QuestionId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250303222112_InitDb'
)
BEGIN
    CREATE INDEX [IX_Answers_RatingItemId] ON [Answers] ([RatingItemId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250303222112_InitDb'
)
BEGIN
    CREATE INDEX [IX_Answers_ResponseId] ON [Answers] ([ResponseId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250303222112_InitDb'
)
BEGIN
    CREATE INDEX [IX_Answers_SelectedOptionId] ON [Answers] ([SelectedOptionId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250303222112_InitDb'
)
BEGIN
    CREATE INDEX [IX_Options_QuestionId] ON [Options] ([QuestionId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250303222112_InitDb'
)
BEGIN
    CREATE INDEX [IX_RatingScaleItems_QuestionId] ON [RatingScaleItems] ([QuestionId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250303222112_InitDb'
)
BEGIN
    CREATE INDEX [IX_Responses_UserId] ON [Responses] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250303222112_InitDb'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250303222112_InitDb', N'8.0.0');
END;
GO

COMMIT;
GO

