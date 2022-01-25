Først laves bibliotekket simulate.dll ved at køre kommandoen:

$ fsharpc --nologo -a simulate.fs

Derefter bruges bibliotekket i .fsx filen ved at skrive #r "simulate.dll" og så kan der kaldes funktioner fra .fs filen ved brug af module simulate. 

For at compile testSimulate.fsx med fsharpc og bibliotekket, så bruges kommandoen:

$ fsharpc --nologo -r simulate.dll testSimulate.fsx

Det skaber en testSimulate.exe fil, som køres i mono med kommmandoen:

$ mono testSimulate.exe