Create procedure spUpdateEmployee      
(      
   @EmpId INTEGER ,  
   @Name VARCHAR(20),
   @Email VARCHAR(20),
   @Password VARCHAR(20),
   @Mobile NVARCHAR(10),
   @City VARCHAR(20),    
   @Department VARCHAR(20),    
   @Gender VARCHAR(6)    
)      
as      
begin      
   Update tblEmployee       
   set Name=@Name,  
   Email=@Email,
   Password=@Password,
   Mobile=@Mobile,
   City=@City,      
   Department=@Department,    
   Gender=@Gender      
   where EmployeeId=@EmpId      
End