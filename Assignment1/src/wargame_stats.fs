module stats
open game
open shuffle

/// <summary>it works a lot like a game in wargame_game but here it adds 1 to the gambit, which is how many rounds the game has been through</summary>
/// <param name="p1">player 1</param>
/// <param name="p2">player 2</param>
/// <param name="pool">the cards on the table</param>
/// <param name="gambit">the number of rounds the game has had</param>
/// <returns>either 0, 1, 2 in a tuple with the gambit</returns>
let rec game p1 p2 pool gambit = 
    //matches the players first card against each other
    match (getCard p1, getCard p2) with 
        //it ends in tie
        | (None, None) -> (0, gambit) 
        //player 1 wins
        | (Some _, None) -> (1, gambit) 
        //player 2 wins
        | (None, Some _) -> (2, gambit) 
        //If both players have cards in their decks, it checks their fst card of the deck against each other. If player 1s card
        //is bigger than player 2s card, then player 1 get the card added to their deck and the game is played again. Same goes for player 2. 
        //If the cards are tied, then there is war and the cards are added to the pool until one of the players win the round. 1 gets added
        //to the gambit no matter the event
        | (Some (card1, new_p1), Some (card2, new_p2)) -> 
            if card1 > card2 then 
                game (addCards new_p1 [card1; card2] @pool) new_p2 [] (gambit+1)
            elif card1 < card2 then
                game new_p1 (addCards new_p2 ([card2; card1] @pool)) [] (gambit+1)
            else game new_p1 new_p2 (pool@[card1; card2]) (gambit+1)


/// <summary>runs the game</summary>
/// <param name="l">list of tuples, (winner, gambit)</param>
/// <param name="n">number of games that shall be ran</param>
/// <returns>a tuple with the winner of the game and gambit added 1</returns>
let rec runGames (l:(int * int) list) (n:int) : (int * int) list =
    if n = 1 then
        let (winner, gambit) = (game player1 player2 pool 0)
        ((winner, gambit)::l)
    //if the number of games that shall be played is higher than 1, then it runGames and adding 1 to the gambit each time until n=1
    else
        let (winner, gambit) = (game player1 player2 pool 0) 
        runGames ((winner, gambit)::l) (n-1)
 
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
Antal gange der blev krig: %A
Antal gennesnitlige udspil pr. spil = %A " aLotOfGames (List.unzip aLotOfGames) (fst ((List.unzip aLotOfGames))) player1 player2 tied averagePlay

