call "%~dp0Cleanup.cmd"

robocopy "%~dp0..\DeveSolToSharp\bin\Release\netstandard2.1" "%~dp0Output" /E /xf *.pdb

"%~dp0\7z_x64_1805\7za.exe" a -mm=Deflate -mfb=258 -mpass=15 "%~dp0DeveSolToSharp.zip" "%~dp0Output\*"
"%~dp0\7z_x64_1805\7za.exe" a -t7z -m0=LZMA2 -mmt=on -mx9 -md=1536m -mfb=273 -ms=on -mqs=on -sccUTF-8 "%~dp0DeveSolToSharp.7z" "%~dp0Output\*"