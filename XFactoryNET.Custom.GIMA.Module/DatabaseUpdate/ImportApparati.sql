/****** Object:  StoredProcedure [dbo].[ImportaApparati]    Script Date: 10/25/2014 16:48:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImportaApparati]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ImportaApparati]
GO

USE [XFactoryNET]
GO

/****** Object:  StoredProcedure [dbo].[ImportaApparati]    Script Date: 10/25/2014 16:48:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		ISISoft
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ImportaApparati] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Presse
    DECLARE @ObjectType int;
    SET @ObjectType = (SELECT XPObjectType.OID FROM XPObjectType WHERE XPObjectType.TypeName = 'XFactoryNET.Module.BusinessObjects.Pressa');
	IF (@ObjectType IS NULL) 
    BEGIN
		INSERT INTO XPObjectType (AssemblyName,TypeName) VALUES('XFactoryNET.Module','XFactoryNET.Module.BusinessObjects.Pressa')
		SET @ObjectType = @@IDENTITY;
    END
    
	UPDATE Lotto SET Lotto.Apparato = NULL WHERE Lotto.Apparato IN (SELECT Codice FROM Pressa);
	UPDATE Percorso SET ApparatoFrom = NULL WHERE ApparatoFrom IN (SELECT Codice FROM Pressa);
	UPDATE Percorso SET ApparatoTo = NULL WHERE ApparatoTo IN (SELECT Codice FROM Pressa);
	
	DELETE FROM Pressa;
	DELETE FROM Apparato WHERE ObjectType = @ObjectType;

    Insert into Apparato (Codice,Descrizione,CaricoAbilitato,ScaricoAbilitato,Numero,ObjectType) SELECT Name,'Pressa ' + cast(Name as varchar),1,1,CONVERT(int,SUBSTRING(T.Name,3,1)),@ObjectType FROM T_Apparati  as T WHERE T.IDLAV = 'PELLET'
	INSERT INTO Pressa (Codice) SELECT Name FROM T_Apparati AS T WHERE T.IDLAV = 'PELLET';

	-- Insaccatrici
    SET @ObjectType = (SELECT XPObjectType.OID FROM XPObjectType WHERE XPObjectType.TypeName = 'XFactoryNET.Module.BusinessObjects.Insaccatrice');
	IF (@ObjectType IS NULL) 
    BEGIN
		INSERT INTO XPObjectType (AssemblyName,TypeName) VALUES('XFactoryNET.Module','XFactoryNET.Module.BusinessObjects.Insaccatrice')
		SET @ObjectType = @@IDENTITY;
    END
    
	UPDATE Lotto SET Lotto.Apparato = NULL WHERE Lotto.Apparato IN (SELECT Codice FROM Insaccatrice);
	UPDATE Percorso SET ApparatoFrom = NULL WHERE ApparatoFrom IN (SELECT Codice FROM Insaccatrice);
	UPDATE Percorso SET ApparatoTo = NULL WHERE ApparatoTo IN (SELECT Codice FROM Insaccatrice);
	
	DELETE FROM Insaccatrice;
	DELETE FROM Apparato WHERE ObjectType = @ObjectType;

    Insert into Apparato (Codice,Descrizione,CaricoAbilitato,ScaricoAbilitato,Numero,ObjectType) VALUES ('INSACC','Insaccatrice 1',1,1,1,@ObjectType);
	INSERT INTO Insaccatrice (Codice) VALUES ('INSACC');


	DECLARE cur CURSOR FOR SELECT Codice FROM Pressa;
	DECLARE @codiceApp nvarchar(100);
	OPEN cur;
	FETCH NEXT FROM cur INTO @codiceApp
	WHILE (@@FETCH_STATUS = 0)
	BEGIN
		INSERT INTO Percorso(ApparatoFrom,ApparatoTo,Abilitato) SELECT @codiceApp, 'S'+CONVERT(varchar,S.Name),1 FROM T_DestinazioniLavorazioni AS T INNER JOIN T_Silos AS S ON S.IdDest = T.IdDest WHERE T.IdLav='PELLET';
		FETCH NEXT FROM cur INTO @codiceApp
	END
	CLOSE cur;
	DEALLOCATE cur;
	
	INSERT INTO Percorso(ApparatoFrom,ApparatoTo,Abilitato) SELECT     'S' + CONVERT(varchar, S.IdSilos) AS Expr1, Apparato.Codice, 1 AS Expr3
FROM         T_SilosRifornisce AS S INNER JOIN
                      Apparato ON S.IdApparato = Apparato.Codice WHERE Apparato.Lavorazione = 'INSACCO'


END

GO

/****** Object:  StoredProcedure [dbo].[ImportaApparati]    Script Date: 10/26/2014 23:14:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImportaApparati]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		ISISoft
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ImportaApparati] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Presse
    DECLARE @ObjectType int;
    SET @ObjectType = (SELECT XPObjectType.OID FROM XPObjectType WHERE XPObjectType.TypeName = ''XFactoryNET.Module.BusinessObjects.Pressa'');
	IF (@ObjectType IS NULL) 
    BEGIN
		INSERT INTO XPObjectType (AssemblyName,TypeName) VALUES(''XFactoryNET.Module'',''XFactoryNET.Module.BusinessObjects.Pressa'')
		SET @ObjectType = @@IDENTITY;
    END
    
	UPDATE Lotto SET Lotto.Apparato = NULL WHERE Lotto.Apparato IN (SELECT Codice FROM Pressa);
	UPDATE Percorso SET ApparatoFrom = NULL WHERE ApparatoFrom IN (SELECT Codice FROM Pressa);
	UPDATE Percorso SET ApparatoTo = NULL WHERE ApparatoTo IN (SELECT Codice FROM Pressa);
	
	DELETE FROM Pressa;
	DELETE FROM Apparato WHERE ObjectType = @ObjectType;

    Insert into Apparato (Codice,Descrizione,CaricoAbilitato,ScaricoAbilitato,Numero,ObjectType) SELECT Name,''Pressa '' + cast(Name as varchar),1,1,CONVERT(int,SUBSTRING(T.Name,3,1)),@ObjectType FROM T_Apparati  as T WHERE T.IDLAV = ''PELLET''
	INSERT INTO Pressa (Codice) SELECT Name FROM T_Apparati AS T WHERE T.IDLAV = ''PELLET'';

	-- Insaccatrici
    SET @ObjectType = (SELECT XPObjectType.OID FROM XPObjectType WHERE XPObjectType.TypeName = ''XFactoryNET.Module.BusinessObjects.Insaccatrice'');
	IF (@ObjectType IS NULL) 
    BEGIN
		INSERT INTO XPObjectType (AssemblyName,TypeName) VALUES(''XFactoryNET.Module'',''XFactoryNET.Module.BusinessObjects.Insaccatrice'')
		SET @ObjectType = @@IDENTITY;
    END
    
	UPDATE Lotto SET Lotto.Apparato = NULL WHERE Lotto.Apparato IN (SELECT Codice FROM Insaccatrice);
	UPDATE Percorso SET ApparatoFrom = NULL WHERE ApparatoFrom IN (SELECT Codice FROM Insaccatrice);
	UPDATE Percorso SET ApparatoTo = NULL WHERE ApparatoTo IN (SELECT Codice FROM Insaccatrice);
	
	DELETE FROM Insaccatrice;
	DELETE FROM Apparato WHERE ObjectType = @ObjectType;

    Insert into Apparato (Codice,Descrizione,CaricoAbilitato,ScaricoAbilitato,Numero,ObjectType) VALUES (''INSACC'',''Insaccatrice 1'',1,1,1,@ObjectType);
	INSERT INTO Insaccatrice (Codice) VALUES (''INSACC'');


	DECLARE cur CURSOR FOR SELECT Codice FROM Pressa;
	DECLARE @codiceApp nvarchar(100);
	OPEN cur;
	FETCH NEXT FROM cur INTO @codiceApp
	WHILE (@@FETCH_STATUS = 0)
	BEGIN
		INSERT INTO Percorso(ApparatoFrom,ApparatoTo,Abilitato) SELECT @codiceApp, ''S''+CONVERT(varchar,S.Name),1 FROM T_DestinazioniLavorazioni AS T INNER JOIN T_Silos AS S ON S.IdDest = T.IdDest WHERE T.IdLav=''PELLET'';
		FETCH NEXT FROM cur INTO @codiceApp
	END
	CLOSE cur;
	DEALLOCATE cur;
	
	INSERT INTO Percorso(ApparatoFrom,ApparatoTo,Abilitato) SELECT     ''S'' + CONVERT(varchar, S.IdSilos) AS Expr1, Apparato.Codice, 1 AS Expr3
FROM         T_SilosRifornisce AS S INNER JOIN
                      Apparato ON S.IdApparato = Apparato.Codice WHERE Apparato.Lavorazione = ''INSACCO''


END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ImportaBilance]    Script Date: 10/26/2014 23:14:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImportaBilance]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
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
    SET @ObjectType = (SELECT XPObjectType.OID FROM XPObjectType WHERE XPObjectType.TypeName = ''XFactoryNET.Module.BusinessObjects.Bilancia'');
	IF (@ObjectType IS NULL) 
    BEGIN
		INSERT INTO XPObjectType (AssemblyName,TypeName) VALUES(''XFactoryNET.Module'',''XFactoryNET.Module.BusinessObjects.Bilancia'')
		SET @ObjectType = @@IDENTITY;
    END

	UPDATE Lotto SET Lotto.Apparato = NULL WHERE Lotto.Apparato IN (SELECT Codice FROM Bilancia);
	UPDATE Percorso SET ApparatoFrom = NULL WHERE ApparatoFrom IN (SELECT Codice FROM Bilancia);
	UPDATE Percorso SET ApparatoTo = NULL WHERE ApparatoTo IN (SELECT Codice FROM Bilancia);
	
	DELETE FROM Bilancia;
	DELETE FROM Apparato WHERE ObjectType = @ObjectType;

    Insert into Apparato (Codice,Descrizione,CaricoAbilitato,ScaricoAbilitato,Numero,ObjectType) SELECT Name,''Bilancia '' + cast(Name as varchar),1,1,CONVERT(int,SUBSTRING(T.Name,2,1)),@ObjectType FROM EasyDB.dbo.T_Bilance as T
	INSERT INTO Bilancia (Codice,FondoScala,kMult) SELECT Name,T.FondoScala,t.KCorr FROM EasyDB.dbo.T_Bilance AS T;
	
	DECLARE @MIX nvarchar(12);
	SET @MIX = (SELECT TOP 1 Codice FROM Mixer GROUP BY Codice);
	IF (@MIX IS NULL)
	BEGIN
		SET @MIX = ''MIX1'';
		DECLARE @ObjectTypeMix int;
		SET @ObjectTypeMix = (SELECT XPObjectType.OID FROM XPObjectType WHERE XPObjectType.TypeName = ''XFactoryNET.Module.BusinessObjects.Mixer'');
		IF (@ObjectTypeMix IS NULL) 
		BEGIN
			INSERT INTO XPObjectType (AssemblyName,TypeName) VALUES(''XFactoryNET.Module'',''XFactoryNET.Module.BusinessObjects.Mixer'')
			SET @ObjectTypeMix = @@IDENTITY;
		END
		INSERT INTO Apparato(Codice,Descrizione,Lavorazione,CaricoAbilitato,ScaricoAbilitato,Numero,ObjectType) VALUES(@MIX,''Miscelatore'',''Dosaggio'',1,1,1,@ObjectTypeMix);
		INSERT INTO Mixer(Codice) VALUES(@MIX);
	END

	INSERT INTO Percorso (ApparatoFrom,ApparatoTo,Abilitato) SELECT Name,@MIX,1 FROM EasyDB.dbo.T_Bilance;
	
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ImportaSilos]    Script Date: 10/26/2014 23:14:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImportaSilos]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
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
    SET @ObjectType = (SELECT XPObjectType.OID FROM XPObjectType WHERE XPObjectType.TypeName = ''XFactoryNET.Module.BusinessObjects.Silos'');
	IF (@ObjectType IS NULL) 
    BEGIN
		INSERT INTO XPObjectType (AssemblyName,TypeName) VALUES(''XFactoryNET.Module'',''XFactoryNET.Module.BusinessObjects.Silos'')
		SET @ObjectType = @@IDENTITY;
    END
	UPDATE Lotto SET Lotto.Apparato = NULL WHERE Lotto.Apparato IN (SELECT Codice FROM Silos);
	DELETE FROM Percorso WHERE ApparatoFrom IN (SELECT Codice FROM Silos);
	DELETE FROM Percorso WHERE ApparatoTo IN (SELECT Codice FROM Silos);

    DELETE FROM GruppoGruppi_ApparatoApparati;
    DELETE FROM Gruppo;
	
	DELETE FROM Silos;
	DELETE FROM Apparato WHERE ObjectType = @ObjectType;
    
    Insert into Apparato (Codice,Descrizione,CaricoAbilitato,ScaricoAbilitato,Numero,ObjectType) SELECT ''S''+CONVERT(varchar,Name),''Silos '' + CONVERT(varchar,Name),1,1,CONVERT(varchar,Name),@ObjectType FROM T_Silos as T
    Insert into Silos (Codice,Capacità) SELECT ''S''+CONVERT(varchar,Name),T.Capacità FROM T_Silos as T
    
   
    INSERT INTO Gruppo (Descrizione) SELECT t.Descrizione FROM T_Destinazioni as T
	INSERT INTO GruppoGruppi_ApparatoApparati(Apparati,Gruppi) SELECT ''S''+CONVERT(varchar,Name),G.OID FROM T_Silos AS T INNER JOIN T_Destinazioni AS D ON T.IdDest = D.IdDest INNER JOIN Gruppo as G ON G.Descrizione = D.Descrizione;

	DECLARE @MIX nvarchar(100);
	SET @MIX = (SELECT TOP 1 Codice FROM Mixer GROUP BY Codice);
	IF (@MIX IS NULL)
	BEGIN
		SET @MIX = ''MIX1'';
		DECLARE @ObjectTypeMix int;
		SET @ObjectTypeMix = (SELECT XPObjectType.OID FROM XPObjectType WHERE XPObjectType.TypeName = ''XFactoryNET.Module.BusinessObjects.Mixer'');
		IF (@ObjectTypeMix IS NULL) 
		BEGIN
			INSERT INTO XPObjectType (AssemblyName,TypeName) VALUES(''XFactoryNET.Module'',''XFactoryNET.Module.BusinessObjects.Mixer'')
			SET @ObjectTypeMix = @@IDENTITY;
		END
		INSERT INTO Apparato(Codice,Descrizione,Lavorazione,CaricoAbilitato,ScaricoAbilitato,Numero,ObjectType) VALUES(@MIX,''Miscelatore'',''Dosaggio'',1,1,1,@ObjectTypeMix);
		INSERT INTO Mixer(Codice) VALUES(@MIX);
	END
	INSERT INTO Percorso(ApparatoFrom,ApparatoTo,Abilitato) SELECT @MIX, ''S''+CONVERT(varchar,S.Name),1 FROM T_DestinazioniLavorazioni AS T INNER JOIN T_Silos AS S ON S.IdDest = T.IdDest WHERE T.IdLav=''DOSAGGIO'';
	
	INSERT INTO Percorso(ApparatoFrom,ApparatoTo,Abilitato) SELECT ''S''+CONVERT(varchar,S.IdSilos),RTRIM(S.IdApparato),1 from T_SilosRifornisce as S INNER JOIN Apparato ON S.IdApparato = Apparato.Codice

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ImportaSostituzioni]    Script Date: 10/26/2014 23:14:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImportaSostituzioni]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		ISISoft
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ImportaSostituzioni] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    DELETE FROM Componente WHERE FormulaIngredienti IN (SELECT IDSost FROM [OLDSQL].[EasyDB].dbo.T_Sostituti);
    DELETE FROM FormulaFormule_ArticoloArticoli WHERE Formule IN (SELECT IDSost FROM [OLDSQL].[EasyDB].dbo.T_Sostituti);
    DELETE FROM Formula WHERE Codice IN (SELECT IdSost FROM [OLDSQL].[EasyDB].dbo.T_Sostituti);

    DECLARE @ObjectType int;
    SET @ObjectType = (SELECT XPObjectType.OID FROM XPObjectType WHERE XPObjectType.TypeName = ''XFactoryNET.Module.BusinessObjects.Articolo'');

    INSERT INTO BaseArticolo (Codice,Descrizione,ObjectType) (SELECT DISTINCT RTRIM(D.IdIngrediente), A.Descrizione,@ObjectType
FROM         [OLDSQL].[EasyDB].dbo.T_DettSostituti AS D INNER JOIN
                      [OLDSQL].[EasyDB].dbo.T_Articoli AS A ON RTRIM(D.IdIngrediente) = A.IdArticolo LEFT OUTER JOIN
                      BaseArticolo AS B ON RTRIM(D.IdIngrediente) = B.Codice
WHERE     (B.Codice IS NULL))
    INSERT INTO Articolo (Codice) 
    (SELECT DISTINCT RTRIM(D.IdIngrediente)
		FROM         [OLDSQL].[EasyDB].dbo.T_DettSostituti AS D INNER JOIN
                      [OLDSQL].[EasyDB].dbo.T_Articoli AS A ON RTRIM(D.IdIngrediente) = A.IdArticolo LEFT OUTER JOIN
                      Articolo AS B ON RTRIM(D.IdIngrediente) = B.Codice
		WHERE     (B.Codice IS NULL))

    
	INSERT INTO Formula (Codice,Descrizione,TipoFormula) (SELECT IdSost,Descrizione,1 FROM [OLDSQL].[EasyDB].dbo.T_Sostituti T1)
	INSERT INTO Componente (FormulaIngredienti,Articolo,Percentuale) SELECT     IdSost, IdIngrediente, Perc
		FROM         [OLDSQL].[EasyDB].dbo.T_DettSostituti
	INSERT INTO [FormulaFormule_ArticoloArticoli] (Formule,Articoli) SELECT [IdSost]
      ,[IdMateriale]
  FROM [OLDSQL].[Easydb].[dbo].[T_SostitutiMateriali] S INNER JOIN Articolo ON S.IdMateriale = Articolo.Codice
END
' 
END
GO
