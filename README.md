# asp.net-core-7.0-google-auth-bug
Reporting a bug in ASP.NET Core in .NET 7 with the Google authentication library

[Link to opened issue](https://github.com/dotnet/aspnetcore/issues/46743)

**.NET Version 7.0.103**

## The Issue

ASP.NET Core has an issue with the Google authentication library on startup, where it seems to run into an endless loop which results in the following stack repeating itself until eventually exiting with error coded `-1073741819`:
```
at System.Runtime.CompilerServices.AsyncMethodBuilderCore.Start[[Microsoft.AspNetCore.Authentication.AuthenticationService+<AuthenticateAsync>d__14, Microsoft.AspNetCore.Authentication.Core, Version=7.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60]](<AuthenticateAsync>d__14 ByRef)
at System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1[[System.__Canon, System.Private.CoreLib, Version=7.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]].Start[[Microsoft.AspNetCore.Authentication.AuthenticationService+<AuthenticateAsync>d__14, Microsoft.AspNetCore.Authentication.Core, Version=7.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60]](<AuthenticateAsync>d__14 ByRef)
at Microsoft.AspNetCore.Authentication.AuthenticationService.AuthenticateAsync(Microsoft.AspNetCore.Http.HttpContext, System.String)
at Microsoft.AspNetCore.Authentication.RemoteAuthenticationHandler`1+<HandleAuthenticateAsync>d__13[[System.__Canon, System.Private.CoreLib, Version=7.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]].MoveNext()
at System.Runtime.CompilerServices.AsyncMethodBuilderCore.Start[[Microsoft.AspNetCore.Authentication.RemoteAuthenticationHandler`1+<HandleAuthenticateAsync>d__13[[System.__Canon, System.Private.CoreLib, Version=7.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], Microsoft.AspNetCore.Authentication, Version=7.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60]](<HandleAuthenticateAsync>d__13<System.__Canon> ByRef)
at System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1[[System.__Canon, System.Private.CoreLib, Version=7.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]].Start[[Microsoft.AspNetCore.Authentication.RemoteAuthenticationHandler`1+<HandleAuthenticateAsync>d__13[[System.__Canon, System.Private.CoreLib, Version=7.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], Microsoft.AspNetCore.Authentication, Version=7.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60]](<HandleAuthenticateAsync>d__13<System.__Canon> ByRef)
at Microsoft.AspNetCore.Authentication.RemoteAuthenticationHandler`1[[System.__Canon, System.Private.CoreLib, Version=7.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]].HandleAuthenticateAsync()
at Microsoft.AspNetCore.Authentication.AuthenticationHandler`1+<AuthenticateAsync>d__48[[System.__Canon, System.Private.CoreLib, Version=7.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]].MoveNext()
at System.Runtime.CompilerServices.AsyncMethodBuilderCore.Start[[Microsoft.AspNetCore.Authentication.AuthenticationHandler`1+<AuthenticateAsync>d__48[[System.__Canon, System.Private.CoreLib, Version=7.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], Microsoft.AspNetCore.Authentication, Version=7.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60]](<AuthenticateAsync>d__48<System.__Canon> ByRef)
at System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1[[System.__Canon, System.Private.CoreLib, Version=7.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]].Start[[Microsoft.AspNetCore.Authentication.AuthenticationHandler`1+<AuthenticateAsync>d__48[[System.__Canon, System.Private.CoreLib, Version=7.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], Microsoft.AspNetCore.Authentication, Version=7.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60]](<AuthenticateAsync>d__48<System.__Canon> ByRef)
at Microsoft.AspNetCore.Authentication.AuthenticationHandler`1[[System.__Canon, System.Private.CoreLib, Version=7.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]].AuthenticateAsync()
at Microsoft.AspNetCore.Authentication.AuthenticationService+<AuthenticateAsync>d__14.MoveNext()
```

The issue is tied to the **projects** framework, not the libraries, as setting the libraries version to below 7.x but keeping the projects framework at 7 cause the issue still.


## Expected Behavior

A raised exception if an error has occurred, otherwise not crashing.


## Steps To Reproduce

- Create a new ASP.NET Core Web App project, with its framework set to .NET 7.0 (can create the project with a different version and change it to 7.0 later)
- Install `Microsoft.AspNetCore.Authentication.Google` NuGet package
- Configure authentication to use the Google provider, with valid `ClientId` and `ClientSecret` (not necessarily connected to an actual client, the value just needs to be valid)
- Run the project

## `dotnet --info`
```
.NET SDK:
	Version:   7.0.103
	Commit:    276c71d299

Runtime Environment:
	OS Name:     Windows
	OS Version:  10.0.19043
	OS Platform: Windows
	RID:         win10-x64
	Base Path:   C:\Program Files\dotnet\sdk\7.0.103\

Host:
	Version:      7.0.3
	Architecture: x64
	Commit:       0a2bda10e8

.NET SDKs installed:
	3.0.103 [C:\Program Files\dotnet\sdk]
	3.1.422 [C:\Program Files\dotnet\sdk]
	7.0.103 [C:\Program Files\dotnet\sdk]

.NET runtimes installed:
	Microsoft.AspNetCore.App 3.0.3 [C:\Program Files\dotnet\shared\Microsoft.AspNetCore.App]
	Microsoft.AspNetCore.App 3.1.28 [C:\Program Files\dotnet\shared\Microsoft.AspNetCore.App]
	Microsoft.AspNetCore.App 6.0.14 [C:\Program Files\dotnet\shared\Microsoft.AspNetCore.App]
	Microsoft.AspNetCore.App 7.0.3 [C:\Program Files\dotnet\shared\Microsoft.AspNetCore.App]
	Microsoft.NETCore.App 3.0.3 [C:\Program Files\dotnet\shared\Microsoft.NETCore.App]
	Microsoft.NETCore.App 3.1.28 [C:\Program Files\dotnet\shared\Microsoft.NETCore.App]
	Microsoft.NETCore.App 6.0.14 [C:\Program Files\dotnet\shared\Microsoft.NETCore.App]
	Microsoft.NETCore.App 7.0.3 [C:\Program Files\dotnet\shared\Microsoft.NETCore.App]
	Microsoft.WindowsDesktop.App 3.0.3 [C:\Program Files\dotnet\shared\Microsoft.WindowsDesktop.App]
	Microsoft.WindowsDesktop.App 3.1.28 [C:\Program Files\dotnet\shared\Microsoft.WindowsDesktop.App]
	Microsoft.WindowsDesktop.App 6.0.14 [C:\Program Files\dotnet\shared\Microsoft.WindowsDesktop.App]
	Microsoft.WindowsDesktop.App 7.0.3 [C:\Program Files\dotnet\shared\Microsoft.WindowsDesktop.App]

Other architectures found:
	x86   [C:\Program Files (x86)\dotnet]
		registered at [HKLM\SOFTWARE\dotnet\Setup\InstalledVersions\x86\InstallLocation]

Environment variables:
	Not set

global.json file:
	Not found
```
