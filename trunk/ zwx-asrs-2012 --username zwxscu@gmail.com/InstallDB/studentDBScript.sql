USE [students]
GO
/****** Object:  Table [dbo].[depart]    Script Date: 10/28/2012 22:29:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[depart](
	[dno] [numeric](20, 0) NOT NULL,
	[dname] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_depart] PRIMARY KEY CLUSTERED 
(
	[dno] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[course]    Script Date: 10/28/2012 22:29:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[course](
	[cno] [numeric](20, 0) NOT NULL,
	[cname] [nvarchar](50) NOT NULL,
	[credits] [int] NOT NULL,
 CONSTRAINT [PK_course] PRIMARY KEY CLUSTERED 
(
	[cno] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[student]    Script Date: 10/28/2012 22:29:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[student](
	[sno] [numeric](20, 0) NOT NULL,
	[sname] [nvarchar](50) NOT NULL,
	[ssex] [bit] NOT NULL,
	[dno] [numeric](20, 0) NOT NULL,
 CONSTRAINT [PK_student] PRIMARY KEY CLUSTERED 
(
	[sno] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[pro_insert]    Script Date: 10/28/2012 22:29:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pro_insert]
	@cnob numeric(20,0),
	@cname nvarchar(50),
	@credits int
 
  
	-- Add the parameters for the stored procedure here
	 --<@Param1, sysname, @p1> <Datatype_For_Param1, , int> = <Default_Value_For_Param1, , 0>, 
	--<@Param2, sysname, @p2> <Datatype_For_Param2, , int> = <Default_Value_For_Param2, , 0>
AS

	INSERT INTO course VALUES(@cnob,@cname,@credits)
GO
/****** Object:  StoredProcedure [dbo].[pro_courseDelByname]    Script Date: 10/28/2012 22:29:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pro_courseDelByname]
	@name nvarchar(50)
	-- Add the parameters for the stored procedure here
	--<@Param1, sysname, @p1> <Datatype_For_Param1, , int> = <Default_Value_For_Param1, , 0>, 
	--<@Param2, sysname, @p2> <Datatype_For_Param2, , int> = <Default_Value_For_Param2, , 0>
AS
DELETE FROM course
WHERE cname=@name
GO
/****** Object:  Table [dbo].[enrollment]    Script Date: 10/28/2012 22:29:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[enrollment](
	[sno] [numeric](20, 0) NOT NULL,
	[cno] [numeric](20, 0) NOT NULL,
	[grade] [int] NOT NULL,
 CONSTRAINT [PK_enrollment_1] PRIMARY KEY CLUSTERED 
(
	[sno] ASC,
	[cno] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[student_grade]    Script Date: 10/28/2012 22:29:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[student_grade]
AS
SELECT     TOP (100) PERCENT dbo.student.sno AS 学号, dbo.student.sname AS 姓名, dbo.student.ssex AS 性别, dbo.depart.dname AS 院系, 
                      dbo.course.cname AS 课程, dbo.enrollment.grade AS 成绩
FROM         dbo.course INNER JOIN
                      dbo.enrollment ON dbo.course.cno = dbo.enrollment.cno INNER JOIN
                      dbo.student ON dbo.enrollment.sno = dbo.student.sno INNER JOIN
                      dbo.depart ON dbo.student.dno = dbo.depart.dno
ORDER BY 学号
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[40] 2[3] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "course"
            Begin Extent = 
               Top = 164
               Left = 375
               Bottom = 264
               Right = 527
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "student"
            Begin Extent = 
               Top = 10
               Left = 15
               Bottom = 125
               Right = 167
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "depart"
            Begin Extent = 
               Top = 175
               Left = 185
               Bottom = 260
               Right = 337
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "enrollment"
            Begin Extent = 
               Top = 6
               Left = 418
               Bottom = 106
               Right = 570
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 1035
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 600
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'student_grade'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'student_grade'
GO
/****** Object:  StoredProcedure [dbo].[pro_queryGrade]    Script Date: 10/28/2012 22:29:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<张文香>
-- Create date: <2012-08-26>
-- Description:	<存储过程——查询学生成绩表>
-- =============================================
CREATE PROCEDURE [dbo].[pro_queryGrade]
	-- Add the parameters for the stored procedure here
	--<@Param1, sysname, @p1> <Datatype_For_Param1, , int> = <Default_Value_For_Param1, , 0>, 
	--<@Param2, sysname, @p2> <Datatype_For_Param2, , int> = <Default_Value_For_Param2, , 0>
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	--SELECT <@Param1, sysname, @p1>, <@Param2, sysname, @p2>
	SELECT* FROM [students].[dbo].[student_grade]
END
GO
/****** Object:  ForeignKey [FK_course_course]    Script Date: 10/28/2012 22:29:46 ******/
ALTER TABLE [dbo].[course]  WITH CHECK ADD  CONSTRAINT [FK_course_course] FOREIGN KEY([cno])
REFERENCES [dbo].[course] ([cno])
GO
ALTER TABLE [dbo].[course] CHECK CONSTRAINT [FK_course_course]
GO
/****** Object:  ForeignKey [FK_enrollment_course]    Script Date: 10/28/2012 22:29:46 ******/
ALTER TABLE [dbo].[enrollment]  WITH CHECK ADD  CONSTRAINT [FK_enrollment_course] FOREIGN KEY([cno])
REFERENCES [dbo].[course] ([cno])
GO
ALTER TABLE [dbo].[enrollment] CHECK CONSTRAINT [FK_enrollment_course]
GO
/****** Object:  ForeignKey [FK_enrollment_student]    Script Date: 10/28/2012 22:29:46 ******/
ALTER TABLE [dbo].[enrollment]  WITH CHECK ADD  CONSTRAINT [FK_enrollment_student] FOREIGN KEY([sno])
REFERENCES [dbo].[student] ([sno])
GO
ALTER TABLE [dbo].[enrollment] CHECK CONSTRAINT [FK_enrollment_student]
GO
/****** Object:  ForeignKey [FK_student_depart]    Script Date: 10/28/2012 22:29:46 ******/
ALTER TABLE [dbo].[student]  WITH CHECK ADD  CONSTRAINT [FK_student_depart] FOREIGN KEY([dno])
REFERENCES [dbo].[depart] ([dno])
GO
ALTER TABLE [dbo].[student] CHECK CONSTRAINT [FK_student_depart]
GO
