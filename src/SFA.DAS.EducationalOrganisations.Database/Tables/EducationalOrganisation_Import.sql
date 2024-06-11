CREATE TABLE dbo.[EducationalOrganisation_Import] (
    [Id]                uniqueidentifier	NOT NULL,
    [Name]	            nvarchar(350)	    NULL,
    [EducationalType]	nvarchar(150)	    NULL,
    [AddressLine1]		nvarchar(150)	    NULL,
    [AddressLine2]		nvarchar(150)	    NULL,
    [AddressLine3]		nvarchar(150)	    NULL,
    [Town]			    nvarchar(50)	    NULL,
    [County]			nvarchar(50)	    NULL,
    [PostCode]			nvarchar(8)		    NULL,
    [URN]	            nvarchar(100)		NULL,
    CONSTRAINT [PK_EducationalOrganisation_Import] PRIMARY KEY (Id)
)