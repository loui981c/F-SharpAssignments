Koden oversættes og køres på følgende måde:

1.Åbn file i Terminal
[Fx. cd Desktop/9g/src]

2.Dan bibliotek af .fs file.
[Fx. fsharpc -a readNWrite.fs]
Nu vil der lave en ny fil, readNWrite.dll, som er kodes bibliotek.

3.Oversæt den applications fil med biblioteket.
3a.[Fx. fsharpc -r readNWrite.dll cat.fsx]
Nu er der en eksekverbar fil, cat.exe.
3b.[Fx. fsharpc -r readNWrite.dll tac.fsx]
Nu er der en eksekverbar fil, tac.exe.

4.Oversæt fil med fsharp.
[Fx. fsharpc countLinks.fsx]
Nu er der en eksekverbar fil, countLinks.fsx.

5.Kør med mono
5a.[Fx. mono cat.exe]
5b.[Fx. mono tac.exe]
5c.[Fx. mono countLinks.exe]
Nu kommer resultat frem.





