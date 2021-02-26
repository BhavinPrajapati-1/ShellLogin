FOR JWT AUthentication install this pacakges =>

Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Microsoft.IdentityModel.Tokens
System.IdentityModel.Tokens.Jwt
Microsoft.AspNetCore.Authentication.JwtBearer

=======================================================


ADD CONTEXT 
in .NET cli
dotnet user-secrets set ConnectionStrings.JWTTokenAuth "Data Source=BHAVIN\\SQLEXPRESS;Initial Catalog=JWTTokenBasedAuth;Integrated Security=True"
dotnet ef dbcontext scaffold Name=ConnectionStrings.JWTTokenAuth Microsoft.EntityFrameworkCore.SqlServer

OR 
PM CONSOLE :
Scaffold-DbContext "Data Source=BHAVIN\\SQLEXPRESS;Initial Catalog=JWTTokenBasedAuth;Integrated Security=True" Microsoft.EntityFrameworkCore.SqlServer -ContextDir DataContext -Context DemoTokenContext -OutputDir DataContext -Force 