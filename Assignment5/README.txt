Koden oversættes og køres på følgende måde:

1.Åbn file i terminal
[Fx. cd Desktop/11g/src]


2.Dan biblioteker af .fs fil.
[fsharpc -a roguelike.fs]
Nu vil der lave en ny fil, .dll, som er kodens bibliotek.

3.Oversæt den applications fil med biblioteket.
[Fx. fsharpc -r roguelike.dll roguelike-game.fsx]
Nu er der en eksekverbar fil, roguelike-game.exe.

4.Kør med mono
[Fx. mono roguelike-game.exe]
Nu kommer spillet frem.

5.Man kan eventuel se programmets runtime
[Fx. time mono roguelike-game.exe]