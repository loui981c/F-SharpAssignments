Først laves bibliotekket img_util.dll ved at køre kommandoen:

$ fsharpc --nologo -I "c:/Program Files (x86)/Mono/lib/gtk-sharp-2.0" -r gdk-sharp.dll -r gtk-sharp.dll -a img_util.fsi img_util.fs 

Derefter bruges bibliotekket i .fsx filen ved at skrive #r "img_util.dll" og så kan der kaldes funktioner fra .fs og .fsi filen ved brug af module ImgUtil. 

For at compile 8i0.fsx med fsharpc og bibliotekket, så bruges kommandoen:

$ fsharpc --nologo -r img_util.dll 8i0.fsx

Det skaber en 8i0.exe fil, som køres i mono med kommmandoen:

$ mono spiral.exe