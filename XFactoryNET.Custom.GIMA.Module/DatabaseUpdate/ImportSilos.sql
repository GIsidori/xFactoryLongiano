
/****** Object:  StoredProcedure [dbo].[ImportaSilos]    Script Date: 08/13/2014 14:43:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImportaSilos]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ImportaSilos]
GO

/****** Object:  StoredProcedure [dbo].[ImportaSilos]    Script Date: 08/13/2014 14:43:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		ISISoft
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ImportaSilos] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    DECLARE @ObjectType int;
    SET @ObjectType = (SELECT XPObjectType.OID FROM XPObjectType WHERE XPObjectType.TypeName = 'XFactoryNET.Module.BusinessObjects.Silos');
	IF (@ObjectType IS NULL) 
    BEGIN
		INSERT INTO XPObjectType (AssemblyName,TypeName) VALUES('XFactoryNET.Module','XFactoryNET.Module.BusinessObjects.Silos')
		SET @ObjectType = @@IDENTITY;
    END
	UPDATE Lotto SET Lotto.Apparato = NULL WHERE Lotto.Apparato IN (SELECT Codice FROM Silos);
	DELETE FROM Percorso WHERE ApparatoFrom IN (SELECT Codice FROM Silos);
	DELETE FROM Percorso WHERE ApparatoTo IN (SELECT Codice FROM Silos);

    DELETE FROM GruppoGruppi_ApparatoApparati;
    DELETE FROM Gruppo;
	
	DELETE FROM Silos;
	DELETE FROM Apparato WHERE ObjectType = @ObjectType;
    
    Insert into Apparato (Codice,Descrizione,CaricoAbilitato,ScaricoAbilitato,Numero,ObjectType) SELECT 'S'+CONVERT(varchar,Name),'Silos ' + CONVERT(varchar,Name),1,1,CONVERT(varchar,Name),@ObjectType FROM T_Silos as T
    Insert into Silos (Codice,Capacità) SELECT 'S'+CONVERT(varchar,Name),T.Capacità FROM T_Silos as T
    
   
    INSERT INTO Gruppo (Descrizione) SELECT t.Descrizione FROM T_Destinazioni as T
	INSERT INTO GruppoGruppi_ApparatoApparati(Apparati,Gruppi) SELECT 'S'+CONVERT(varchar,Name),G.OID FROM T_Silos AS T INNER JOIN T_Destinazioni AS D ON T.IdDest = D.IdDest INNER JOIN Gruppo as G ON G.Descrizione = D.Descrizione;

	DECLARE @MIX nvarchar(100);
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
	INSERT INTO Percorso(ApparatoFrom,ApparatoTo,Abilitato) SELECT @MIX, 'S'+CONVERT(varchar,S.Name),1 FROM T_DestinazioniLavorazioni AS T INNER JOIN T_Silos AS S ON S.IdDest = T.IdDest WHERE T.IdLav='DOSAGGIO';
	
	INSERT INTO Percorso(ApparatoFrom,ApparatoTo,Abilitato) SELECT 'S'+CONVERT(varchar,S.IdSilos),RTRIM(S.IdApparato),1 from T_SilosRifornisce as S INNER JOIN Apparato ON S.IdApparato = Apparato.Codice

END
GO


