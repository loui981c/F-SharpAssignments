#r "roguelike.dll"

open roguelike

//makes a world
let newWorld = World(12, 12)

//walls vertical 
newWorld.AddItem (Wall1 (3,1))
newWorld.AddItem (Wall1 (3,2))
newWorld.AddItem (Wall1 (8,1))
newWorld.AddItem (Wall1 (8,2))
newWorld.AddItem (Wall1 (8,3))
newWorld.AddItem (Wall1 (2,5))
newWorld.AddItem (Wall1 (2,6))
newWorld.AddItem (Wall1 (2,7))

//walls horisontiel
newWorld.AddItem (Wall2 (1,3))
newWorld.AddItem (Wall2 (2,3))
newWorld.AddItem (Wall2 (3,3))
newWorld.AddItem (Wall2 (4,1))
newWorld.AddItem (Wall2 (5,1))
newWorld.AddItem (Wall2 (6,1))
newWorld.AddItem (Wall2 (7,1))
newWorld.AddItem (Wall2 (8,4))
newWorld.AddItem (Wall2 (7,4))
newWorld.AddItem (Wall2 (9,6))
newWorld.AddItem (Wall2 (8,6))
newWorld.AddItem (Wall2 (7,6))

//jars
newWorld.AddItem (Jar (7,2))
newWorld.AddItem (Jar (6,8))

//traps 
newWorld.AddItem (Trap (2,2))
newWorld.AddItem (Trap (2,9))

//plants
newWorld.AddItem (FleshEatingPlant (6, 2))
newWorld.AddItem (FleshEatingPlant (2, 8))

//water
newWorld.AddItem (Water (2, 4))
newWorld.AddItem (Water (9, 7))

//fire
newWorld.AddItem (Fire (7, 3))
newWorld.AddItem (Fire (1, 6))
newWorld.AddItem (Fire (4, 5))

//exit
newWorld.AddItem (Exit (9, 9))

//start position
newWorld.MovePlayerStart (1, 1)

//calling the game
newWorld.Play()         