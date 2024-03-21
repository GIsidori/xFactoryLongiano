
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImportaBilance]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ImportaBilance]
GO

-- =============================================
-- Author:		ISISoft
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ImportaBilance] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	    DECLARE @ObjectType int;
    SET @ObjectType = (SELECT XPObjectType.OID FROM XPObjectType WHERE XPObjectType.TypeName = 'XFactoryNET.Module.BusinessObjects.Bilancia');
	IF (@ObjectType IS NULL) 
    BEGIN
		INSERT INTO XPObjectType (AssemblyName,TypeName) VALUES('XFactoryNET.Module','XFactoryNET.Module.BusinessObjects.Bilancia')
		SET @ObjectType = @@IDENTITY;
    END

	UPDATE Lotto SET Lotto.Apparato = NULL WHERE Lotto.Apparato IN (SELECT Codice FROM Bilancia);
	UPDATE Percorso SET ApparatoFrom = NULL WHERE ApparatoFrom IN (SELECT Codice FROM Bilancia);
	UPDATE Percorso SET ApparatoTo = NULL WHERE ApparatoTo IN (SELECT Codice FROM Bilancia);
	
	DELETE FROM Bilancia;
	DELETE FROM Apparato WHERE ObjectType = @ObjectType;

    Insert into Apparato (Codice,Descrizione,CaricoAbilitato,ScaricoAbilitato,Numero,ObjectType) SELECT Name,'Bilancia ' + cast(Name as varchar),1,1,CONVERT(int,SUBSTRING(T.Name,2,1)),@ObjectType FROM EasyDB.dbo.T_Bilance as T
	INSERT INTO Bilancia (Codice,FondoScala,kMult) SELECT Name,T.FondoScala,t.KCorr FROM EasyDB.dbo.T_Bilance AS T;
	
	DECLARE @MIX nvarchar(12);
	SET @MIX = (SELECT TOP 1 Codice FROM Mixer GROUP BY Codice);
	IF (@MIX IS NULL)
	BEGIN
		SET @MIX = 'MIX1';
		DECLARE @ObjectTypeMix int;
		SET @ObjectTypeMix = (SELECT XPObjectType.OID FROM XPObjectType WHERE XPObjectType.TypeName = 'XFactoryNET.Module.BusinessObjects.Mixer');
		IF (@ObjectTypeMix IS NULL) 
		BEGIN
			INSERT INTO XPObjectType (AssemblyName,TypeName) VALUES('XFactoryNET.Module','XFactoryNET.Module.BusinessObjects.Mixer')
			SET @ObjectTypeMix = @@IDENTITY;
		END
		INSERT INTO Apparato(Codice,Descrizione,Lavorazione,CaricoAbilitato,ScaricoAbilitato,Numero,ObjectType) VALUES(@MIX,'Miscelatore','Dosaggio',1,1,1,@ObjectTypeMix);
		INSERT INTO Mixer(Codice) VALUES(@MIX);
	END

	INSERT INTO Percorso (ApparatoFrom,ApparatoTo,Abilitato) SELECT Name,@MIX,1 FROM EasyDB.dbo.T_Bilance;
	
END

