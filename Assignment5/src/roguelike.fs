module roguelike
open System

type Color = ConsoleColor
type Pixel = char * Color * Color
let setPixel = (Pixel(' ', Color.White, Color.DarkMagenta))

/// <summary>makes a canvas</summary>
/// <param name="h">an int, the number of colums</param>
/// <param name="w">an int, the number of rows</param>
/// <returns>a canvas with height h and width w</returns>
type Canvas(h:int, w:int) =
    let Rows = w
    let Cols = h

    //creates a blank canvas
    let Pixels = Array2D.create Cols Rows setPixel

    /// <summary>sets a background and foreground color and a character onto the canvas</summary>
    /// <param name="x">an int, x position on canvas</param>
    /// <param name="y">an int, y position on canvas</param>
    /// <param name="p">Pixel, which is char * Color * Color</param>
    /// <returns>unit</returns>
    member this.Set (x:int, y:int, p:Pixel) =
        let (c, fg, bg) = p
        //sets the chosen c, fg, bg onto (x,y) position onto canvas
        Pixels.[x, y] <- (c, fg, bg)

    /// <summary>shows the canvas in the terminal. Here it has the same color and character for every x and y</summary>
    /// <param name="takes no argument"></param>
    /// <returns>unit</returns>
    member this.Show () =
        //checks all y and then x in rows and cols
        for y=0 to Rows-1 do
            for x=0 to Cols-1 do
                //changes the color and characther to the setPixel value for all x and y
                let (c, fg, bg) = Pixels.[x,y]

                Console.ForegroundColor <- fg
                Console.BackgroundColor <- bg
                printf "%c" c
                //stops the for loop when x has reached the end
                if x=Cols-1 then printf "\n"

        Console.ResetColor()


/// <summary>abstract class to show items and etc on canvas by val and abstract members</summary>
/// <param name="x">an int, x position on canvas</param>
/// <param name="y">an int, x position on canvas</param>
/// <returns>nothing</returns>
[< AbstractClass >]
type Entity(x:int,y:int) =
    //makes it possible to change the look of an item with get, set
    member val Presence : Pixel = setPixel with get, set
    //makes it possible to change the position an item with get, set
    member val Pos : (int*int) = (x, y) with get, set
    //makes it possible to put item onto the canvas
    abstract member RenderOn : Canvas -> unit
    default this.RenderOn (canvas:Canvas) : unit = ()


/// <summary>creates a player</summary>
/// <param name="x">an int, x position on canvas</param>
/// <param name="y">an int, y position on canvas</param>
/// <returns>a player at position x,y</returns>
type Player (x:int,y:int) as player = 
    inherit Entity(x,y) 
    //sets the design of the player
    do player.Presence <- (Pixel('p', Color.DarkBlue, Color.Black))

    //properties
    member val IsDead = false with get, set
    member val infoEvent : string = "" with get, set
    member val hp = 10 with get, set

    //puts the player on the canvas
    override this.RenderOn (canvas:Canvas) : unit = 
        canvas.Set (fst this.Pos, snd this.Pos, this.Presence)

    //Methods

    /// <summary>deals damage to the player</summary>
    /// <param name="dmg">an int, how much damage the player takes</param>
    /// <returns>unit</returns>
    member this.Damage (dmg:int) : unit =
        //substracs damage from hitpoints 
        this.hp <- this.hp - dmg
        //tells the user how much damage the player took and how much it has left
        this.infoEvent <- sprintf "%i damage.\n HP:%i\n" dmg this.hp
        //checks if player has 0 or less in hp. 
        //If so, the players IsDead status is set to true and it tells the user the player has died
        if this.hp <= 0 then 
            this.IsDead <- true
            this.infoEvent <- this.infoEvent + sprintf "You have died"
        //else the status is the same and the game continues 
        else 
            this.IsDead <- false


    /// <summary>heals the player</summary>
    /// <param name="h">an int, how much healing the player takes</param>
    /// <returns>unit</returns>
    member this.Heal (h:int) : unit =
        //adds healing to hp
        this.hp <- this.hp + h
        //tells the user how much the player has healed and its hp
        this.infoEvent <- sprintf "You have healed %i. Your HP:%i" h this.hp

    //Removes player from canvas when moving
    member this.ResetOnCanvas (canvas:Canvas) : (unit) =
        canvas.Set (fst this.Pos, snd this.Pos, setPixel) 

    /// <summary>moves the player on the canvas</summary>
    /// <param name="nx">an int, the new x position</param>
    /// <param name="ny">an int, the new y position</param>
    /// <returns>unit</returns>
    member this.MoveTo(nx:int, ny:int) : unit = 
        //changes the position by get,set
        this.Pos <- (nx, ny)
 

/// <summary>abstract class for creating item</summary>
/// <param name="x">an int, x position on canvas</param>
/// <param name="y">an int, y position on canvas</param>
/// <param name="full">a bool, checks if cell is full or not</param>
/// <returns>nothing</returns>
[< AbstractClass >]
type Item (x:int, y:int, full:bool) =
    inherit Entity(x,y)

    //how an item interacts with the player
    abstract member InteractWith : Player -> unit 

    //changes the value of full if item fills the whole cell
    member val FullyOccupy : bool = full with get, set 

    
/// <summary>creates a wall at x,y position that are vertical</summary>
/// <param name="x">an int, xx position</param>
/// <param name="y">an int, y position</param>
/// <returns>wall1</returns>
type Wall1 (x,y) as wall1 =
    inherit Item(x, y, true)
    //sets design for wall
    do wall1.Presence <- (Pixel('|', Color.Black, Color.DarkMagenta))
    //makes the wall appear on canvas
    override this.RenderOn (canvas:Canvas) : unit =
        canvas.Set (fst this.Pos, snd this.Pos, this.Presence)
    //does so nothing happens and the player cannot move into the wall
    override this.InteractWith (p:Player) : unit = ()

/// <summary>creates a wall at x,y position that are horizontal</summary>
/// <param name="x">an int, x position</param>
/// <param name="y">an int, y position</param>
/// <returns>wall2</returns>
type Wall2 (x,y) as wall2 =
    inherit Item(x, y, true)
    //sets design for wall
    do wall2.Presence <- (Pixel('-', Color.Black, Color.DarkMagenta))
    //makes the wall appear on canvas
    override this.RenderOn (canvas:Canvas) : unit =
        canvas.Set (fst this.Pos, snd this.Pos, this.Presence)
    //does so nothing happens and the player cannot move into the wall
    override this.InteractWith (p:Player) : unit = ()

 
/// <summary>creates the water that heals the player by 2 hp</summary>
/// <param name="x">an int, x position</param>
/// <param name="y">an int, y position</param>
/// <returns>water</returns>       
type Water (x,y) as water =
    inherit Item(x,y, false)
    //sets the design
    do water.Presence <- (Pixel('~', Color.White, Color.Blue))
    //makes the water appear on canvas
    override this.RenderOn (canvas:Canvas) : unit = 
        canvas.Set (fst this.Pos, snd this.Pos, this.Presence)
    //when player move onto the water, it heals by 2 hp
    override this.InteractWith (p:Player) : unit = 
        p.Heal (2)


 
/// <summary>creates the fire that deals 5 hp in damage and dies after 5 interactions</summary>
/// <param name="x">an int, x position</param>
/// <param name="y">an int, y position</param>
/// <returns>Fire</returns>
type Fire (x,y) as fire =
    inherit Item(x,y, false)
    //sets fire on canvas
    do fire.Presence <- (Pixel('§', Color.White, Color.Red))

    //properties
    member val fireHP = 5 with get, set
    member val IsOut = false with get, set

    //checks if the fire is out. If so it render the color of the canvas on the canvas. Otherwise it renders the fire 
    override this.RenderOn (canvas:Canvas) : unit = 
        if fire.IsOut then 
            canvas.Set (fst this.Pos, snd this.Pos, setPixel)
        else
            canvas.Set (fst this.Pos, snd this.Pos, this.Presence)

    //how the player interacts with fire. If the fire is out then nothing happens, else it deals 1 hp to the players hp and checks if the fires
    //hp is 0 or less to determine if it is out
    override this.InteractWith (p:Player) : unit =
        if this.IsOut then ()    
        else 
            p.Damage (1)
            this.fireHP <- this.fireHP - 1
            if this.fireHP <= 0 then this.IsOut <- true



/// <summary>creates flesh eating plant that deals 5 damage to the players hp and fills a whole cell on the canvas</summary>
/// <param name="x">an int, x position</param>
/// <param name="y">an int, y position</param>
/// <returns>flesh eating plant</returns>
type FleshEatingPlant (x,y) as plant =
    inherit Item(x,y, true)
    //sets the design
    do plant.Presence <- (Pixel('¤', Color.White, Color.Green))
    //makes it appear on canvas
    override this.RenderOn (canvas:Canvas) : unit = 
        canvas.Set (fst this.Pos, snd this.Pos, this.Presence)

    //makes it deal 5 damage to hp to the player when interacted with
    override this.InteractWith (p:Player) : unit =
        p.Damage (5)

//Jar and Trap has the same design so the player either gets an award or has damaged dealt to its hp

/// <summary>creates jar at x,y position and heals player by 5 hp</summary>
/// <param name="x">an int, x position</param>
/// <param name="y">an int, y position</param>
/// <returns>jar</returns>
type Jar (x,y) as jar =
    inherit Item (x,y, false)
    //set the design
    do jar.Presence <- (Pixel('O', Color.Black, Color.Yellow))
    //makes it appear on canvas
    override this.RenderOn (canvas:Canvas) : unit = 
        canvas.Set (fst this.Pos, snd this.Pos, this.Presence)

    //makes the players heal by 5 hp
    override this.InteractWith (p:Player) : unit =
        p.Heal (5)

/// <summary>generatses a sample of random numbers from 0 to n by using System.Random and then picks one number out</summary>
/// <param name="n">an int, how big the sample of random number, the function can choose from</param>
/// <returns>a random number, int</returns>
let rand : int -> int =
    let rnd = System.Random()
    in fun n -> rnd.Next(0,n)

/// <summary>creates the trap that deals 5 hp damage to the player and the player is placed on at random position</summary>
/// <param name="x">an int, x position</param>
/// <param name="y">an int, y position</param>
/// <returns>trap</returns>
type Trap(x,y) as trap = 
    inherit Item (x,y, false)
    //set the design
    do trap.Presence <- (Pixel('O', Color.Black, Color.Yellow))
    //makes a random number from 0 to 10
    let randomNumber = rand(10)

    //makes the trap appear on canvas
    override this.RenderOn (canvas:Canvas) : unit = 
        canvas.Set (fst this.Pos, snd this.Pos, this.Presence)

    //makes the trap deal 5 damage to the players hp and then moves the player to a random position
    override this.InteractWith (p:Player) : unit =
        p.Damage (9)
        p.MoveTo (randomNumber, randomNumber)


/// <summary>creates the exit that makes the user win the game. The players hp must be 5 or higher to exit</summary>
/// <param name="x">an int, x position</param>
/// <param name="y">an int, y position</param>
/// <returns>exit</returns>
type Exit(x,y) as exit =
    inherit Item(x,y, false)
    //sets the design
    do exit.Presence <- (Pixel('X', Color.Black, Color.DarkMagenta))

    //makes exit appear on canvas
    override this.RenderOn (canvas:Canvas) : unit = 
        canvas.Set (fst this.Pos, snd this.Pos, this.Presence)
    
    //property
    member val OpenDoor = false with get, set

    //when interacted with, it checks the players hp. If it is 5 or higher, the property of opendoor becomes true and the player can exit
    override this.InteractWith (p:Player) : unit =
        if p.hp >= 5 then 
            this.OpenDoor <- true 
            do sprintf "You have enough hp(%i) to exit the dungeon" p.hp


/// <summary>creates world which is a canvas where items have been added and the user can move the player around on</summary>
/// <param name="X">an int, how wide the world shall be</param>
/// <param name="Y">an int, how high the world shall be</param>
/// <returns>World</returns>
type World (X:int, Y:int) = 
    //creates the canvas with constructors as input
    let canvas = Canvas(X,Y)
    //properties
    member val ItemLst: Item list = [] with get, set
    member val Status = true with get, set
    member val player = Player(0,0) with get, set
    
    member val RecentItemInteract : Item option = None with get, set

    //sets the players start position
    member this.MovePlayerStart (x:int, y:int) : unit = this.player.MoveTo (x, y)

    //adds item onto canvas by appending the item onto the list of items 
    member this.AddItem (i:Item) : unit = 
        this.ItemLst <- i::this.ItemLst

    //goes over every element in th item list and uses renderon function the make the items appear on canvas
    member this.SetItemsOnCanvas () : unit = 
        List.iter (fun (elm:Item) -> elm.RenderOn(canvas)) this.ItemLst
        //makes the player appear on canvas
        this.player.RenderOn canvas

    //uses the method show in class canvas to make the canvas show in the terminal 
    member this.ShowCanvas () : unit = 
        canvas.Show()

    /// <summary>checks if a cell is already full and therefore other items and the player can't move here. It also checks if the input
    /// is out of range of the height and width of the world</summary>
    /// <param name="cellX">an int, the x position on canvas</param>
    /// <param name="cellY">an int, the y position on canvas</param>
    /// <returns>a tuple of a bool and an Item option</returns>
    member this.CheckingCell (cellX:int, cellY:int) : (bool*Item option) =
        let mutable freeCell = true
        let mutable itemAtPos : Item option = None

        //if the Y and X cell is out the range of the canvas then there is no free cell and it won't be returned
        if cellY < 0 || cellY > Y - 1 || cellX < 0 || cellX > X - 1 then freeCell <- false

        //else it checks all elements in Item list and if the item has the position of X,Y cell then it 
        //attach the item as an option to itemAtPos 
        else
            for i=0 to this.ItemLst.Length-1 do
                if this.ItemLst.[i].Pos = (cellX, cellY) then
                    itemAtPos <- Some this.ItemLst.[i]
        
        //matches itemAtPos. If there is no items at X,Y values then it returns nothing. Otherwise it takes the item as checks if it is 
        //filling out the whole cell, if so then free cell becomes false
            match itemAtPos with
                | None -> ()
                | Some elm -> if elm.FullyOccupy then freeCell <- false
            
        //return values
        (freeCell, itemAtPos)
    
    /// <summary>calls the functions to set items onto canvas and show canvas, reads to users moves, checks if the player has died and
    /// prints out if the user wins or loses</summary>
    /// <param name="">takes no argument</param>
    /// <returns>unit</returns>
    member this.Play () : unit = 
        //goes through this while loop until the player dies or wins
        while this.Status do
            //clear the terminal
            Console.Clear()
            //sets the added items onto the canvas
            this.SetItemsOnCanvas()
            //shows canvas in terminal
            this.ShowCanvas()

            printfn "%s" this.player.infoEvent

            //matches the item the player interacted with. If there is no item then return nothing and else if the item is the exit and the 
            //player don't have enough hp then return a string
            match this.RecentItemInteract with
                | None -> ()
                | Some elm -> if elm :? Exit then printfn "You need to have 5 hp to exit the dungeon. You have %i" this.player.hp 

            //binds the the users moves
            let key_info = Console.ReadKey(true)

            //matches the users moves
            match key_info.Key with
                    //when the user presses up arrow
                    | ConsoleKey.UpArrow -> 
                        let tryMove = this.CheckingCell (fst this.player.Pos, snd this.player.Pos-1)
                        if fst tryMove then 
                            this.player.ResetOnCanvas canvas
                            this.player.MoveTo (fst this.player.Pos, snd this.player.Pos-1)
                        match snd tryMove with
                            | None -> this.RecentItemInteract <- None
                            | Some elm -> 
                                elm.InteractWith this.player 
                                this.RecentItemInteract <- Some elm
                    //when the user presses left arrow
                    | ConsoleKey.LeftArrow ->
                        let tryMove = this.CheckingCell (fst this.player.Pos-1, snd this.player.Pos)
                        if fst tryMove then 
                            this.player.ResetOnCanvas canvas
                            this.player.MoveTo (fst this.player.Pos-1, snd this.player.Pos)
                        match snd tryMove with
                            | None -> this.RecentItemInteract <- None
                            | Some elm -> 
                                elm.InteractWith this.player 
                                this.RecentItemInteract <- Some elm
                    //when the user presses down arrow
                    | ConsoleKey.DownArrow ->
                        let tryMove = this.CheckingCell (fst this.player.Pos, snd this.player.Pos+1)
                        if fst tryMove then 
                            this.player.ResetOnCanvas canvas
                            this.player.MoveTo (fst this.player.Pos, snd this.player.Pos+1)
                        match snd tryMove with
                            | None -> this.RecentItemInteract <- None
                            | Some elm -> 
                                elm.InteractWith this.player 
                                this.RecentItemInteract <- Some elm
                    //when the user presses right arrow
                    | ConsoleKey.RightArrow  ->
                        let tryMove = this.CheckingCell (fst this.player.Pos+1, snd this.player.Pos)
                        if fst tryMove then 
                            this.player.ResetOnCanvas canvas
                            this.player.MoveTo (fst this.player.Pos+1, snd this.player.Pos)
                        match snd tryMove with
                            | None -> this.RecentItemInteract <- None
                            | Some elm -> 
                                elm.InteractWith this.player 
                                this.RecentItemInteract <- Some elm 
                    //exits the game
                    | ConsoleKey.Escape ->
                        this.Status <- false
                    //if the user presses anything else than up, down, left, right
                    | _ -> ()
            
            //opens the door at the exit and the game is stopped
            match this.RecentItemInteract with
            | None -> ()
            | Some elm -> 
                if elm :? Exit then 
                    if (elm :?> Exit).OpenDoor then this.Status <- false

            //the player dies and the game is stopped
            if this.player.IsDead then this.Status <- false

        //if the player has died then it clears the terminal and prints, you lose
        if this.player.IsDead then
            Console.Clear()
            printfn "YOU LOST"

        //else the player goes through the exit and wins
        else
            Console.Clear()
            printfn "YOU WON"
