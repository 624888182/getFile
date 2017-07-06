USE LMis
-----------------------------------------------------------------------------
--                                                                         -- 
--				stored procedure for table tbrole_DeptRoleDetail                --
--                                                                         --
-----------------------------------------------------------------------------

--IsExist procedure for table tbrole_DeptRoleDetail
IF OBJECT_ID ( 'tbrole_DeptRoleDetail_isExist_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_DeptRoleDetail_isExist_sp;
GO

CREATE PROCEDURE tbrole_DeptRoleDetail_isExist_sp
	@DeptRoleGpCode nvarchar(20)
AS
BEGIN
	DECLARE @records AS INT
	
	SELECT 
		@records = COUNT(*) 
	FROM
		tbrole_DeptRoleDetail
	WHERE
		DeptRoleGpCode = @DeptRoleGpCode
		
	RETURN @records
END
GO

--Insert procedure for table tbrole_DeptRoleDetail
IF OBJECT_ID ( 'tbrole_DeptRoleDetail_insert_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_DeptRoleDetail_insert_sp;
GO

CREATE PROCEDURE tbrole_DeptRoleDetail_insert_sp
	@DeptRoleGpCode nvarchar(20),
	@DeptCode nvarchar(20)
AS
BEGIN
	INSERT INTO tbrole_DeptRoleDetail
		(
			DeptRoleGpCode,
			DeptCode
		)
	VALUES
		(
			@DeptRoleGpCode,
			@DeptCode
		)
END
GO

--Update procedure for table tbrole_DeptRoleDetail
IF OBJECT_ID ( 'tbrole_DeptRoleDetail_update_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_DeptRoleDetail_update_sp;
GO

CREATE PROCEDURE tbrole_DeptRoleDetail_update_sp
	@DeptRoleGpCode nvarchar(20),
	@DeptCode nvarchar(20)
AS
BEGIN
	UPDATE tbrole_DeptRoleDetail
	SET
		DeptRoleGpCode = @DeptRoleGpCode,
		DeptCode = @DeptCode
	WHERE
		DeptRoleGpCode = @DeptRoleGpCode
END
GO

--Delete procedure for table tbrole_DeptRoleDetail
IF OBJECT_ID ( 'tbrole_DeptRoleDetail_delete_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_DeptRoleDetail_delete_sp;
GO

CREATE PROCEDURE tbrole_DeptRoleDetail_delete_sp
	@DeptRoleGpCode nvarchar(20)
AS
BEGIN
	DELETE FROM tbrole_DeptRoleDetail
	WHERE
		DeptRoleGpCode = @DeptRoleGpCode
END
GO

--Get all data for table tbrole_DeptRoleDetail
IF OBJECT_ID ( 'tbrole_DeptRoleDetail_getAll_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_DeptRoleDetail_getAll_sp;
GO

CREATE PROCEDURE tbrole_DeptRoleDetail_getAll_sp

AS
BEGIN
	SELECT
		DeptRoleGpCode,
		DeptCode
	FROM 
		tbrole_DeptRoleDetail
END
GO

IF OBJECT_ID ( 'tbrole_DeptRoleDetail_get_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_DeptRoleDetail_get_sp;
GO

CREATE PROCEDURE tbrole_DeptRoleDetail_get_sp
	@DeptRoleGpCode nvarchar(20)
AS
BEGIN
	SELECT
		DeptRoleGpCode,
		DeptCode
	FROM
		tbrole_DeptRoleDetail
	WHERE
		DeptRoleGpCode = @DeptRoleGpCode
END
GO


-- 分页查找数据
IF OBJECT_ID ( 'tbrole_DeptRoleDetail_find_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_DeptRoleDetail_find_sp;
GO

CREATE PROCEDURE tbrole_DeptRoleDetail_find_sp
	@DeptRoleGpCode nvarchar(20),
	@DeptCode nvarchar(20),
	@startRowIndex INT,
	@maximumRows INT

AS
BEGIN
	IF @DeptRoleGpCode IS NOT NULL
		SET @DeptRoleGpCode = '%'+ @DeptRoleGpCode + '%'
	IF @DeptCode IS NOT NULL
		SET @DeptCode = '%'+ @DeptCode + '%'
	-- 取开始结果前的最大值。

	DECLARE @MaxRecord AS nvarchar(20)
	
	SELECT 
		@MaxRecord = MAX(DeptRoleGpCode) 
	FROM 
		(SELECT DISTINCT TOP (@startRowIndex) 
			DeptRoleGpCode
		FROM 
			tbrole_DeptRoleDetail
		WHERE
			(@DeptRoleGpCode IS NULL OR (DeptRoleGpCode LIKE @DeptRoleGpCode))  AND 
			(@DeptCode IS NULL OR (DeptCode LIKE @DeptCode)) 
		ORDER BY 
			DeptRoleGpCode ASC
		) AS T

	IF @MaxRecord IS NULL
		SET @MaxRecord = 0
		
	--取当前页中需要显示的数据
	SELECT DISTINCT TOP (@maximumRows)
		DeptRoleGpCode,
		DeptCode
	FROM 
		tbrole_DeptRoleDetail
	WHERE
		(dbo.tbrole_DeptRoleDetail.DeptRoleGpCode > @MaxRecord) AND --用于分页时使用

		(@DeptRoleGpCode IS NULL OR (DeptRoleGpCode LIKE @DeptRoleGpCode))  AND 
		(@DeptCode IS NULL OR (DeptCode LIKE @DeptCode)) 
	ORDER BY 
		dbo.tbrole_DeptRoleDetail.DeptRoleGpCode ASC
END
GO

IF OBJECT_ID ( 'tbrole_DeptRoleDetail_findCount_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_DeptRoleDetail_findCount_sp;
GO

--取符合要求的数据的总数。

CREATE PROCEDURE tbrole_DeptRoleDetail_findCount_sp
	@DeptRoleGpCode nvarchar(20),
	@DeptCode nvarchar(20),
	@startRowIndex INT,
	@maximumRows INT

AS
BEGIN
	IF @DeptRoleGpCode IS NOT NULL
		SET @DeptRoleGpCode = '%'+ @DeptRoleGpCode + '%'
	IF @DeptCode IS NOT NULL
		SET @DeptCode = '%'+ @DeptCode + '%'
	--取符合要求的数据的总数。

	DECLARE @recordCount AS INT

	SELECT 
		@recordCount = COUNT(DISTINCT DeptRoleGpCode)
	FROM 
		tbrole_DeptRoleDetail
	WHERE
		(@DeptRoleGpCode IS NULL OR (DeptRoleGpCode LIKE @DeptRoleGpCode))  AND 
		(@DeptCode IS NULL OR (DeptCode LIKE @DeptCode)) 
	return @recordCount
END
GO
-----------------------------------------------------------------------------
--                                                                         -- 
--				stored procedure for table tbrole_DeptRoleGp                --
--                                                                         --
-----------------------------------------------------------------------------

--IsExist procedure for table tbrole_DeptRoleGp
IF OBJECT_ID ( 'tbrole_DeptRoleGp_isExist_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_DeptRoleGp_isExist_sp;
GO

CREATE PROCEDURE tbrole_DeptRoleGp_isExist_sp
	@DeptRoleGpCode nvarchar(20)
AS
BEGIN
	DECLARE @records AS INT
	
	SELECT 
		@records = COUNT(*) 
	FROM
		tbrole_DeptRoleGp
	WHERE
		DeptRoleGpCode = @DeptRoleGpCode
		
	RETURN @records
END
GO

--Insert procedure for table tbrole_DeptRoleGp
IF OBJECT_ID ( 'tbrole_DeptRoleGp_insert_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_DeptRoleGp_insert_sp;
GO

CREATE PROCEDURE tbrole_DeptRoleGp_insert_sp
	@DeptRoleGpCode nvarchar(20),
	@DeptRoleGpName nvarchar(50)
AS
BEGIN
	INSERT INTO tbrole_DeptRoleGp
		(
			DeptRoleGpCode,
			DeptRoleGpName
		)
	VALUES
		(
			@DeptRoleGpCode,
			@DeptRoleGpName
		)
END
GO

--Update procedure for table tbrole_DeptRoleGp
IF OBJECT_ID ( 'tbrole_DeptRoleGp_update_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_DeptRoleGp_update_sp;
GO

CREATE PROCEDURE tbrole_DeptRoleGp_update_sp
	@DeptRoleGpCode nvarchar(20),
	@DeptRoleGpName nvarchar(50)
AS
BEGIN
	UPDATE tbrole_DeptRoleGp
	SET
		DeptRoleGpCode = @DeptRoleGpCode,
		DeptRoleGpName = @DeptRoleGpName
	WHERE
		DeptRoleGpCode = @DeptRoleGpCode
END
GO

--Delete procedure for table tbrole_DeptRoleGp
IF OBJECT_ID ( 'tbrole_DeptRoleGp_delete_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_DeptRoleGp_delete_sp;
GO

CREATE PROCEDURE tbrole_DeptRoleGp_delete_sp
	@DeptRoleGpCode nvarchar(20)
AS
BEGIN
	DELETE FROM tbrole_DeptRoleGp
	WHERE
		DeptRoleGpCode = @DeptRoleGpCode
END
GO

--Get all data for table tbrole_DeptRoleGp
IF OBJECT_ID ( 'tbrole_DeptRoleGp_getAll_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_DeptRoleGp_getAll_sp;
GO

CREATE PROCEDURE tbrole_DeptRoleGp_getAll_sp

AS
BEGIN
	SELECT
		DeptRoleGpCode,
		DeptRoleGpName
	FROM 
		tbrole_DeptRoleGp
END
GO

IF OBJECT_ID ( 'tbrole_DeptRoleGp_get_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_DeptRoleGp_get_sp;
GO

CREATE PROCEDURE tbrole_DeptRoleGp_get_sp
	@DeptRoleGpCode nvarchar(20)
AS
BEGIN
	SELECT
		DeptRoleGpCode,
		DeptRoleGpName
	FROM
		tbrole_DeptRoleGp
	WHERE
		DeptRoleGpCode = @DeptRoleGpCode
END
GO


-- 分页查找数据
IF OBJECT_ID ( 'tbrole_DeptRoleGp_find_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_DeptRoleGp_find_sp;
GO

CREATE PROCEDURE tbrole_DeptRoleGp_find_sp
	@DeptRoleGpCode nvarchar(20),
	@DeptRoleGpName nvarchar(50),
	@startRowIndex INT,
	@maximumRows INT

AS
BEGIN
	IF @DeptRoleGpCode IS NOT NULL
		SET @DeptRoleGpCode = '%'+ @DeptRoleGpCode + '%'
	IF @DeptRoleGpName IS NOT NULL
		SET @DeptRoleGpName = '%'+ @DeptRoleGpName + '%'
	-- 取开始结果前的最大值。

	DECLARE @MaxRecord AS nvarchar(20)
	
	SELECT 
		@MaxRecord = MAX(DeptRoleGpCode) 
	FROM 
		(SELECT DISTINCT TOP (@startRowIndex) 
			DeptRoleGpCode
		FROM 
			tbrole_DeptRoleGp
		WHERE
			(@DeptRoleGpCode IS NULL OR (DeptRoleGpCode LIKE @DeptRoleGpCode))  AND 
			(@DeptRoleGpName IS NULL OR (DeptRoleGpName LIKE @DeptRoleGpName)) 
		ORDER BY 
			DeptRoleGpCode ASC
		) AS T

	IF @MaxRecord IS NULL
		SET @MaxRecord = 0
		
	--取当前页中需要显示的数据
	SELECT DISTINCT TOP (@maximumRows)
		DeptRoleGpCode,
		DeptRoleGpName
	FROM 
		tbrole_DeptRoleGp
	WHERE
		(dbo.tbrole_DeptRoleGp.DeptRoleGpCode > @MaxRecord) AND --用于分页时使用

		(@DeptRoleGpCode IS NULL OR (DeptRoleGpCode LIKE @DeptRoleGpCode))  AND 
		(@DeptRoleGpName IS NULL OR (DeptRoleGpName LIKE @DeptRoleGpName)) 
	ORDER BY 
		dbo.tbrole_DeptRoleGp.DeptRoleGpCode ASC
END
GO

IF OBJECT_ID ( 'tbrole_DeptRoleGp_findCount_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_DeptRoleGp_findCount_sp;
GO

--取符合要求的数据的总数。

CREATE PROCEDURE tbrole_DeptRoleGp_findCount_sp
	@DeptRoleGpCode nvarchar(20),
	@DeptRoleGpName nvarchar(50),
	@startRowIndex INT,
	@maximumRows INT

AS
BEGIN
	IF @DeptRoleGpCode IS NOT NULL
		SET @DeptRoleGpCode = '%'+ @DeptRoleGpCode + '%'
	IF @DeptRoleGpName IS NOT NULL
		SET @DeptRoleGpName = '%'+ @DeptRoleGpName + '%'
	--取符合要求的数据的总数。

	DECLARE @recordCount AS INT

	SELECT 
		@recordCount = COUNT(DISTINCT DeptRoleGpCode)
	FROM 
		tbrole_DeptRoleGp
	WHERE
		(@DeptRoleGpCode IS NULL OR (DeptRoleGpCode LIKE @DeptRoleGpCode))  AND 
		(@DeptRoleGpName IS NULL OR (DeptRoleGpName LIKE @DeptRoleGpName)) 
	return @recordCount
END
GO
-----------------------------------------------------------------------------
--                                                                         -- 
--				stored procedure for table tbrole_Operate                --
--                                                                         --
-----------------------------------------------------------------------------

--IsExist procedure for table tbrole_Operate
IF OBJECT_ID ( 'tbrole_Operate_isExist_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_Operate_isExist_sp;
GO

CREATE PROCEDURE tbrole_Operate_isExist_sp
	@OperCode nvarchar(20)
AS
BEGIN
	DECLARE @records AS INT
	
	SELECT 
		@records = COUNT(*) 
	FROM
		tbrole_Operate
	WHERE
		OperCode = @OperCode
		
	RETURN @records
END
GO

--Insert procedure for table tbrole_Operate
IF OBJECT_ID ( 'tbrole_Operate_insert_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_Operate_insert_sp;
GO

CREATE PROCEDURE tbrole_Operate_insert_sp
	@OperCode nvarchar(20),
	@OperName nvarchar(80)
AS
BEGIN
	INSERT INTO tbrole_Operate
		(
			OperCode,
			OperName
		)
	VALUES
		(
			@OperCode,
			@OperName
		)
END
GO

--Update procedure for table tbrole_Operate
IF OBJECT_ID ( 'tbrole_Operate_update_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_Operate_update_sp;
GO

CREATE PROCEDURE tbrole_Operate_update_sp
	@OperCode nvarchar(20),
	@OperName nvarchar(80)
AS
BEGIN
	UPDATE tbrole_Operate
	SET
		OperCode = @OperCode,
		OperName = @OperName
	WHERE
		OperCode = @OperCode
END
GO

--Delete procedure for table tbrole_Operate
IF OBJECT_ID ( 'tbrole_Operate_delete_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_Operate_delete_sp;
GO

CREATE PROCEDURE tbrole_Operate_delete_sp
	@OperCode nvarchar(20)
AS
BEGIN
	DELETE FROM tbrole_Operate
	WHERE
		OperCode = @OperCode
END
GO

--Get all data for table tbrole_Operate
IF OBJECT_ID ( 'tbrole_Operate_getAll_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_Operate_getAll_sp;
GO

CREATE PROCEDURE tbrole_Operate_getAll_sp

AS
BEGIN
	SELECT
		OperCode,
		OperName
	FROM 
		tbrole_Operate
END
GO

IF OBJECT_ID ( 'tbrole_Operate_get_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_Operate_get_sp;
GO

CREATE PROCEDURE tbrole_Operate_get_sp
	@OperCode nvarchar(20)
AS
BEGIN
	SELECT
		OperCode,
		OperName
	FROM
		tbrole_Operate
	WHERE
		OperCode = @OperCode
END
GO


-- 分页查找数据
IF OBJECT_ID ( 'tbrole_Operate_find_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_Operate_find_sp;
GO

CREATE PROCEDURE tbrole_Operate_find_sp
	@OperCode nvarchar(20),
	@OperName nvarchar(80),
	@startRowIndex INT,
	@maximumRows INT

AS
BEGIN
	IF @OperCode IS NOT NULL
		SET @OperCode = '%'+ @OperCode + '%'
	IF @OperName IS NOT NULL
		SET @OperName = '%'+ @OperName + '%'
	-- 取开始结果前的最大值。

	DECLARE @MaxRecord AS nvarchar(20)
	
	SELECT 
		@MaxRecord = MAX(OperCode) 
	FROM 
		(SELECT DISTINCT TOP (@startRowIndex) 
			OperCode
		FROM 
			tbrole_Operate
		WHERE
			(@OperCode IS NULL OR (OperCode LIKE @OperCode))  AND 
			(@OperName IS NULL OR (OperName LIKE @OperName)) 
		ORDER BY 
			OperCode ASC
		) AS T

	IF @MaxRecord IS NULL
		SET @MaxRecord = 0
		
	--取当前页中需要显示的数据
	SELECT DISTINCT TOP (@maximumRows)
		OperCode,
		OperName
	FROM 
		tbrole_Operate
	WHERE
		(dbo.tbrole_Operate.OperCode > @MaxRecord) AND --用于分页时使用

		(@OperCode IS NULL OR (OperCode LIKE @OperCode))  AND 
		(@OperName IS NULL OR (OperName LIKE @OperName)) 
	ORDER BY 
		dbo.tbrole_Operate.OperCode ASC
END
GO

IF OBJECT_ID ( 'tbrole_Operate_findCount_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_Operate_findCount_sp;
GO

--取符合要求的数据的总数。

CREATE PROCEDURE tbrole_Operate_findCount_sp
	@OperCode nvarchar(20),
	@OperName nvarchar(80),
	@startRowIndex INT,
	@maximumRows INT

AS
BEGIN
	IF @OperCode IS NOT NULL
		SET @OperCode = '%'+ @OperCode + '%'
	IF @OperName IS NOT NULL
		SET @OperName = '%'+ @OperName + '%'
	--取符合要求的数据的总数。

	DECLARE @recordCount AS INT

	SELECT 
		@recordCount = COUNT(DISTINCT OperCode)
	FROM 
		tbrole_Operate
	WHERE
		(@OperCode IS NULL OR (OperCode LIKE @OperCode))  AND 
		(@OperName IS NULL OR (OperName LIKE @OperName)) 
	return @recordCount
END
GO
-----------------------------------------------------------------------------
--                                                                         -- 
--				stored procedure for table tbrole_OperRoleDetail                --
--                                                                         --
-----------------------------------------------------------------------------

--IsExist procedure for table tbrole_OperRoleDetail
IF OBJECT_ID ( 'tbrole_OperRoleDetail_isExist_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_OperRoleDetail_isExist_sp;
GO

CREATE PROCEDURE tbrole_OperRoleDetail_isExist_sp
	@OperRoleGpCode nvarchar(20),
	@ModuleCode nvarchar(20)
AS
BEGIN
	DECLARE @records AS INT
	
	SELECT 
		@records = COUNT(*) 
	FROM
		tbrole_OperRoleDetail
	WHERE
		OperRoleGpCode = @OperRoleGpCode AND 
		ModuleCode = @ModuleCode
		
	RETURN @records
END
GO

--Insert procedure for table tbrole_OperRoleDetail
IF OBJECT_ID ( 'tbrole_OperRoleDetail_insert_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_OperRoleDetail_insert_sp;
GO

CREATE PROCEDURE tbrole_OperRoleDetail_insert_sp
	@OperRoleGpCode nvarchar(20),
	@ModuleCode nvarchar(20),
	@OperCode nvarchar(80)
AS
BEGIN
	INSERT INTO tbrole_OperRoleDetail
		(
			OperRoleGpCode,
			ModuleCode,
			OperCode
		)
	VALUES
		(
			@OperRoleGpCode,
			@ModuleCode,
			@OperCode
		)
END
GO

--Update procedure for table tbrole_OperRoleDetail
IF OBJECT_ID ( 'tbrole_OperRoleDetail_update_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_OperRoleDetail_update_sp;
GO

CREATE PROCEDURE tbrole_OperRoleDetail_update_sp
	@OperRoleGpCode nvarchar(20),
	@ModuleCode nvarchar(20),
	@OperCode nvarchar(80)
AS
BEGIN
	UPDATE tbrole_OperRoleDetail
	SET
		OperRoleGpCode = @OperRoleGpCode,
		ModuleCode = @ModuleCode,
		OperCode = @OperCode
	WHERE
		OperRoleGpCode = @OperRoleGpCode AND 
		ModuleCode = @ModuleCode
END
GO

--Delete procedure for table tbrole_OperRoleDetail
IF OBJECT_ID ( 'tbrole_OperRoleDetail_delete_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_OperRoleDetail_delete_sp;
GO

CREATE PROCEDURE tbrole_OperRoleDetail_delete_sp
	@OperRoleGpCode nvarchar(20),
	@ModuleCode nvarchar(20)
AS
BEGIN
	DELETE FROM tbrole_OperRoleDetail
	WHERE
		OperRoleGpCode = @OperRoleGpCode AND 
		ModuleCode = @ModuleCode
END
GO

--Get all data for table tbrole_OperRoleDetail
IF OBJECT_ID ( 'tbrole_OperRoleDetail_getAll_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_OperRoleDetail_getAll_sp;
GO

CREATE PROCEDURE tbrole_OperRoleDetail_getAll_sp

AS
BEGIN
	SELECT
		OperRoleGpCode,
		ModuleCode,
		OperCode
	FROM 
		tbrole_OperRoleDetail
END
GO

IF OBJECT_ID ( 'tbrole_OperRoleDetail_get_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_OperRoleDetail_get_sp;
GO

CREATE PROCEDURE tbrole_OperRoleDetail_get_sp
	@OperRoleGpCode nvarchar(20),
	@ModuleCode nvarchar(20)
AS
BEGIN
	SELECT
		OperRoleGpCode,
		ModuleCode,
		OperCode
	FROM
		tbrole_OperRoleDetail
	WHERE
		OperRoleGpCode = @OperRoleGpCode AND 
		ModuleCode = @ModuleCode
END
GO


-----------------------------------------------------------------------------
--                                                                         -- 
--				stored procedure for table tbrole_OperRoleGp                --
--                                                                         --
-----------------------------------------------------------------------------

--IsExist procedure for table tbrole_OperRoleGp
IF OBJECT_ID ( 'tbrole_OperRoleGp_isExist_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_OperRoleGp_isExist_sp;
GO

CREATE PROCEDURE tbrole_OperRoleGp_isExist_sp
	@OperRoleGpCode nvarchar(20)
AS
BEGIN
	DECLARE @records AS INT
	
	SELECT 
		@records = COUNT(*) 
	FROM
		tbrole_OperRoleGp
	WHERE
		OperRoleGpCode = @OperRoleGpCode
		
	RETURN @records
END
GO

--Insert procedure for table tbrole_OperRoleGp
IF OBJECT_ID ( 'tbrole_OperRoleGp_insert_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_OperRoleGp_insert_sp;
GO

CREATE PROCEDURE tbrole_OperRoleGp_insert_sp
	@OperRoleGpCode nvarchar(20),
	@OperRoleGpName nvarchar(60)
AS
BEGIN
	INSERT INTO tbrole_OperRoleGp
		(
			OperRoleGpCode,
			OperRoleGpName
		)
	VALUES
		(
			@OperRoleGpCode,
			@OperRoleGpName
		)
END
GO

--Update procedure for table tbrole_OperRoleGp
IF OBJECT_ID ( 'tbrole_OperRoleGp_update_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_OperRoleGp_update_sp;
GO

CREATE PROCEDURE tbrole_OperRoleGp_update_sp
	@OperRoleGpCode nvarchar(20),
	@OperRoleGpName nvarchar(60)
AS
BEGIN
	UPDATE tbrole_OperRoleGp
	SET
		OperRoleGpCode = @OperRoleGpCode,
		OperRoleGpName = @OperRoleGpName
	WHERE
		OperRoleGpCode = @OperRoleGpCode
END
GO

--Delete procedure for table tbrole_OperRoleGp
IF OBJECT_ID ( 'tbrole_OperRoleGp_delete_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_OperRoleGp_delete_sp;
GO

CREATE PROCEDURE tbrole_OperRoleGp_delete_sp
	@OperRoleGpCode nvarchar(20)
AS
BEGIN
	DELETE FROM tbrole_OperRoleGp
	WHERE
		OperRoleGpCode = @OperRoleGpCode
END
GO

--Get all data for table tbrole_OperRoleGp
IF OBJECT_ID ( 'tbrole_OperRoleGp_getAll_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_OperRoleGp_getAll_sp;
GO

CREATE PROCEDURE tbrole_OperRoleGp_getAll_sp

AS
BEGIN
	SELECT
		OperRoleGpCode,
		OperRoleGpName
	FROM 
		tbrole_OperRoleGp
END
GO

IF OBJECT_ID ( 'tbrole_OperRoleGp_get_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_OperRoleGp_get_sp;
GO

CREATE PROCEDURE tbrole_OperRoleGp_get_sp
	@OperRoleGpCode nvarchar(20)
AS
BEGIN
	SELECT
		OperRoleGpCode,
		OperRoleGpName
	FROM
		tbrole_OperRoleGp
	WHERE
		OperRoleGpCode = @OperRoleGpCode
END
GO


-- 分页查找数据
IF OBJECT_ID ( 'tbrole_OperRoleGp_find_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_OperRoleGp_find_sp;
GO

CREATE PROCEDURE tbrole_OperRoleGp_find_sp
	@OperRoleGpCode nvarchar(20),
	@OperRoleGpName nvarchar(60),
	@startRowIndex INT,
	@maximumRows INT

AS
BEGIN
	IF @OperRoleGpCode IS NOT NULL
		SET @OperRoleGpCode = '%'+ @OperRoleGpCode + '%'
	IF @OperRoleGpName IS NOT NULL
		SET @OperRoleGpName = '%'+ @OperRoleGpName + '%'
	-- 取开始结果前的最大值。

	DECLARE @MaxRecord AS nvarchar(20)
	
	SELECT 
		@MaxRecord = MAX(OperRoleGpCode) 
	FROM 
		(SELECT DISTINCT TOP (@startRowIndex) 
			OperRoleGpCode
		FROM 
			tbrole_OperRoleGp
		WHERE
			(@OperRoleGpCode IS NULL OR (OperRoleGpCode LIKE @OperRoleGpCode))  AND 
			(@OperRoleGpName IS NULL OR (OperRoleGpName LIKE @OperRoleGpName)) 
		ORDER BY 
			OperRoleGpCode ASC
		) AS T

	IF @MaxRecord IS NULL
		SET @MaxRecord = 0
		
	--取当前页中需要显示的数据
	SELECT DISTINCT TOP (@maximumRows)
		OperRoleGpCode,
		OperRoleGpName
	FROM 
		tbrole_OperRoleGp
	WHERE
		(dbo.tbrole_OperRoleGp.OperRoleGpCode > @MaxRecord) AND --用于分页时使用

		(@OperRoleGpCode IS NULL OR (OperRoleGpCode LIKE @OperRoleGpCode))  AND 
		(@OperRoleGpName IS NULL OR (OperRoleGpName LIKE @OperRoleGpName)) 
	ORDER BY 
		dbo.tbrole_OperRoleGp.OperRoleGpCode ASC
END
GO

IF OBJECT_ID ( 'tbrole_OperRoleGp_findCount_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_OperRoleGp_findCount_sp;
GO

--取符合要求的数据的总数。

CREATE PROCEDURE tbrole_OperRoleGp_findCount_sp
	@OperRoleGpCode nvarchar(20),
	@OperRoleGpName nvarchar(60),
	@startRowIndex INT,
	@maximumRows INT

AS
BEGIN
	IF @OperRoleGpCode IS NOT NULL
		SET @OperRoleGpCode = '%'+ @OperRoleGpCode + '%'
	IF @OperRoleGpName IS NOT NULL
		SET @OperRoleGpName = '%'+ @OperRoleGpName + '%'
	--取符合要求的数据的总数。

	DECLARE @recordCount AS INT

	SELECT 
		@recordCount = COUNT(DISTINCT OperRoleGpCode)
	FROM 
		tbrole_OperRoleGp
	WHERE
		(@OperRoleGpCode IS NULL OR (OperRoleGpCode LIKE @OperRoleGpCode))  AND 
		(@OperRoleGpName IS NULL OR (OperRoleGpName LIKE @OperRoleGpName)) 
	return @recordCount
END
GO
-----------------------------------------------------------------------------
--                                                                         -- 
--				stored procedure for table tbrole_SysModule                --
--                                                                         --
-----------------------------------------------------------------------------

--IsExist procedure for table tbrole_SysModule
IF OBJECT_ID ( 'tbrole_SysModule_isExist_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_SysModule_isExist_sp;
GO

CREATE PROCEDURE tbrole_SysModule_isExist_sp
	@ModuleCode nvarchar(20)
AS
BEGIN
	DECLARE @records AS INT
	
	SELECT 
		@records = COUNT(*) 
	FROM
		tbrole_SysModule
	WHERE
		ModuleCode = @ModuleCode
		
	RETURN @records
END
GO

--Insert procedure for table tbrole_SysModule
IF OBJECT_ID ( 'tbrole_SysModule_insert_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_SysModule_insert_sp;
GO

CREATE PROCEDURE tbrole_SysModule_insert_sp
	@ModuleCode nvarchar(20),
	@ModuleNameCn nvarchar(80),
	@ModuleNameEn nvarchar(80),
	@SeqNo int,
	@ParentModuleCode nvarchar(20),
	@OperCodeGroup nvarchar(80),
	@URL nvarchar(80),
	@SysName nvarchar(40),
	@IsOperModule nvarchar(1),
	@IsRole nvarchar(1)
AS
BEGIN
	INSERT INTO tbrole_SysModule
		(
			ModuleCode,
			ModuleNameCn,
			ModuleNameEn,
			SeqNo,
			ParentModuleCode,
			OperCodeGroup,
			URL,
			SysName,
			IsOperModule,
			IsRole
		)
	VALUES
		(
			@ModuleCode,
			@ModuleNameCn,
			@ModuleNameEn,
			@SeqNo,
			@ParentModuleCode,
			@OperCodeGroup,
			@URL,
			@SysName,
			@IsOperModule,
			@IsRole
		)
END
GO

--Update procedure for table tbrole_SysModule
IF OBJECT_ID ( 'tbrole_SysModule_update_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_SysModule_update_sp;
GO

CREATE PROCEDURE tbrole_SysModule_update_sp
	@ModuleCode nvarchar(20),
	@ModuleNameCn nvarchar(80),
	@ModuleNameEn nvarchar(80),
	@SeqNo int,
	@ParentModuleCode nvarchar(20),
	@OperCodeGroup nvarchar(80),
	@URL nvarchar(80),
	@SysName nvarchar(40),
	@IsOperModule nvarchar(1),
	@IsRole nvarchar(1)
AS
BEGIN
	UPDATE tbrole_SysModule
	SET
		ModuleCode = @ModuleCode,
		ModuleNameCn = @ModuleNameCn,
		ModuleNameEn = @ModuleNameEn,
		SeqNo = @SeqNo,
		ParentModuleCode = @ParentModuleCode,
		OperCodeGroup = @OperCodeGroup,
		URL = @URL,
		SysName = @SysName,
		IsOperModule = @IsOperModule,
		IsRole = @IsRole
	WHERE
		ModuleCode = @ModuleCode
END
GO

--Delete procedure for table tbrole_SysModule
IF OBJECT_ID ( 'tbrole_SysModule_delete_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_SysModule_delete_sp;
GO

CREATE PROCEDURE tbrole_SysModule_delete_sp
	@ModuleCode nvarchar(20)
AS
BEGIN
	DELETE FROM tbrole_SysModule
	WHERE
		ModuleCode = @ModuleCode
END
GO

--Get all data for table tbrole_SysModule
IF OBJECT_ID ( 'tbrole_SysModule_getAll_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_SysModule_getAll_sp;
GO

CREATE PROCEDURE tbrole_SysModule_getAll_sp

AS
BEGIN
	SELECT
		ModuleCode,
		ModuleNameCn,
		ModuleNameEn,
		SeqNo,
		ParentModuleCode,
		OperCodeGroup,
		URL,
		SysName,
		IsOperModule,
		IsRole
	FROM 
		tbrole_SysModule
END
GO

IF OBJECT_ID ( 'tbrole_SysModule_get_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_SysModule_get_sp;
GO

CREATE PROCEDURE tbrole_SysModule_get_sp
	@ModuleCode nvarchar(20)
AS
BEGIN
	SELECT
		ModuleCode,
		ModuleNameCn,
		ModuleNameEn,
		SeqNo,
		ParentModuleCode,
		OperCodeGroup,
		URL,
		SysName,
		IsOperModule,
		IsRole
	FROM
		tbrole_SysModule
	WHERE
		ModuleCode = @ModuleCode
END
GO


-- 分页查找数据
IF OBJECT_ID ( 'tbrole_SysModule_find_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_SysModule_find_sp;
GO

CREATE PROCEDURE tbrole_SysModule_find_sp
	@ModuleCode nvarchar(20),
	@ModuleNameCn nvarchar(80),
	@ModuleNameEn nvarchar(80),
	@SeqNo int,
	@ParentModuleCode nvarchar(20),
	@OperCodeGroup nvarchar(80),
	@URL nvarchar(80),
	@SysName nvarchar(40),
	@IsOperModule nvarchar(1),
	@IsRole nvarchar(1),
	@startRowIndex INT,
	@maximumRows INT

AS
BEGIN
	IF @ModuleCode IS NOT NULL
		SET @ModuleCode = '%'+ @ModuleCode + '%'
	IF @ModuleNameCn IS NOT NULL
		SET @ModuleNameCn = '%'+ @ModuleNameCn + '%'
	IF @ModuleNameEn IS NOT NULL
		SET @ModuleNameEn = '%'+ @ModuleNameEn + '%'
	IF @ParentModuleCode IS NOT NULL
		SET @ParentModuleCode = '%'+ @ParentModuleCode + '%'
	IF @OperCodeGroup IS NOT NULL
		SET @OperCodeGroup = '%'+ @OperCodeGroup + '%'
	IF @URL IS NOT NULL
		SET @URL = '%'+ @URL + '%'
	IF @SysName IS NOT NULL
		SET @SysName = '%'+ @SysName + '%'
	IF @IsOperModule IS NOT NULL
		SET @IsOperModule = '%'+ @IsOperModule + '%'
	IF @IsRole IS NOT NULL
		SET @IsRole = '%'+ @IsRole + '%'
	-- 取开始结果前的最大值。

	DECLARE @MaxRecord AS nvarchar(20)
	
	SELECT 
		@MaxRecord = MAX(ModuleCode) 
	FROM 
		(SELECT DISTINCT TOP (@startRowIndex) 
			ModuleCode
		FROM 
			tbrole_SysModule
		WHERE
			(@ModuleCode IS NULL OR (ModuleCode LIKE @ModuleCode))  AND 
			(@ModuleNameCn IS NULL OR (ModuleNameCn LIKE @ModuleNameCn))  AND 
			(@ModuleNameEn IS NULL OR (ModuleNameEn LIKE @ModuleNameEn))  AND 
			(@SeqNo IS NULL OR (SeqNo LIKE @SeqNo))  AND 
			(@ParentModuleCode IS NULL OR (ParentModuleCode LIKE @ParentModuleCode))  AND 
			(@OperCodeGroup IS NULL OR (OperCodeGroup LIKE @OperCodeGroup))  AND 
			(@URL IS NULL OR (URL LIKE @URL))  AND 
			(@SysName IS NULL OR (SysName LIKE @SysName))  AND 
			(@IsOperModule IS NULL OR (IsOperModule LIKE @IsOperModule))  AND 
			(@IsRole IS NULL OR (IsRole LIKE @IsRole)) 
		ORDER BY 
			ModuleCode ASC
		) AS T

	IF @MaxRecord IS NULL
		SET @MaxRecord = 0
		
	--取当前页中需要显示的数据
	SELECT DISTINCT TOP (@maximumRows)
		ModuleCode,
		ModuleNameCn,
		ModuleNameEn,
		SeqNo,
		ParentModuleCode,
		OperCodeGroup,
		URL,
		SysName,
		IsOperModule,
		IsRole
	FROM 
		tbrole_SysModule
	WHERE
		(dbo.tbrole_SysModule.ModuleCode > @MaxRecord) AND --用于分页时使用

		(@ModuleCode IS NULL OR (ModuleCode LIKE @ModuleCode))  AND 
		(@ModuleNameCn IS NULL OR (ModuleNameCn LIKE @ModuleNameCn))  AND 
		(@ModuleNameEn IS NULL OR (ModuleNameEn LIKE @ModuleNameEn))  AND 
		(@SeqNo IS NULL OR (SeqNo LIKE @SeqNo))  AND 
		(@ParentModuleCode IS NULL OR (ParentModuleCode LIKE @ParentModuleCode))  AND 
		(@OperCodeGroup IS NULL OR (OperCodeGroup LIKE @OperCodeGroup))  AND 
		(@URL IS NULL OR (URL LIKE @URL))  AND 
		(@SysName IS NULL OR (SysName LIKE @SysName))  AND 
		(@IsOperModule IS NULL OR (IsOperModule LIKE @IsOperModule))  AND 
		(@IsRole IS NULL OR (IsRole LIKE @IsRole)) 
	ORDER BY 
		dbo.tbrole_SysModule.ModuleCode ASC
END
GO

IF OBJECT_ID ( 'tbrole_SysModule_findCount_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_SysModule_findCount_sp;
GO

--取符合要求的数据的总数。

CREATE PROCEDURE tbrole_SysModule_findCount_sp
	@ModuleCode nvarchar(20),
	@ModuleNameCn nvarchar(80),
	@ModuleNameEn nvarchar(80),
	@SeqNo int,
	@ParentModuleCode nvarchar(20),
	@OperCodeGroup nvarchar(80),
	@URL nvarchar(80),
	@SysName nvarchar(40),
	@IsOperModule nvarchar(1),
	@IsRole nvarchar(1),
	@startRowIndex INT,
	@maximumRows INT

AS
BEGIN
	IF @ModuleCode IS NOT NULL
		SET @ModuleCode = '%'+ @ModuleCode + '%'
	IF @ModuleNameCn IS NOT NULL
		SET @ModuleNameCn = '%'+ @ModuleNameCn + '%'
	IF @ModuleNameEn IS NOT NULL
		SET @ModuleNameEn = '%'+ @ModuleNameEn + '%'
	IF @ParentModuleCode IS NOT NULL
		SET @ParentModuleCode = '%'+ @ParentModuleCode + '%'
	IF @OperCodeGroup IS NOT NULL
		SET @OperCodeGroup = '%'+ @OperCodeGroup + '%'
	IF @URL IS NOT NULL
		SET @URL = '%'+ @URL + '%'
	IF @SysName IS NOT NULL
		SET @SysName = '%'+ @SysName + '%'
	IF @IsOperModule IS NOT NULL
		SET @IsOperModule = '%'+ @IsOperModule + '%'
	IF @IsRole IS NOT NULL
		SET @IsRole = '%'+ @IsRole + '%'
	--取符合要求的数据的总数。

	DECLARE @recordCount AS INT

	SELECT 
		@recordCount = COUNT(DISTINCT ModuleCode)
	FROM 
		tbrole_SysModule
	WHERE
		(@ModuleCode IS NULL OR (ModuleCode LIKE @ModuleCode))  AND 
		(@ModuleNameCn IS NULL OR (ModuleNameCn LIKE @ModuleNameCn))  AND 
		(@ModuleNameEn IS NULL OR (ModuleNameEn LIKE @ModuleNameEn))  AND 
		(@SeqNo IS NULL OR (SeqNo LIKE @SeqNo))  AND 
		(@ParentModuleCode IS NULL OR (ParentModuleCode LIKE @ParentModuleCode))  AND 
		(@OperCodeGroup IS NULL OR (OperCodeGroup LIKE @OperCodeGroup))  AND 
		(@URL IS NULL OR (URL LIKE @URL))  AND 
		(@SysName IS NULL OR (SysName LIKE @SysName))  AND 
		(@IsOperModule IS NULL OR (IsOperModule LIKE @IsOperModule))  AND 
		(@IsRole IS NULL OR (IsRole LIKE @IsRole)) 
	return @recordCount
END
GO
-----------------------------------------------------------------------------
--                                                                         -- 
--				stored procedure for table tbrole_Users                --
--                                                                         --
-----------------------------------------------------------------------------

--IsExist procedure for table tbrole_Users
IF OBJECT_ID ( 'tbrole_Users_isExist_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_Users_isExist_sp;
GO

CREATE PROCEDURE tbrole_Users_isExist_sp
	@UserID nvarchar(50)
AS
BEGIN
	DECLARE @records AS INT
	
	SELECT 
		@records = COUNT(*) 
	FROM
		tbrole_Users
	WHERE
		UserID = @UserID
		
	RETURN @records
END
GO

--Insert procedure for table tbrole_Users
IF OBJECT_ID ( 'tbrole_Users_insert_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_Users_insert_sp;
GO

CREATE PROCEDURE tbrole_Users_insert_sp
	@UserID nvarchar(50),
	@UserName nvarchar(50),
	@OrgNo nvarchar(50),
	@DeptNo nvarchar(50),
	@Ascription nvarchar(50),
	@OperRoleGpCode nvarchar(50),
	@DeptRoleGpCode nvarchar(50),
	@Mail nvarchar(80),
	@Tel nvarchar(50),
	@PassWD nvarchar(50),
	@Flag nvarchar(50),
	@Remark nvarchar(200),
	@LastActivityDate datetime,
	@LastLoginDate datetime,
	@IsOnLine bit,
	@IsLockedOut bit,
	@LastLockedOutDate datetime
AS
BEGIN
	INSERT INTO tbrole_Users
		(
			UserID,
			UserName,
			OrgNo,
			DeptNo,
			Ascription,
			OperRoleGpCode,
			DeptRoleGpCode,
			Mail,
			Tel,
			PassWD,
			Flag,
			Remark,
			LastActivityDate,
			LastLoginDate,
			IsOnLine,
			IsLockedOut,
			LastLockedOutDate
		)
	VALUES
		(
			@UserID,
			@UserName,
			@OrgNo,
			@DeptNo,
			@Ascription,
			@OperRoleGpCode,
			@DeptRoleGpCode,
			@Mail,
			@Tel,
			@PassWD,
			@Flag,
			@Remark,
			@LastActivityDate,
			@LastLoginDate,
			@IsOnLine,
			@IsLockedOut,
			@LastLockedOutDate
		)
END
GO

--Update procedure for table tbrole_Users
IF OBJECT_ID ( 'tbrole_Users_update_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_Users_update_sp;
GO

CREATE PROCEDURE tbrole_Users_update_sp
	@UserID nvarchar(50),
	@UserName nvarchar(50),
	@OrgNo nvarchar(50),
	@DeptNo nvarchar(50),
	@Ascription nvarchar(50),
	@OperRoleGpCode nvarchar(50),
	@DeptRoleGpCode nvarchar(50),
	@Mail nvarchar(80),
	@Tel nvarchar(50),
	@PassWD nvarchar(50),
	@Flag nvarchar(50),
	@Remark nvarchar(200),
	@LastActivityDate datetime,
	@LastLoginDate datetime,
	@IsOnLine bit,
	@IsLockedOut bit,
	@LastLockedOutDate datetime
AS
BEGIN
	UPDATE tbrole_Users
	SET
		UserID = @UserID,
		UserName = @UserName,
		OrgNo = @OrgNo,
		DeptNo = @DeptNo,
		Ascription = @Ascription,
		OperRoleGpCode = @OperRoleGpCode,
		DeptRoleGpCode = @DeptRoleGpCode,
		Mail = @Mail,
		Tel = @Tel,
		PassWD = @PassWD,
		Flag = @Flag,
		Remark = @Remark,
		LastActivityDate = @LastActivityDate,
		LastLoginDate = @LastLoginDate,
		IsOnLine = @IsOnLine,
		IsLockedOut = @IsLockedOut,
		LastLockedOutDate = @LastLockedOutDate
	WHERE
		UserID = @UserID
END
GO

--Delete procedure for table tbrole_Users
IF OBJECT_ID ( 'tbrole_Users_delete_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_Users_delete_sp;
GO

CREATE PROCEDURE tbrole_Users_delete_sp
	@UserID nvarchar(50)
AS
BEGIN
	DELETE FROM tbrole_Users
	WHERE
		UserID = @UserID
END
GO

--Get all data for table tbrole_Users
IF OBJECT_ID ( 'tbrole_Users_getAll_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_Users_getAll_sp;
GO

CREATE PROCEDURE tbrole_Users_getAll_sp

AS
BEGIN
	SELECT
		UserID,
		UserName,
		OrgNo,
		DeptNo,
		Ascription,
		OperRoleGpCode,
		DeptRoleGpCode,
		Mail,
		Tel,
		PassWD,
		Flag,
		Remark,
		LastActivityDate,
		LastLoginDate,
		IsOnLine,
		IsLockedOut,
		LastLockedOutDate
	FROM 
		tbrole_Users
END
GO

IF OBJECT_ID ( 'tbrole_Users_get_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_Users_get_sp;
GO

CREATE PROCEDURE tbrole_Users_get_sp
	@UserID nvarchar(50)
AS
BEGIN
	SELECT
		UserID,
		UserName,
		OrgNo,
		DeptNo,
		Ascription,
		OperRoleGpCode,
		DeptRoleGpCode,
		Mail,
		Tel,
		PassWD,
		Flag,
		Remark,
		LastActivityDate,
		LastLoginDate,
		IsOnLine,
		IsLockedOut,
		LastLockedOutDate
	FROM
		tbrole_Users
	WHERE
		UserID = @UserID
END
GO


-- 分页查找数据
IF OBJECT_ID ( 'tbrole_Users_find_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_Users_find_sp;
GO

CREATE PROCEDURE tbrole_Users_find_sp
	@UserID nvarchar(50),
	@UserName nvarchar(50),
	@OrgNo nvarchar(50),
	@DeptNo nvarchar(50),
	@Ascription nvarchar(50),
	@OperRoleGpCode nvarchar(50),
	@DeptRoleGpCode nvarchar(50),
	@Mail nvarchar(80),
	@Tel nvarchar(50),
	@PassWD nvarchar(50),
	@Flag nvarchar(50),
	@Remark nvarchar(200),
	@LastActivityDate datetime,
	@LastLoginDate datetime,
	@IsOnLine bit,
	@IsLockedOut bit,
	@LastLockedOutDate datetime,
	@startRowIndex INT,
	@maximumRows INT

AS
BEGIN
	IF @UserID IS NOT NULL
		SET @UserID = '%'+ @UserID + '%'
	IF @UserName IS NOT NULL
		SET @UserName = '%'+ @UserName + '%'
	IF @OrgNo IS NOT NULL
		SET @OrgNo = '%'+ @OrgNo + '%'
	IF @DeptNo IS NOT NULL
		SET @DeptNo = '%'+ @DeptNo + '%'
	IF @Ascription IS NOT NULL
		SET @Ascription = '%'+ @Ascription + '%'
	IF @OperRoleGpCode IS NOT NULL
		SET @OperRoleGpCode = '%'+ @OperRoleGpCode + '%'
	IF @DeptRoleGpCode IS NOT NULL
		SET @DeptRoleGpCode = '%'+ @DeptRoleGpCode + '%'
	IF @Mail IS NOT NULL
		SET @Mail = '%'+ @Mail + '%'
	IF @Tel IS NOT NULL
		SET @Tel = '%'+ @Tel + '%'
	IF @PassWD IS NOT NULL
		SET @PassWD = '%'+ @PassWD + '%'
	IF @Flag IS NOT NULL
		SET @Flag = '%'+ @Flag + '%'
	IF @Remark IS NOT NULL
		SET @Remark = '%'+ @Remark + '%'
	-- 取开始结果前的最大值。

	DECLARE @MaxRecord AS nvarchar(50)
	
	SELECT 
		@MaxRecord = MAX(UserID) 
	FROM 
		(SELECT DISTINCT TOP (@startRowIndex) 
			UserID
		FROM 
			tbrole_Users
		WHERE
			(@UserID IS NULL OR (UserID LIKE @UserID))  AND 
			(@UserName IS NULL OR (UserName LIKE @UserName))  AND 
			(@OrgNo IS NULL OR (OrgNo LIKE @OrgNo))  AND 
			(@DeptNo IS NULL OR (DeptNo LIKE @DeptNo))  AND 
			(@Ascription IS NULL OR (Ascription LIKE @Ascription))  AND 
			(@OperRoleGpCode IS NULL OR (OperRoleGpCode LIKE @OperRoleGpCode))  AND 
			(@DeptRoleGpCode IS NULL OR (DeptRoleGpCode LIKE @DeptRoleGpCode))  AND 
			(@Mail IS NULL OR (Mail LIKE @Mail))  AND 
			(@Tel IS NULL OR (Tel LIKE @Tel))  AND 
			(@PassWD IS NULL OR (PassWD LIKE @PassWD))  AND 
			(@Flag IS NULL OR (Flag LIKE @Flag))  AND 
			(@Remark IS NULL OR (Remark LIKE @Remark))  AND 
			(@LastActivityDate IS NULL OR (LastActivityDate LIKE @LastActivityDate))  AND 
			(@LastLoginDate IS NULL OR (LastLoginDate LIKE @LastLoginDate))  AND 
			(@IsOnLine IS NULL OR (IsOnLine LIKE @IsOnLine))  AND 
			(@IsLockedOut IS NULL OR (IsLockedOut LIKE @IsLockedOut))  AND 
			(@LastLockedOutDate IS NULL OR (LastLockedOutDate LIKE @LastLockedOutDate)) 
		ORDER BY 
			UserID ASC
		) AS T

	IF @MaxRecord IS NULL
		SET @MaxRecord = 0
		
	--取当前页中需要显示的数据
	SELECT DISTINCT TOP (@maximumRows)
		UserID,
		UserName,
		OrgNo,
		DeptNo,
		Ascription,
		OperRoleGpCode,
		DeptRoleGpCode,
		Mail,
		Tel,
		PassWD,
		Flag,
		Remark,
		LastActivityDate,
		LastLoginDate,
		IsOnLine,
		IsLockedOut,
		LastLockedOutDate
	FROM 
		tbrole_Users
	WHERE
		(dbo.tbrole_Users.UserID > @MaxRecord) AND --用于分页时使用

		(@UserID IS NULL OR (UserID LIKE @UserID))  AND 
		(@UserName IS NULL OR (UserName LIKE @UserName))  AND 
		(@OrgNo IS NULL OR (OrgNo LIKE @OrgNo))  AND 
		(@DeptNo IS NULL OR (DeptNo LIKE @DeptNo))  AND 
		(@Ascription IS NULL OR (Ascription LIKE @Ascription))  AND 
		(@OperRoleGpCode IS NULL OR (OperRoleGpCode LIKE @OperRoleGpCode))  AND 
		(@DeptRoleGpCode IS NULL OR (DeptRoleGpCode LIKE @DeptRoleGpCode))  AND 
		(@Mail IS NULL OR (Mail LIKE @Mail))  AND 
		(@Tel IS NULL OR (Tel LIKE @Tel))  AND 
		(@PassWD IS NULL OR (PassWD LIKE @PassWD))  AND 
		(@Flag IS NULL OR (Flag LIKE @Flag))  AND 
		(@Remark IS NULL OR (Remark LIKE @Remark))  AND 
		(@LastActivityDate IS NULL OR (LastActivityDate LIKE @LastActivityDate))  AND 
		(@LastLoginDate IS NULL OR (LastLoginDate LIKE @LastLoginDate))  AND 
		(@IsOnLine IS NULL OR (IsOnLine LIKE @IsOnLine))  AND 
		(@IsLockedOut IS NULL OR (IsLockedOut LIKE @IsLockedOut))  AND 
		(@LastLockedOutDate IS NULL OR (LastLockedOutDate LIKE @LastLockedOutDate)) 
	ORDER BY 
		dbo.tbrole_Users.UserID ASC
END
GO

IF OBJECT_ID ( 'tbrole_Users_findCount_sp', 'P' ) IS NOT NULL 
    DROP PROCEDURE tbrole_Users_findCount_sp;
GO

--取符合要求的数据的总数。

CREATE PROCEDURE tbrole_Users_findCount_sp
	@UserID nvarchar(50),
	@UserName nvarchar(50),
	@OrgNo nvarchar(50),
	@DeptNo nvarchar(50),
	@Ascription nvarchar(50),
	@OperRoleGpCode nvarchar(50),
	@DeptRoleGpCode nvarchar(50),
	@Mail nvarchar(80),
	@Tel nvarchar(50),
	@PassWD nvarchar(50),
	@Flag nvarchar(50),
	@Remark nvarchar(200),
	@LastActivityDate datetime,
	@LastLoginDate datetime,
	@IsOnLine bit,
	@IsLockedOut bit,
	@LastLockedOutDate datetime,
	@startRowIndex INT,
	@maximumRows INT

AS
BEGIN
	IF @UserID IS NOT NULL
		SET @UserID = '%'+ @UserID + '%'
	IF @UserName IS NOT NULL
		SET @UserName = '%'+ @UserName + '%'
	IF @OrgNo IS NOT NULL
		SET @OrgNo = '%'+ @OrgNo + '%'
	IF @DeptNo IS NOT NULL
		SET @DeptNo = '%'+ @DeptNo + '%'
	IF @Ascription IS NOT NULL
		SET @Ascription = '%'+ @Ascription + '%'
	IF @OperRoleGpCode IS NOT NULL
		SET @OperRoleGpCode = '%'+ @OperRoleGpCode + '%'
	IF @DeptRoleGpCode IS NOT NULL
		SET @DeptRoleGpCode = '%'+ @DeptRoleGpCode + '%'
	IF @Mail IS NOT NULL
		SET @Mail = '%'+ @Mail + '%'
	IF @Tel IS NOT NULL
		SET @Tel = '%'+ @Tel + '%'
	IF @PassWD IS NOT NULL
		SET @PassWD = '%'+ @PassWD + '%'
	IF @Flag IS NOT NULL
		SET @Flag = '%'+ @Flag + '%'
	IF @Remark IS NOT NULL
		SET @Remark = '%'+ @Remark + '%'
	--取符合要求的数据的总数。

	DECLARE @recordCount AS INT

	SELECT 
		@recordCount = COUNT(DISTINCT UserID)
	FROM 
		tbrole_Users
	WHERE
		(@UserID IS NULL OR (UserID LIKE @UserID))  AND 
		(@UserName IS NULL OR (UserName LIKE @UserName))  AND 
		(@OrgNo IS NULL OR (OrgNo LIKE @OrgNo))  AND 
		(@DeptNo IS NULL OR (DeptNo LIKE @DeptNo))  AND 
		(@Ascription IS NULL OR (Ascription LIKE @Ascription))  AND 
		(@OperRoleGpCode IS NULL OR (OperRoleGpCode LIKE @OperRoleGpCode))  AND 
		(@DeptRoleGpCode IS NULL OR (DeptRoleGpCode LIKE @DeptRoleGpCode))  AND 
		(@Mail IS NULL OR (Mail LIKE @Mail))  AND 
		(@Tel IS NULL OR (Tel LIKE @Tel))  AND 
		(@PassWD IS NULL OR (PassWD LIKE @PassWD))  AND 
		(@Flag IS NULL OR (Flag LIKE @Flag))  AND 
		(@Remark IS NULL OR (Remark LIKE @Remark))  AND 
		(@LastActivityDate IS NULL OR (LastActivityDate LIKE @LastActivityDate))  AND 
		(@LastLoginDate IS NULL OR (LastLoginDate LIKE @LastLoginDate))  AND 
		(@IsOnLine IS NULL OR (IsOnLine LIKE @IsOnLine))  AND 
		(@IsLockedOut IS NULL OR (IsLockedOut LIKE @IsLockedOut))  AND 
		(@LastLockedOutDate IS NULL OR (LastLockedOutDate LIKE @LastLockedOutDate)) 
	return @recordCount
END
GO
