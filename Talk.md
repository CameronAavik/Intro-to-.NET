1. Present `Intro to DotNet.pdf`
2. Show how to create a new hello world console application
```
dotnet -h
dotnet new -l
dotnet new console -n HelloWorld
cd .\HelloWorld\
dotnet run
cd ..
dotnet run -p .\HelloWorld\
```
3. Show bin directory is where the output is saved and how to run it
```
dotnet .\HelloWorld\bin\Debug\netcoreapp2.0\HelloWorld.dll
```
4. Walk through HashMap implementation
5. Walk through OtherFeatures if there is time