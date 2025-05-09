CREATE TRIGGER FeedbacksTrigger
ON Feedbacks
AFTER INSERT, DELETE, UPDATE
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @r INT;
	DECLARE @d INT;
	DECLARE @p INT;
	DECLARE @m INT;
	DECLARE @id UNIQUEIDENTIFIER;
	SET @r = ( SELECT COALESCE( SUM(Rate), 0 ) FROM inserted ) ;
	SET @d = ( SELECT COALESCE( SUM(Rate), 0 ) FROM deleted  ) ;
	SET @p = ( SELECT COUNT(Rate) FROM inserted ) ;
	SET @m = ( SELECT COUNT(Rate) FROM deleted  ) ;
	SET @id = COALESCE( (SELECT RealtyId FROM inserted), (SELECT RealtyId FROM deleted) );

	IF( EXISTS( SELECT Id FROM AccRates WHERE RealtyId = @id) )

		UPDATE AccRates
		SET 
			AvgRate = (AvgRate * CountRate + @r - @d) / (CountRate + @p - @m),
			CountRate = CountRate + @p - @m,
			LastRatedAt = CURRENT_TIMESTAMP
		WHERE
			RealtyId = @id;

	ELSE 

		INSERT INTO AccRates VALUES
			( NEWID(), @id, @r, @p, CURRENT_TIMESTAMP ) ;
END;