Create procedure spAddEmployee     
(    
    @Name VARCHAR(20),
	@Email VARCHAR(20),
	@Password VARCHAR(20),
	@Mobile NVARCHAR(10),
    @City VARCHAR(20),    
    @Department VARCHAR(20),    
    @Gender VARCHAR(6)    
)    
as     
Begin     
    Insert into tblEmployee (Name,Email,Password,Mobile,City,Department, Gender)     
    Values (@Name,@Email,@Password,@Mobile,@City,@Department, @Gender)     
End