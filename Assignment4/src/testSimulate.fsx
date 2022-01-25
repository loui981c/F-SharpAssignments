#r "simulate.dll"

open simulate

//Whitebox-test
//Making different drones with the speed 1 meter/minute because they have to travel for 1 minute at a time
let drone1 = Drone ((0.0,0.0), (3.0,3.0), 1.0)
let drone2 = Drone ((0.0,0.0), (3.0,3.0), 1.0)
let drone3 = Drone ((0.0,0.0), (3.0,3.0), 1.0)
let drone4 = Drone ((0.0,0.0), (5.0,5.0), 1.0)

//Tesing Position, Destination, Speed and IsFinished
printfn "Position, should return (0.0, 0.0): %A" drone1.Position
printfn "Destination, should return (3.0, 3.0): %A" drone1.Destination
printfn "Speed, should return 1.0: %A" drone1.Speed
printfn "IsFinished, should return false: %A" drone1.IsFinished

//Testing Fly
printfn "Position, should return (0.0, 0.0): %A" drone1.Position
drone1.Fly ()
printfn "Position: %A" drone1.Position
printfn "IsFinished, should return true: %A" drone1.IsFinished

//Testing crash
printfn "Crash, should return (): %A" drone4.Crash
printfn "IsFinished, should return true: %A" drone4.IsFinished

//Making a airspace with 3 drones 
let airspace = Airspace ([drone1;drone2])

//Testing Drones
printfn "Drones, should return [drone1;drone2]: %A" airspace.Drones

//Testing FlyDrones which is the same as Flycopies just on a different list
printfn "Position, should return (0.0, 0.0): %A" drone1.Position
printfn "Position, should return (0.0, 0.0): %A" drone2.Position
printfn "FlyDrones, should return (): %A" airspace.FlyDrones
printfn "Position, should return (1.0, 1.0): %A" drone1.Position
printfn "Position, should return (1.0, 1.0): %A" drone2.Position


//Testing AddDrones 
printfn "Drones, should return [drone1;drone2]: %A" airspace.Drones
printfn "AddDrones, should return (): %A" (airspace.AddDrones drone3)
printfn "Drones, should return [drone1;drone2;drone3]: %A" airspace.Drones


//Testing copyDrones and WillCollide 
printfn "copyDrones, should return (): %A" airspace.copyDrones 
printfn "WillCollide, should return [(0,1);(0,2);(1,2)]: %A" (airspace.WillCollide 1)
//Returns [(0, 1); (0, 2); (1, 0); (1, 2); (2, 0); (2, 1)] but (0,1) is the same as (1,0) because the numbers indicates the index of a 
//drone, so 0=drone1, 1=drone2 and 2=drone3




