CREATE TABLE dbo.[EducationalOrganisation] (
    [Id]                uniqueidentifier	NOT NULL,
    [Name]	            nvarchar(350)	    NULL,
    [EducationalType]	nvarchar(150)	    NULL,
    [AddressLine1]		nvarchar(150)	    NULL,
    [AddressLine2]		nvarchar(150)	    NOT NULL,
    [AddressLine3]		nvarchar(150)	    NULL,
    [Town]			    nvarchar(50)	    NOT NULL,
    [County]			nvarchar(50)	    NOT NULL,
    [PostCode]			nvarchar(8)		    NULL,
    [URN]	            nvarchar(100)		NULL,
    CONSTRAINT [PK_EducationalOrganisation] PRIMARY KEY (Id),
    INDEX [IX_EducationalOrganisation_Name] NONCLUSTERED(Name)
    )