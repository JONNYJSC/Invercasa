USE [master]
GO
/****** Object:  Database [dbInvercasa]    Script Date: 10/06/2022 11:06:57 p. m. ******/
CREATE DATABASE [dbInvercasa]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'dbInvercasa', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\dbInvercasa.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'dbInvercasa_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\dbInvercasa_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [dbInvercasa] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbInvercasa].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [dbInvercasa] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [dbInvercasa] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [dbInvercasa] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [dbInvercasa] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [dbInvercasa] SET ARITHABORT OFF 
GO
ALTER DATABASE [dbInvercasa] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [dbInvercasa] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dbInvercasa] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dbInvercasa] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dbInvercasa] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [dbInvercasa] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [dbInvercasa] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dbInvercasa] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [dbInvercasa] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dbInvercasa] SET  DISABLE_BROKER 
GO
ALTER DATABASE [dbInvercasa] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dbInvercasa] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [dbInvercasa] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [dbInvercasa] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [dbInvercasa] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dbInvercasa] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [dbInvercasa] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [dbInvercasa] SET RECOVERY FULL 
GO
ALTER DATABASE [dbInvercasa] SET  MULTI_USER 
GO
ALTER DATABASE [dbInvercasa] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [dbInvercasa] SET DB_CHAINING OFF 
GO
ALTER DATABASE [dbInvercasa] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [dbInvercasa] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [dbInvercasa] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [dbInvercasa] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'dbInvercasa', N'ON'
GO
ALTER DATABASE [dbInvercasa] SET QUERY_STORE = OFF
GO
USE [dbInvercasa]
GO
/****** Object:  UserDefinedFunction [dbo].[FnCalcularVacaciones]    Script Date: 10/06/2022 11:06:57 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[FnCalcularVacaciones]
(
	@fechaInicial DATE,
    @fechaFinal DATE
)
RETURNS DECIMAL(18, 2)
AS
BEGIN

	DECLARE @diasMes INT = 30;
	DECLARE @VacacionesMes DECIMAL(18,2) = 2.5;
    DECLARE @diasGanados DECIMAL(18, 2) = DATEDIFF(DAY, @fechaInicial, @fechaFinal);
	DECLARE @Vacaciones DECIMAL(18,2) = (@diasGanados / @diasMes) * @VacacionesMes;

	RETURN @vacaciones;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[FnValidarCedula]    Script Date: 10/06/2022 11:06:57 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[FnValidarCedula]
(
    @NumeroIdentificacion VARCHAR(50)
)
RETURNS VARCHAR(50)
AS
BEGIN
    DECLARE @Identidad VARCHAR(50);
    SET @Identidad =
    (
        SELECT e.NumeroIdentificacion
        FROM dbo.Empleado e
        WHERE e.NumeroIdentificacion = @NumeroIdentificacion
    );
    IF NOT EXISTS
    (
        SELECT e.NumeroIdentificacion
        FROM dbo.Empleado e
        WHERE e.NumeroIdentificacion = @NumeroIdentificacion
    )
        SET @Identidad = 'Si';
    ELSE
        SET @Identidad = 'No';
    RETURN @Identidad;
END;
GO
/****** Object:  Table [dbo].[Empleado]    Script Date: 10/06/2022 11:06:57 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empleado](
	[IdEmpleado] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](30) NOT NULL,
	[TipoIdentificacion] [varchar](50) NOT NULL,
	[NumeroIdentificacion] [varchar](50) NOT NULL,
	[FechaIngreso] [date] NOT NULL,
	[SalarioBaseMensual] [decimal](18, 2) NOT NULL,
	[Direccion] [varchar](250) NOT NULL,
 CONSTRAINT [PK_Empleado] PRIMARY KEY CLUSTERED 
(
	[IdEmpleado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vacaciones]    Script Date: 10/06/2022 11:06:57 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vacaciones](
	[IdVaciones] [int] IDENTITY(1,1) NOT NULL,
	[FechaInicio] [date] NOT NULL,
	[FechaFin] [date] NOT NULL,
	[IdEmpleado] [int] NOT NULL,
 CONSTRAINT [PK_Vacaciones] PRIMARY KEY CLUSTERED 
(
	[IdVaciones] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Vacaciones]  WITH CHECK ADD  CONSTRAINT [FK_Vacaciones_Empleado] FOREIGN KEY([IdEmpleado])
REFERENCES [dbo].[Empleado] ([IdEmpleado])
GO
ALTER TABLE [dbo].[Vacaciones] CHECK CONSTRAINT [FK_Vacaciones_Empleado]
GO
/****** Object:  StoredProcedure [dbo].[SP_DEL_Eemplado]    Script Date: 10/06/2022 11:06:57 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_DEL_Eemplado]
(@IdEmplado INT)
AS
SET NOCOUNT ON;
BEGIN
        DELETE FROM dbo.Empleado
        WHERE IdEmpleado = @IdEmplado;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_DEL_Vacaciones]    Script Date: 10/06/2022 11:06:57 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_DEL_Vacaciones]
(@idVacaciones INT)
AS
SET NOCOUNT ON;
BEGIN
        DELETE FROM dbo.Vacaciones
        WHERE IdVaciones = @idVacaciones;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_INS_Empleado]    Script Date: 10/06/2022 11:06:57 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_INS_Empleado]
(
    @Nombre VARCHAR(30),
    @TipoIdentificacion VARCHAR(50),
    @NumeroIdentificacion VARCHAR(50),
    @FechaIngreso DATE,
    @SalarioBaseMensual DECIMAL,
    @Direccion VARCHAR(250)
)
AS
SET NOCOUNT ON;
BEGIN
    INSERT dbo.Empleado
    (
        Nombre,
        TipoIdentificacion,
        NumeroIdentificacion,
        FechaIngreso,
        SalarioBaseMensual,
        Direccion
    )
    VALUES
    (   @Nombre,               -- Nombre - varchar(30)
        @TipoIdentificacion,   -- TipoIdentificacion - varchar(50)
        @NumeroIdentificacion, -- NumeroIdentificacion - varchar(50)
        @FechaIngreso,         -- FechaIngreso - datetime
        @SalarioBaseMensual,   -- SalarioBaseMensual - decimal(18, 2)
        @Direccion             -- Direccion - varchar(250)
        );
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_INS_Vacaciones]    Script Date: 10/06/2022 11:06:57 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_INS_Vacaciones]
(
    @FechaInicio DATE,
    @FechaFin DATE,
    @IdEmplado INT
)
AS
SET NOCOUNT ON;
BEGIN
    INSERT dbo.Vacaciones
    (
        FechaInicio,
        FechaFin,
        IdEmpleado
    )
    VALUES
    (   @FechaInicio, -- FechaInicio - date
        @FechaFin,    -- FechaFin - date
        @IdEmplado    -- IdEmpleado - int
        );
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_ReporteVacaciones]    Script Date: 10/06/2022 11:06:57 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ReporteVacaciones]
    (@IdEmpleado INT)
AS
SET NOCOUNT ON;
BEGIN
    SELECT e.IdEmpleado,
      e.Nombre,
      e.FechaIngreso,
	  ISNULL(b.DiasTomados, 0) AS DiasTomados,
	  (SELECT dbo.FnCalcularVacaciones(e.FechaIngreso, GETDATE())) AS DiasGenerados
FROM dbo.Empleado e
LEFT JOIN (SELECT SUM(DATEDIFF(DAY, v.FechaInicio, v.FechaFin)) AS DiasTomados,
		          v.IdEmpleado 
		   FROM dbo.Vacaciones v 
		   GROUP BY v.IdEmpleado) b ON b.IdEmpleado = e.IdEmpleado		   
WHERE e.IdEmpleado = @IdEmpleado
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SEL_Empleado]    Script Date: 10/06/2022 11:06:57 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_SEL_Empleado]
(@idEmpleado INT = 0)
AS
SET NOCOUNT ON;
BEGIN
    IF @idEmpleado > 0
        SELECT *
        FROM dbo.Empleado e
        WHERE e.IdEmpleado = @idEmpleado;
    ELSE
        SELECT *
        FROM dbo.Empleado e;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_SEL_Vacaciones]    Script Date: 10/06/2022 11:06:57 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_SEL_Vacaciones]
(@IdVacaciones INT = 0)
AS
SET NOCOUNT ON;
BEGIN

    IF @IdVacaciones > 0
        SELECT *
        FROM dbo.Vacaciones v
        WHERE v.IdVaciones = @IdVacaciones;
    ELSE
        SELECT *
        FROM dbo.Vacaciones v;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_UPD_Emplado]    Script Date: 10/06/2022 11:06:57 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_UPD_Emplado]
(
    @IdEmplado INT,
    @Nombre VARCHAR(30),
    @TipoIdentificacion VARCHAR(50),
    @NumeroIdentificacion VARCHAR(50),
    @FechaIngreso DATETIME,
    @SalarioBaseMensual DECIMAL,
    @Direccion VARCHAR(250)
)
AS
SET NOCOUNT ON;
BEGIN
    UPDATE dbo.Empleado
    SET Nombre = @Nombre,
        TipoIdentificacion = @TipoIdentificacion,
        NumeroIdentificacion = @NumeroIdentificacion,
        FechaIngreso = @FechaIngreso,
        SalarioBaseMensual = @SalarioBaseMensual,
        Direccion = @Direccion
    WHERE IdEmpleado = @IdEmplado;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_UPD_Vacaciones]    Script Date: 10/06/2022 11:06:57 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_UPD_Vacaciones]
(
	@IdEmplado INT,
    @FechaInicio DATE,
    @FechaFin DATE
)
AS
SET NOCOUNT ON;
BEGIN
    UPDATE dbo.Vacaciones
    SET FechaInicio = @FechaInicio,
        FechaFin = @FechaFin
    WHERE IdEmpleado = @IdEmplado;
END;
GO
USE [master]
GO
ALTER DATABASE [dbInvercasa] SET  READ_WRITE 
GO