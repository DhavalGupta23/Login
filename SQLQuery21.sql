CREATE PROCEDURE [dbo].[email_exist]
	@Email VARCHAR(20)

AS
Begin     
     select *    
    from tblEmployee WHERE Email LIKE '%or%';   

End