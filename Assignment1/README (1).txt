Koden oversættes og køres på følgende måde:

1.Åbn file i terminal
[Fx. cd Desktop/7g/src]


2.Dan biblioteker af .fs file.
[fsharpc -a wargame-shuffle.fs]
[fsharpc -a wargame-shuffle.fs wargame_game.fs]
[fsharpc -a wargame-shuffle.fs wargame_game.fs wargame_stats.fs]
Nu vil der lave en ny fil, .dll af alle 3 filer, som er kodes bibliotek.

3.Oversæt den applications fil med biblioteket.
[Fx. fsharpc -r wargame-shuffle.dll -r wargame_game.dll -r wargame_stats.dll Blackboxtest-forall-1.fsx]
Nu er der en eksekverbar fil, Blackboxtest-forall-1.exe.

4.Kør med mono
[Fx. mono Blackboxtest-forall-1.exe]
Nu kommer resultat frem.

5.Man kan eventuel se programmets runtime
[Fx. time mono Blackboxtest-forall-1.exe]
