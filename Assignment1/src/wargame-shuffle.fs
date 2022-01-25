module shuffle
open System


type card = int
type deck = card list

/// <summary>deals the deck of cards by using the help function skip</summary>
/// <param name="d">it is the deck of cards</param>
/// <returns>a tuple made of int lists which is the two stacks of cards</returns>
let deal (d: deck): deck * deck =
    let rec skip (d1: deck) (d2: deck) (d3: deck) : deck * deck =
        match d1 with
            | [] -> (d2,d3)
            | c1 :: c2 :: cs -> skip cs (c1 :: d2) (c2 :: d3)
            | c1 :: cs -> skip cs (c1 :: d2) d3
    skip d [] []
 

//help function to shuffle. It randomises the cards in the deck
let rand = let rnd = System.Random() in fun n -> rnd.Next(0,n)

/// <summary>shuffles a deck of cards</summary>
/// <param name="x">deck of cards</param>
/// <returns>returns the 1st element of the tuple, which is the cards shuffled</returns>
let shuffle (x : deck) : deck =
    //takes the deck, makes a new list of randomised numbers and then multiplies the length of the list by a big number
    let list = List.init (x.Length) (fun r -> rand (x.Length * 120))
    //collecting the deck and the list of randomised number in a tuple
    let list2 = List.zip (x) (list) 
    //sorts the numbers in list2 by comparing the numbers on the second place in the tuple so the input x is being shuffled. Then it unzips
    //the list there is just 1 tuple with the card numbers on place 1 and the high numbers on place 2 
    let list3 = List.sortWith (fun x y -> compare (snd x) (snd y)) list2 |> List.unzip  
    fst list3


/// <summary>makes a new deck that is shuffled</summary>
/// <param name="">takes no argument</param>
/// <returns>a shuffled deck</returns>
let newdeck () = 
    //makes a suit in the deck by adding 13 cards from 2 to 14
    let perSerie = List.init 13 (fun i -> i+2)
    //makes all 4 suits in the deck by using list collect on perSerie. Then all the cards are shuffled
    List.collect (fun elm -> [elm;elm;elm;elm]) perSerie |> shuffle 

