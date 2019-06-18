SELECT * FROM dbo.Checkouts;

SELECT * FROM dbo.LibraryAssets WHERE Id = 1;

SELECT * FROM dbo.Statuses;

/* Library Assets STATUS */
SELECT la.Title, la.Id as LA_ID, s.*
FROM dbo.LibraryAssets la
INNER JOIN dbo.Statuses s ON la.StatusId = s.Id
WHERE la.Id < 10;

/* Checkouts related: libraryAssets and LibraryCards */
SELECT c.*, lc.*, p.FirstName, p.LastName
FROM dbo.Checkouts c
INNER JOIN LibraryCards lc ON lc.Id = c.LibraryCardId
INNER JOIN Patrons p ON p.LibraryCardId = lc.Id;

SELECT * FROM Holds;

/* Bug Fix */
UPDATE dbo.LibraryAssets Set StatusId = 1 Where Id = 1;

/* Reset Checkouts, Holds and CheckoutHistory*/
DELETE FROM dbo.Checkouts
DELETE FROM dbo.Holds
DELETE FROM dbo.CheckoutHistories
