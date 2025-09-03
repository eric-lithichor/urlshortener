## Caveats

I'm not a C# developer, so I don't know all the conventions for this language - 
both times I worked as a developer I worked in Java, and when I worked with C# I 
wrote full stack test frameworks (which have different conventions)

I'm working on a Mac laptop, not Windows. I don't know how portable C# code is 
between OSs, but I used to have problems with Java when submitting code samples

I used VS Code for this project, and ran it in the terminal - in case that 
makes a difference:
- dotnet build
- dotnet UrlShortener/bin/Debug/net9.0/UrlShortener.dll

I didn't use dependency injection because it adds unnecessary complexity for a 
POC that doesn't have unit tests. If I was going to use them, I'd use non-static 
classes and constructors that require the database helper