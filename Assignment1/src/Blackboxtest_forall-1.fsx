#r "wargame_stats.dll"
#r "wargame_game.dll"
#r "wargame-shuffle.dll"
open stats
open shuffle
open game

//Blackbox tests                  

// Blackbox: deal-funktion 
printfn "Blackbox test af deal-funktion:"  
printfn ""

// deal test 1 
let listeTest1 = [1 ; 2 ; 1 ; 2]
let expectedList1 = ([1 ; 1], [2 ; 2])
printfn "deal-funktion test 1 = %b" ((deal(listeTest1)) = expectedList1)

// deal test 2
let listeTest2 = [2 ; 3 ; 2 ; 3]
let expectedList2 = ([2 ; 2], [3 ; 3])
printfn "deal-funktion test 2 = %b" ((deal(listeTest2)) = expectedList2)

// deal test 3
let listeTest3 = [1 ; 2 ; 3 ; 2 ; 3]
let expectedList3 = ([3 ; 3 ; 1], [2 ; 2])
printfn "deal-funktion test 3 = %b" ((deal(listeTest3)) = expectedList3)

// deal test 4
let listeTest4 = [1 ; 0 ; 80000000 ; 0]
let expectedList4 = ([80000000 ; 1 ], [0 ; 0])
printfn "deal-funktion test 4 = %b" ((deal(listeTest4)) = expectedList4)

// deal test 5
let listeTest5 = []
let expectedList5 = ([], [])
printfn"deal-funktion test 5 - %b" ((deal(listeTest5)) = expectedList5)


printfn ""
printfn ""

//Blackbox: Shuffle-funktion
printfn "Blackbox test af Shuffle-funktion:"  
printfn ""

// Shuffle test 1 
let listeTest6 = [1 ; 2 ; 3]
let shuffleExpected1 = (shuffle(listeTest6))
printfn "Shuffle-funktion test 1 = %b " ((shuffleExpected1 = [1 ; 2 ; 3]) || (shuffleExpected1 = [1 ; 3 ; 2]) || (shuffleExpected1 = [2 ; 1 ; 3]) || (shuffleExpected1 = [2 ; 3 ; 1]) || shuffleExpected1 = [3 ; 1 ; 2] || (shuffleExpected1 = [3 ; 2 ; 1]))

// Shuffle test 2 
let listeTest7 = [1 ; 2 ]
let shuffleExpected2 = (shuffle(listeTest7))
printfn "Shuffle-funktion test 2 = %b" ((shuffleExpected2 = [1 ; 2 ]) || (shuffleExpected2 = [2 ; 1]))

// Shuffle test 3 
let listeTest8 = [5 ; 0]
let shuffleExpected3 = (shuffle(listeTest8))
printfn "Shuffle-funktion test 3 = %b" ((shuffleExpected3 = [5 ; 0 ]) || (shuffleExpected3 = [0 ; 5])) 

// Shuffle test 4 
let listeTest9 = [1 ; 1 ; 1 ; 840 ; 1 ]
let shuffleExpected4 = (shuffle(listeTest9))
printfn "Shuffle-funktion test 4 = %b"((shuffleExpected4 = [1 ; 1 ; 1 ; 840 ; 1 ]) || (shuffleExpected4 = [1 ; 1 ; 1 ; 1 ; 840 ]) || (shuffleExpected4 = [840 ; 1 ; 1 ; 1 ; 1 ]) || (shuffleExpected4 = [1 ; 840 ; 1 ; 1 ; 1 ]) || (shuffleExpected4 = [1 ; 1 ; 840 ; 1 ; 1 ])) 

// Shuffle test 5 
let listeTest10 = [5 ]
let shuffleExpected5 = (shuffle(listeTest10))
printfn "Shuffle-funktion test 5 = %b" ((shuffleExpected5 = [5]))

printfn ""
printfn ""

printfn "%s" "Blackbox test af dealForWar-funktionen (og newdeck-funktionen)"
// Funktion til at teste om newdeck-funktionen fungerer 
let idealDeck (t:int list) : bool = (((t)|> Seq.filter ((=)2) |> Seq.length)=4) &&
                                    (((t)|> Seq.filter ((=)3) |> Seq.length)=4) &&
                                    (((t)|> Seq.filter ((=)4) |> Seq.length)=4) &&
                                    (((t)|> Seq.filter ((=)5) |> Seq.length)=4) &&
                                    (((t)|> Seq.filter ((=)6) |> Seq.length)=4) &&
                                    (((t)|> Seq.filter ((=)7) |> Seq.length)=4) &&
                                    (((t)|> Seq.filter ((=)8) |> Seq.length)=4) &&
                                    (((t)|> Seq.filter ((=)9) |> Seq.length)=4) &&
                                    (((t)|> Seq.filter ((=)10) |> Seq.length)=4) &&
                                    (((t)|> Seq.filter ((=)11) |> Seq.length)=4) &&
                                    (((t)|> Seq.filter ((=)12) |> Seq.length)=4) &&
                                    (((t)|> Seq.filter ((=)13) |> Seq.length)=4) &&
                                    (((t)|> Seq.filter ((=)14) |> Seq.length)=4)

printfn ""

// dealForWar test 1
let decktest1 = newdeck()
let dealForWar1 = (deal(decktest1))
let dealForWar1p1 = fst dealForWar1 
let dealForWar1p2 = snd dealForWar1
printfn "dealForWar-funktion test 1 = %b" (((dealForWar1p1.Length = 26) && (dealForWar1p2.Length = 26)) && ((idealDeck decktest1)))

// dealForWar test 2
let decktest2 = newdeck()
let dealForWar2 = (deal(decktest2))
let dealForWar2p1 = fst dealForWar2
let dealForWar2p2 = snd dealForWar2
printfn "dealForWar-funktion test 2 = %b" (((dealForWar2p1.Length = 26) && (dealForWar2p2.Length = 26)) && ((idealDeck decktest2)))

// dealForWar test 3
let decktest3 = newdeck()
let dealForWar3 = (deal(decktest3))
let dealForWar3p1 = fst dealForWar3
let dealForWar3p2 = snd dealForWar3
printfn "dealForWar-funktion test 3 = %b" (((dealForWar3p1.Length = 26) && (dealForWar3p2.Length = 26)) && ((idealDeck decktest3)))

// dealForWar test 4
let decktest4 = newdeck()
let dealForWar4 = (deal(decktest4))
let dealForWar4p1 = fst dealForWar4
let dealForWar4p2 = snd dealForWar4
printfn "dealForWar-funktion test 4 = %b" (((dealForWar4p1.Length = 26) && (dealForWar4p2.Length = 26)) && ((idealDeck decktest4)))

// dealForWar test 5
let decktest5 = newdeck()
let dealForWar5 = (deal(decktest5))
let dealForWar5p1 = fst dealForWar5
let dealForWar5p2 = snd dealForWar5
printfn "dealForWar-funktion test 5 = %A" (((dealForWar5p1.Length = 26) && (dealForWar5p2.Length = 26)) && ((idealDeck decktest5)))


printfn ""
printfn ""

printfn "%s" "Blackbox test af addCards-funktionen:"
printfn ""
//addCards test1
let addCard_test1_Player = [2 ; 2 ; 2]
let addCard_test1_Deck = [1 ; 2 ;]
let addCards1 = addCards addCard_test1_Player addCard_test1_Deck
printfn "addCards-funktion test 1 = %A" ((addCards1 = [2 ; 2 ; 2 ; 1 ; 2]) || (addCards1 = [2 ; 2 ; 2 ; 2 ; 1]))

//addCards test2
let addCard_test2_Player = [0]
let addCard_test2_Deck = [301 ; 22]
let addCards2 = addCards addCard_test2_Player addCard_test2_Deck
printfn "addCards-funktion test 2 = %A" ((addCards2 = [0 ; 301 ; 22]) || (addCards2 = [0 ; 22 ; 301]))

//addCards test3
let addCard_test3_Player = [ 1 ; 14 ; 2 ; 6 ; 6 ; 9 ; 7]
let addCard_test3_Deck = [12; 6]
let addCards3 = addCards addCard_test3_Player addCard_test3_Deck
printfn "addCards-funktion test 3 = %A" ((addCards3 = [ 1 ; 14 ; 2 ; 6 ; 6 ; 9 ; 7 ; 12 ; 6]) || (addCards3 = [ 1 ; 14 ; 2 ; 6 ; 6 ; 9 ; 7 ; 6 ; 12]))

//addCards test4
let addCard_test4_Player = [ 1 ; 14 ; 2 ; 6 ; 6 ; 9 ; 7]
let addCard_test4_Deck = []
let addCards4 = addCards addCard_test4_Player addCard_test4_Deck
printfn "addCards-funktion test 4 = %A" (addCards4 = [ 1 ; 14 ; 2 ; 6 ; 6 ; 9 ; 7 ]) 

//addCards test5
let addCard_test5_Player = []
let addCard_test5_Deck = [10 ; 55 ; 1]
let addCards5 = addCards addCard_test5_Player addCard_test5_Deck
printfn "addCards-funktion test 5 = %A" ((addCards5 = [10 ; 55 ; 1]) || 
                                         (addCards5 = [10 ; 1 ; 55]) || 
                                         (addCards5 = [55 ; 1 ; 10]) || 
                                         (addCards5 = [55 ; 10 ; 1]) || 
                                         (addCards5 = [1 ; 10 ; 55]) ||
                                         (addCards5 = [1 ; 55 ; 10])) 
printfn" "


printfn "%s" "Blackbox test af getCard-funktionen:"
printfn "" 

// getCard test 1
let getCard1 = getCard [2 ; 3 ; 3 ; 3]
printfn "getCard test 1 = %A" (getCard1 = (Some (2, [3 ; 3 ; 3])))

// getCard test 2
let getCard2 = getCard [14 ; 7 ; 6 ; 8 ]
printfn "getCard test 2 = %A" (getCard2 = (Some (14, [7 ; 6 ; 8])))

// getCard test 3
let getCard3 = getCard [9 ; 2 ; 3 ; 4 ; 5 ; 3 ; 8 ; 11 ; 2 ; 14 ; 7 ; 6 ; 8 ]
printfn "getCard test 3 = %A" (getCard3 = (Some (9, [2 ; 3 ; 4 ; 5 ; 3 ; 8 ; 11 ; 2 ; 14 ; 7 ; 6 ; 8 ])))

// getCard test 4
let getCard4 = getCard []
printfn "getCard test 4 = %A" (getCard4 = None)

// getCard test 5
let getCard5 = getCard [8]
printfn "getCard test 5 = %A" (getCard5 = Some (8, []))

printfn "" 
printfn "" 

printfn "%s" "Blackbox test af game-funktionen:"
printfn "" 

// game test 1
printfn "game test 1 = %A" (game(newdeck())(newdeck())([]))


printfn "" 
printfn "" 

printfn "%s" "Blackbox test af runGames-funktionen:"
printfn "" 

// runGames test 1
let aLotOfGames = (runGames [] 10000)
let player1 = (fst ((List.unzip aLotOfGames)))|> Seq.filter ((=)1) |> Seq.length
let player2 = (fst ((List.unzip aLotOfGames)))|> Seq.filter ((=)2) |> Seq.length
let tied = (fst ((List.unzip aLotOfGames)))|> Seq.filter ((=)0) |> Seq.length
let averagePlay = ((List.fold (fun a x -> a+x) 0 (snd (List.unzip aLotOfGames)))/10000)/2


printfn "Spillet usorteret: %A 
Spillet som sorteret %A
Liste over hvem der har vundet: %A
Antal gange spiller1 vandt: %A
Antal gange spiller2 vandt: %A
Antal gange det blev uafgjort: %A
Antal gennesnitlige runder(gambit) pr. spil = %A " aLotOfGames (List.unzip aLotOfGames) (fst ((List.unzip aLotOfGames))) player1 player2 tied averagePlay