call "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\Tools\VsDevCmd.bat"
msbuild .\metatable\vs2019\hkg-metatable.sln /t:Rebuild /p:Configuration=Debug
msbuild .\metatable\vs2019\hkg-collector.sln /t:Rebuild /p:Configuration=Debug
msbuild .\metatable\vs2019\hkg-builder.sln /t:Rebuild /p:Configuration=Debug

