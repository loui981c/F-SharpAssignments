/// <summary>Dette er besvarelsen til 8i0</summary>
#r "img_util.dll"

type point = int * int

type color =  ImgUtil.color

type figure = Circle of point * int * color | Rectangle of point * point * color | Mix of figure * figure


//8i0 
/// <param name="figTest">Funktionen outputter en "Mix" figur ved at bruge "Circle" og "Rectangle" i typen figure. Circle indputter point,
/// som er der, hvor cirklens centrum bliver plottet (int*int). Indputter også int, som er cirklens radius, og color, som er cirklens 
/// farve. Farven fås i typen color som ImgUtil.color, som er en funktion taget fra img_util bibliotekket. Mix inputter 2 figure og
/// cirklen og rektanglen er inputtet, og dermed bliver outputtet de 2 figurer.</param>
/// <returns>Funktionen returnere mix i typen figure, som består af en rød cirkel og en blå rektangel</returns>
//val figTest : figure
let figTest : figure =
    let redCircle = Circle ((50,50), (45), (ImgUtil.red)) 
    let blueRectangle = Rectangle ((40,40),(90,110),(ImgUtil.blue))
    let bothFigures = Mix (redCircle, blueRectangle)
    bothFigures
 

//8i1
//colorAt er en funktion taget fra opgavebeskrivelsen 
let rec colorAt (x,y) figure =
  match figure with
    | Circle ((cx,cy),r,col) ->
      if (x-cx)*(x-cx) + (y-cy)*(y-cy) <= r*r
      // uses Pythagoras' equation to determine
      // distance to center
      then Some col else None
    | Rectangle ((x0,y0),(x1,y1),col) ->
      if x0 <= x && x <= x1 && y0 <= y && y <= y1
      // within corners
      then Some col else None
    | Mix (f1,f2) ->
          match (colorAt (x,y) f1, colorAt (x,y) f2) with
            | (None, c) -> c // no overlap
            | (c, None) -> c // no overlap
            | (Some c1, Some c2) ->
              let (a1, r1, g1, b1) = ImgUtil.fromColor c1
              let (a2 ,r2, g2, b2) = ImgUtil.fromColor c2
              in Some (ImgUtil.fromArgb ((a1+a2)/2 , (r1+r2)/2,  // calculate
                                         (g1+g2)/2 , (b1+b2)/2)) // average color

/// <param name="makePicture">Funktionen indputter string, figure, int og int. Stringen bliver navnet på filen og bruges i 
/// ImgUtil.toPngFile funktionen fra bibliotekket. Funktionen laver et canvas ved at inputte int w (bredde) og int h (højde), i funktionen
/// ImgUtil.mk, som bestemmer canvas's størrelse. For at plotte figuren, som er et af inputene i funktionen, så skal der plotte pixels ind
/// på canvaset. For at plotte på hele canvaset, så laves der et for-loop inde i et, hvor for alle pixels fra x=0 til bredden w minus 1,
/// bliver løbet igennem i første loop. I andet loop løbes højden igennem i stedet for med samme metode. Det der sker i et hvert loop er,
/// hvis funktionen colorAt finder et punkt på canvas uden for figuren, så bruges ImgUtil.setPixel funktionen til at farve det punkt gråt.
/// Hvis funktionen colorAt finder et punkt på canvas indenfor enten cirklen, rektanglen eller mix, så farves der på canvas i den 
/// henholdsvis rød, blå eller gennemsnit farven mellem rød og blå. Da colorAt returnere en color option, så skal Value bruges til at ændre
/// det til den farve, som ImgUtil.setPixel inputter. Loopen er færdig, når alle punkter på canvaset er tjekket. Derefter benyttes 
/// ImgUtil.toPngFile funktionen til at lave en .png fil ved at inputte string (et navn på filen) og et canvas.</param>
/// <returns>Funktionen outputter png fil</returns>
//val makePicture : string -> figure -> int -> int -> unit
let makePicture (s:string) (f:figure) (w:int) (h:int) : unit =
    let C = ImgUtil.mk w h
    for x=0 to (w-1) do
        for y=0 to (h-1) do
            if (colorAt (x,y) f)=None then ImgUtil.setPixel (ImgUtil.fromRgb (128,128,128)) (x,y) C
            else ImgUtil.setPixel (colorAt (x,y) f).Value (x,y) C 
    do ImgUtil.toPngFile s C

//8i2
//Dette er et kald på funktionen makePicture, som skaber en .png fil, når der complieres i terminalen
do (makePicture ("figTest.png") (figTest) (100) (150))

//8i3
/// <param name="checkFigure">Funktionen inputter en figur, hvor den tjekker, ved brug af pattern matching, om figuren er en cirkel eller
/// en rektangel. Hvis figuren er en cirkel, så returnere funktionen true, hvis radius er større end 0 ellers returnere den false. Hvis
/// figuren er en rektangel, så returnere funktionen true, hvis koordinaterne i det andet koordinatsæt er større eller lig med 
/// koordinaterne i det første koordinatsæt, ellers returnere den false.</param>
/// <returns>Funktionen returnere et bool-udtryk</returns>
//val checkFigure : figure -> bool
let checkFigure (f:figure) : bool =
    match f with
        | Circle ((cx,cy),r,col) ->
          if r>0 then true 
          else false
        | Rectangle ((x0,y0),(x1,y1),col) ->
          if x1 >= x0 && y1 >= y0 then true 
          else false

//Eksempel
printfn "checkFigure: %b" (checkFigure (Rectangle ((90,110),(40,40),(ImgUtil.blue)))) 


//8i4
/// <param name="move">Funktionen er rekrusiv og inputter en figur og en vektor, som er int * int. Figuren tjekker først, ved pattern 
/// matching, om figuren er typen Mix. Hvis den er, så køres funktionen på begge figure i mix samt vektoren indtil der ikke er flere mix. 
/// Derefter tjekker funktionen om figuren indeholder en cirkel, hvor cirklens centrum koordinater vil blive lagt sammen med vektorens 
/// koordinater, som funktionen så returnere. Hvis figuren indeholder et rektangel, så vil funktionen returnere rektanglen, hvor alle 
/// første koordinater er blevet lagt sammen med vektorens første koordinat, og det med anden koordinaterne.</param> 
/// <returns>Funktionen returnere en ny figur, som er blevet rykket på canvas</returns>
//val move : figure -> int * int -> figure
let rec move (f:figure) (v:int*int) : figure =
    match f with 
        | Mix (f1,f2) ->
          Mix (move f1 v, move f2 v)
        | Circle ((cx,cy),r,col) ->
          Circle ((cx+fst(v),cy+snd(v)),r,col)
        | Rectangle ((x0,y0),(x1,y1),col) ->
          Rectangle((x0+fst(v),x1+fst(v)),(y0+snd(v),y1+snd(v)),col)

makePicture "moveTest.png" (move figTest (-20,20)) 100 150

//8i5
/// <param name="boundingBox">Funktionen er rekrusiv og inputter en figur, hvor den pattern matcher om det er en cirkel, rektangel eller et mix. Hvis
/// det er en cirkel, så trækkes der radius fra både x,y koordinatet, som laver et mindste punkt for en boks, og så lægges der radius
/// til for x,y koordinatet, som skaber et størreste punkt for en boks rundt om figuren. Hvis inputtet er en rektangel, så returneres 
/// der de samme punkter som der inputtes. Hvis inputtet er et mix, så defineres resultatet af boundingBox på figur 1 (f1) som en tuple 
/// (point1,point2), og resultatet af boundingBox på figur 2 (f2) som tuple (point3,point4). Der findes mininumspunktet ved at tage
/// mininum af de mindste x,y koordinater, som er point1 og point3, ved at bruge funktionen min på første elementerne, som er x, i point1
/// og point3. Det samme metode bruges på andet elementerne, som er y, i point1 og point3. Dermed er mindstepunktet og første del af tuplen
/// fundet. Der gøres det præcis det samme for at finde max, hvor de eneste forskelle er, at der bruges funktion max på point2 og point4.
/// Funktion outputter til sidst et tuplesæt med mindstepunktet og størstepunktet for figuren.</param>
/// <returns>Funktion returnere et tuplesæt med 2 tupler.</returns>
//val boundingBox : figure -> point * point
let rec boundingBox (f:figure) : point * point =
    match f with
        | Circle ((cx,cy),r,col) ->
            (cx-r,cy-r),(cx+r,cy+r)
        | Rectangle ((x0,y0),(x1,y1),col) ->
            (x0,y0),(x1,y1)
        | Mix (f1,f2) ->
            let point1,point2 = boundingBox f1
            let point3, point4 = boundingBox f2
            (min (fst(point1)) (fst(point3)), min (snd(point1)) (snd(point3))), (max (fst(point2))(fst(point4)),max (snd(point2))(snd(point4)))

printfn "boundingBox %A" (boundingBox (figTest))