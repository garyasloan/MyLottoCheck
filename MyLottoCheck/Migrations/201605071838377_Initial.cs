namespace MyLottoCheck.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Email = c.String(maxLength: 256),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(nullable: false, maxLength: 128),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                {
                    RoleId = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);

            CreateTable(
                "MyLottoCheck.Games",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Name = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name);

            Sql(@"
                Create Function MyLottoCheck.CaliforniaMegaMillionsLottoRowCheckSum
                    (
                      @UserId UniqueIdentifier,
                      @FirstPick Int,
                      @SecondPick Int,
                      @ThirdPick Int,
                      @FourthPick Int,
                      @FifthPick Int,
                      @MegaPick Int

                    )
                Returns NVarchar(140)
                    With SchemaBinding
                As
                    Begin
                        Declare @SortTable Table ( Pick Varchar(2) );
                        Insert  @SortTable
                        Values  ( Right('0' + Convert(Varchar(2),@FirstPick),2) );
                        Insert  @SortTable
                        Values  ( Right('0' + Convert(Varchar(2),@SecondPick),2) );
                        Insert  @SortTable
                        Values  ( Right('0' + Convert(Varchar(2),@ThirdPick),2) );
                        Insert  @SortTable
                        Values  ( Right('0' + Convert(Varchar(2),@FourthPick),2) );
                        Insert  @SortTable
                        Values  ( Right('0' + Convert(Varchar(2),@FifthPick),2) );
		

                        Declare @CheckSum Varchar(12);
                        Set @CheckSum = '';
                        Select  @CheckSum = @CheckSum + Pick
                        From    @SortTable
                        Order By Pick;
                        Set @CheckSum = @CheckSum + Right('0' + Convert(Varchar(2),@MegaPick),2);
                        Return @CheckSum + Convert(NVarchar(128), @UserId); 
                    End;
                ");

            Sql(@"
                Create Table MyLottoCheck.CaliforniaMegaMillionUserPicks
                    (
                      Id UniqueIdentifier Not Null
                                          Constraint DF_MyLottoCheck_CaliforniaMegaMillionUserPicks_Id Default ( NewSequentialId() ),
                      UserId NVarchar(128) Not Null,
                      FirstPick Int Not Null,
                      SecondPick Int Not Null,
                      ThirdPick Int Not Null,
                      FourthPick Int Not Null,
                      FifthPick Int Not Null,
                      MegaPick Int Not Null,017

                      DateCreated DateTime2 Not Null
                                            Constraint DF_MyLottoCheck_CaliforniaMegaMillionUserPicks_DateCreated Default ( GetDate() ),
                      RowCheckSum As MyLottoCheck.CaliforniaMegaMillionsLottoRowCheckSum(UserId,FirstPick,SecondPick,ThirdPick,FourthPick,FifthPick,MegaPick)
                        Persisted Constraint PK_MyLottoCheck_CaliforniaMegaMillionUserPicks Primary Key Clustered ( Id Asc ),
                      Constraint CK_MyLottoCheck_CaliforniaMegaMillionUserPicks_FirstPick_Range Check ( FirstPick >= 1
                                                                                                        And FirstPick <= 75 ),
                      Constraint CK_MyLottoCheck_CaliforniaMegaMillionUserPicks_SecondPick_Range Check ( SecondPick >= 1
                                                                                                         And SecondPick <= 75 ),
                      Constraint CK_MyLottoCheck_CaliforniaMegaMillionUserPicks_ThirdPick_Range Check ( ThirdPick >= 1
                                                                                                        And ThirdPick <= 75 ),
                      Constraint CK_MyLottoCheck_CaliforniaMegaMillionUserPicks_FourthPick_Range Check ( FourthPick >= 1
                                                                                                         And FourthPick <= 75 ),
                      Constraint CK_MyLottoCheck_CaliforniaMegaMillionUserPicks_FifthPick_Range Check ( FifthPick >= 1
                                                                                                        And FifthPick <= 75 ),
                      Constraint CK_MyLottoCheck_CaliforniaMegaMillionUserPicks_MegaPick_Range Check ( FifthPick >= 1
                                                                                                       And MegaPick <= 15 ),
                      Constraint CK_MyLottoCheck_CaliforniaMegaMillionUserPicks_FirstPick_Unique Check ( FirstPick != SecondPick
                                                                                                         And FirstPick != ThirdPick
                                                                                                         And FirstPick != FourthPick
                                                                                                         And FirstPick != FifthPick ),
                      Constraint CK_MyLottoCheck_CaliforniaMegaMillionUserPicks_SecondPick_Unique Check ( SecondPick != ThirdPick
                                                                                                          And SecondPick != FourthPick
                                                                                                          And SecondPick != FifthPick ),
                      Constraint CK_MyLottoCheck_CaliforniaMegaMillionUserPicks_ThirdPick_Unique Check ( ThirdPick != FourthPick
                                                                                                         And ThirdPick != FifthPick ),
                      Constraint CK_MyLottoCheck_CaliforniaMegaMillionUserPicks_FourthdPick_Unique Check ( FourthPick != FifthPick ),
                      Constraint UC_MyLottoCheck_CaliforniaMegaMillionUserPicks_RowCheckSum Unique ( RowCheckSum )
                    );");

            Sql(@"
                Create Table MyLottoCheck.CaliforniaMegaMillionsAllWinningNumbersAndPrizes
                    (
                        Id UniqueIdentifier Not Null
                                            Constraint DF_MyLottoCheck_CaliforniaMegaMillionsAllWinningNumbersAndPrizes_Id Default ( NewSequentialId() ),
                        DrawNumber Int Not Null,
                        DrawDate Date Not Null,
                        Number1 Int Not Null,
                        Number2 Int Not Null,
                        Number3 Int Not Null,
                        Number4 Int Not Null,
                        Number5 Int Not Null,
                        MegaNumber Int Not Null,
                        FiveMatchesPlusMegaPrizeAmount BigInt Not Null,
                        FiveMatchesPrizeAmount BigInt Not Null,
                        FourMatchesPlusMegaPrizeAmount BigInt Not Null,
                        FourMatchesPrizeAmount BigInt Not Null,
                        ThreeMatchesPlusMegaPrizeAmount BigInt Not Null,
                        ThreeMatchesPrizeAmount BigInt Not Null,
                        TwoMatchesPlusMegaPrizeAmount BigInt Not Null,
                        OneMatchPlusMegaPrizeAmount BigInt Not Null,
                        MegaMatchOnlyPrizeAmount BigInt Not Null,
                        DateCreated DateTime Not Null
                                            Constraint DF_MyLottoCheck_CaliforniaMegaMillionsAllWinningNumberAndPrize_DateCreated Default ( GetDate() ),
                        Constraint PK_CaliforniaMegaMillionsAllWinningNumbersAndPrizes Primary Key Clustered ( Id Asc ),
                        Constraint UC_CaliforniaMegaMillionsAllWinningNumbersAndPrizes_DrawNumber_DrawDate Unique NonClustered ( DrawNumber Asc,DrawDate Asc ),
                        Constraint UC_MyLottoCheck_CaliforniaMegaMillionsAllWinningNumberAndPrize_DrawNumber Unique NonClustered ( DrawNumber Asc )
                    );");
            Sql(@"
                    Create Procedure MyLottoCheck.GetWinningCaliforniaMegaMillionsDrawsForUser
                        (
                          @UserId UniqueIdentifier
                        )
                    As
                        Begin 

                            Declare @AnalyzeNumbers Table
                                (
                                  PickId UniqueIdentifier,
                                  DrawNumber BigInt,
                                  DrawDate Varchar(10),
                                  FirstPick Int,
                                  MatchedNumber1 Int,
                                  SecondPick Int,
                                  MatchedNumber2 Int,
                                  ThirdPick Int,
                                  MatchedNumber3 Int,
                                  FourthPick Int,
                                  MatchedNumber4 Int,
                                  FifthPick Int,
                                  MatchedNumber5 Int,
                                  MegaPick Int,
                                  MatchedNumberMega Int,
                                  MatchScore Int,
                                  PrizeAmount BigInt
                                );

                            Insert  @AnalyzeNumbers
                                    ( PickId,
                                      DrawNumber,
                                      DrawDate,
                                      FirstPick,
                                      MatchedNumber1,
                                      SecondPick,
                                      MatchedNumber2,
                                      ThirdPick,
                                      MatchedNumber3,
                                      FourthPick,
                                      MatchedNumber4,
                                      FifthPick,
                                      MatchedNumber5,
                                      MegaPick,
                                      MatchedNumberMega
                                    )
                                    Select  mmp.Id,
                                            DrawNumber,
                                            DrawDate,
                                            FirstPick,
                                            Case When ( FirstPick = Number1
                                                        Or FirstPick = Number2
                                                        Or FirstPick = Number3
                                                        Or FirstPick = Number4
                                                        Or FirstPick = Number5
                                                      ) Then 1
                                                 Else 0
                                            End,
                                            SecondPick,
                                            Case When ( SecondPick = Number1
                                                        Or SecondPick = Number2
                                                        Or SecondPick = Number3
                                                        Or SecondPick = Number4
                                                        Or SecondPick = Number5
                                                      ) Then 1
                                                 Else 0
                                            End,
                                            ThirdPick,
                                            Case When ( ThirdPick = Number1
                                                        Or ThirdPick = Number2
                                                        Or ThirdPick = Number3
                                                        Or ThirdPick = Number4
                                                        Or ThirdPick = Number5
                                                      ) Then 1
                                                 Else 0
                                            End,
                                            FourthPick,
                                            Case When ( FourthPick = Number1
                                                        Or FourthPick = Number2
                                                        Or FourthPick = Number3
                                                        Or FourthPick = Number4
                                                        Or FourthPick = Number5
                                                      ) Then 1
                                                 Else 0
                                            End,
                                            FifthPick,
                                            Case When ( FifthPick = Number1
                                                        Or FifthPick = Number2
                                                        Or FifthPick = Number3
                                                        Or FifthPick = Number4
                                                        Or FifthPick = Number5
                                                      ) Then 1
                                                 Else 0
                                            End,
                                            MegaPick,
                                            Case When MegaPick = MegaNumber Then 10
                                                 Else 0
                                            End
                                    From    MyLottoCheck.CaliforniaMegaMillionsAllWinningNumbersAndPrizes mmwn
                                            Cross Join MyLottoCheck.CaliforniaMegaMillionUserPicks mmp
                                    Where   UserId = @UserId;

                            Update  @AnalyzeNumbers
                            Set     MatchScore = MatchedNumber1 + MatchedNumber2 + MatchedNumber3 + MatchedNumber4 + MatchedNumber5 + MatchedNumberMega;

                            Delete  @AnalyzeNumbers
                            Where   MatchScore < 3;

                            Update  @AnalyzeNumbers
                            Set     PrizeAmount = Case MatchScore
                                                    When 10 Then MegaMatchOnlyPrizeAmount
                                                    When 11 Then OneMatchPlusMegaPrizeAmount
                                                    When 12 Then TwoMatchesPlusMegaPrizeAmount
                                                    When 3 Then ThreeMatchesPrizeAmount
                                                    When 13 Then ThreeMatchesPlusMegaPrizeAmount
                                                    When 4 Then FourMatchesPrizeAmount
                                                    When 14 Then FourMatchesPlusMegaPrizeAmount
                                                    When 5 Then FiveMatchesPrizeAmount
                                                    When 15 Then FiveMatchesPlusMegaPrizeAmount
                                                  End
                            From    @AnalyzeNumbers an
                                    Join MyLottoCheck.CaliforniaMegaMillionsAllWinningNumbersAndPrizes mmwn
                                        On an.DrawNumber = mmwn.DrawNumber;
	
                            Select  DrawNumber,
                                    Convert(Varchar(10),Cast(DrawDate As Date),101) DrawDate,
                                    FirstPick,
                                    MatchedNumber1,
                                    SecondPick,
                                    MatchedNumber2,
                                    ThirdPick,
                                    MatchedNumber3,
                                    FourthPick,
                                    MatchedNumber4,
                                    FifthPick,
                                    MatchedNumber5,
                                    MegaPick,
                                    MatchedNumberMega,
                                    PrizeAmount
                            From    @AnalyzeNumbers
                            Order By DrawNumber Desc;
                        End;  
            ");

            Sql(@"
                    Create Table MyLottoCheck.CaliforniaMegaMillionsAllWinningNumbers
                        (
                          Id UniqueIdentifier Not Null
                                              Constraint DF_MyLottoCheck_CaliforniaMegaMillionsAllWinningNumbers_Id Default ( NewSequentialId() ),
                          DrawNumber Int Not Null,
                          DrawDate Date Not Null,
                          Number Int Not Null,
                          IsMegaNumber Bit Not Null,
                          DateCreated DateTime Not Null
                                               Constraint DF_MyLottoCheck_CaliforniaMegaMillionsAllWinningNumbers_DateCreated Default ( GetDate() ),
                          Constraint PK_CaliforniaMegaMillionsAllWinningNumbers Primary Key Clustered ( Id Asc ),
                          Constraint UC_CaliforniaMegaMillionsAllWinningNumbers_DrawNumber_DrawDate_Number_IsMegaNumber Unique NonClustered
                            ( DrawNumber Asc,DrawDate Asc,Number,IsMegaNumber ),
                          Constraint CK_CaliforniaMegaMillionsAllWinningNumbers_Number Check ( Case When ( IsMegaNumber = 0
                                                                                                           And Number >= 1
                                                                                                           And Number <= 75
                                                                                                         )
                                                                                                         Or ( IsMegaNumber = 1
                                                                                                              And Number >= 1
                                                                                                              And Number <= 15
                                                                                                            ) Then 1
                                                                                                    Else 0
                                                                                               End = 1 ));
                ");

            Sql(@"
                    /* ------------------------------------------------------------------------ 
                            Elmah schema creation
                       ------------------------------------------------------------------------ */

                    CREATE TABLE[dbo].[ELMAH_Error]
                    (
                        [ErrorId]
                            UNIQUEIDENTIFIER NOT NULL,
                        [Application] NVARCHAR(60)  COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
                        [Host]        NVARCHAR(50)  COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
                        [Type]        NVARCHAR(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
                        [Source]      NVARCHAR(60)  COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
                        [Message]     NVARCHAR(500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
                        [User]        NVARCHAR(50)  COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
                        [StatusCode]  INT NOT NULL,
                        [TimeUtc]
                            DATETIME NOT NULL,
                        [Sequence]
                            INT IDENTITY(1, 1) NOT NULL,

                    [AllXml]      NTEXT COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
                    ) 
                    ON[PRIMARY] TEXTIMAGE_ON[PRIMARY]

                    GO

                    ALTER TABLE[dbo].[ELMAH_Error]
                            WITH NOCHECK ADD
                    CONSTRAINT[PK_ELMAH_Error] PRIMARY KEY NONCLUSTERED([ErrorId]) ON[PRIMARY]
                    GO

                    ALTER TABLE[dbo].[ELMAH_Error]
                            ADD
                       CONSTRAINT[DF_ELMAH_Error_ErrorId] DEFAULT(NEWID()) FOR[ErrorId]
                    GO

                    CREATE NONCLUSTERED INDEX[IX_ELMAH_Error_App_Time_Seq] ON[dbo].[ELMAH_Error] 
                    (
                        [Application]
                            ASC,
                        [TimeUtc]
                            DESC,
                        [Sequence]
                            DESC
                    ) 
                    ON[PRIMARY]
                    GO

                    /* ------------------------------------------------------------------------ 
                            STORED PROCEDURES                                                      
                       ------------------------------------------------------------------------ */

                    SET QUOTED_IDENTIFIER ON
                    GO
                    SET ANSI_NULLS ON
                    GO

                    CREATE PROCEDURE[dbo].[ELMAH_GetErrorXml]
                    (
                        @Application NVARCHAR(60),
                        @ErrorId UNIQUEIDENTIFIER
                    )
                    AS

                        SET NOCOUNT ON

                        SELECT
                            [AllXml]
                        FROM
                            [ELMAH_Error]
                        WHERE
                            [ErrorId] = @ErrorId
                        AND
                            [Application] = @Application

                    GO
                    SET QUOTED_IDENTIFIER OFF
                    GO
                    SET ANSI_NULLS ON
                    GO

                    SET QUOTED_IDENTIFIER ON
                    GO
                    SET ANSI_NULLS ON
                    GO

                    CREATE PROCEDURE[dbo].[ELMAH_GetErrorsXml]
                    (
                        @Application NVARCHAR(60),
                        @PageIndex INT = 0,
                        @PageSize INT = 15,
                        @TotalCount INT OUTPUT
                    )
                    AS

                        SET NOCOUNT ON

                        DECLARE @FirstTimeUTC DATETIME
                        DECLARE @FirstSequence INT
                        DECLARE @StartRow INT
                        DECLARE @StartRowIndex INT

                        SELECT
                            @TotalCount = COUNT(1)
                        FROM
                            [ELMAH_Error]
                        WHERE
                            [Application] = @Application

                        -- Get the ID of the first error for the requested page

                        SET @StartRowIndex = @PageIndex* @PageSize + 1

                        IF @StartRowIndex <= @TotalCount
                        BEGIN

                            SET ROWCOUNT @StartRowIndex

                            SELECT
                                @FirstTimeUTC = [TimeUtc],
                                @FirstSequence = [Sequence]
                            FROM
                                [ELMAH_Error]
                            WHERE
                                [Application] = @Application
                            ORDER BY
                                [TimeUtc] DESC, 
                                [Sequence]
                            DESC

                        END
                        ELSE
                        BEGIN

                            SET @PageSize = 0

                        END

                        -- Now set the row count to the requested page size and get
                        -- all records below it for the pertaining application.

                        SET ROWCOUNT @PageSize

                        SELECT
                            errorId     = [ErrorId], 
                            application = [Application],
                            host        = [Host], 
                            type        = [Type],
                            source      = [Source],
                            message     = [Message],
                            [user]      = [User],
                            statusCode  = [StatusCode], 
                            time        = CONVERT(VARCHAR(50), [TimeUtc], 126) + 'Z'
                        FROM
                            [ELMAH_Error] error
                        WHERE
                            [Application] = @Application
                        AND
                            [TimeUtc] <= @FirstTimeUTC
                        AND
                            [Sequence] <= @FirstSequence
                        ORDER BY
                            [TimeUtc] DESC,
                            [Sequence] DESC
                        FOR
                            XML AUTO

                    GO
                    SET QUOTED_IDENTIFIER OFF
                    GO
                    SET ANSI_NULLS ON
                    GO

                    SET QUOTED_IDENTIFIER ON
                    GO
                    SET ANSI_NULLS ON
                    GO

                    CREATE PROCEDURE[dbo].[ELMAH_LogError]
                    (
                        @ErrorId UNIQUEIDENTIFIER,
                        @Application NVARCHAR(60),
                        @Host NVARCHAR(30),
                        @Type NVARCHAR(100),
                        @Source NVARCHAR(60),
                        @Message NVARCHAR(500),
                        @User NVARCHAR(50),
                        @AllXml NTEXT,
                        @StatusCode INT,
                        @TimeUtc DATETIME
                    )
                    AS

                        SET NOCOUNT ON

                        INSERT
                        INTO
                            [ELMAH_Error]
                            (
                                [ErrorId],
                                [Application],
                                [Host],
                                [Type],
                                [Source],
                                [Message],
                                [User],
                                [AllXml],
                                [StatusCode],
                                [TimeUtc]
                            )
                        VALUES
                            (
                                @ErrorId,
                                @Application,
                                @Host,
                                @Type,
                                @Source,
                                @Message,
                                @User,
                                @AllXml,
                                @StatusCode,
                                @TimeUtc
                            )

                    GO
                    SET QUOTED_IDENTIFIER OFF
                    GO
                    SET ANSI_NULLS ON
                    GO
                ");
        }

        public override void Down()
        {
        }
    }
}
