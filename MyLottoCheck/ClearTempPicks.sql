Select * From MyLottoCheck.CaliforniaMegaMillionUserPicks

Delete  MyLottoCheck.CaliforniaMegaMillionUserPicks
Where   UserId Not In ( Select  Id
                        From    dbo.AspNetUsers )
        And DateDiff(Minute,DateCreated,GetDate()) > 2;

Select * From MyLottoCheck.CaliforniaMegaMillionUserPicks
