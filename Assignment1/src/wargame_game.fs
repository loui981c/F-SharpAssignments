module game
open shuffle 
#indent "off"

//defining types
type player = int list 
type card = int 
type deck = int list 
type pool = int list

let pool = []

/// <summary>makes 2 equally big card decks as a tuple</summary>
/// <param name="">takes no arguments</param>
/// <returns>2 card stacks<returns>
let dealForGame = (deal (newdeck()))
//sets player 1s deck as the first element in the tuple
let player1 = fst dealForGame
//sets player 2s deck as the second element in the tuple
let player2 = snd dealForGame

/// <summary>draws the first card from the pool</summary>
/// <param name="p">player in the game</param>
/// <returns>Option with the first card in the deck as the first element and the rest of the deck as the second element in the tuple</param>
let getCard (p: player) : option<(card * player)> =
    match p with 
      | [] -> None 
      | x :: tail -> Some (x, tail)

//sets the 1st card for player1 and player2
let player1Card = (getCard player1)
let player2Card = (getCard player2)

/// <summary>adds the cards drawn from the deck to the players deck shuffled</summary>
/// <param name="p">the deck the player has</param>
/// <param name="d">deck of cards</param>
/// <returns>players deck with the added cards and shuffled</returns> 
let addCards (p:player) (d:deck) : player =
    p @ (shuffle d)

/// <summary>takes 2 players and the pool. Checks the players deck against each other. If both players have zero cards in their decks then it's a tie. If player 1 wins, it
/// returns 1 and 2 if player 2 wins, it returns 2. If both players have cards in their decks, it checks their fst card of the deck against each other. If player 1s card
///	is bigger than player 2s card, then player 1 get the card added to their deck and the game is played again. Same goes for player 2. If the cards are tied, then there is 
///	war and the cards are added to the pool until one of the players win the round</summary>
/// <param name="p1">the deck of player 1</param>
/// <param name="p2">the deck of player 2</param>
/// <param name="pool">an empty list where cards can be stored</param>
/// <returns>either 0, 1 or 2 when the end in tied, player 1 wins or player 2 wins</returns>
let rec game p1 p2 pool = 
	//calling getCard to match the 1st card of the players decks
    match (getCard p1, getCard p2) with 
    	//the game ends in a tie
        | (None, None) -> 0 
        //the game ends with player 1 wining
        | (Some _, None) -> 1 
        //the game ends with player 2 wining
        | (None, Some _) -> 2 
        | (Some (card1, new_p1), Some (card2, new_p2)) -> 
            if card1 > card2 then 
                game (addCards new_p1 [card1; card2] @pool) new_p2 []
            elif card1 < card2 then
                game new_p1 (addCards new_p2 ([card2; card1] @pool)) []
            else game new_p1 new_p2 (pool@[card1; card2])