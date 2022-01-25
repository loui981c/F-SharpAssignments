module simulate

open System

type Drone (pos:float*float, des:float*float, speed:float) =
    //Constructor
    let mutable pos = pos
    let mutable isFlying = true

    //Speed
    //The length of travel x=(fst(pos)+fst(des)) y=(snd(pos)+snd(des))
    let lenOfTravel = (fst(des)-fst(pos), snd(des)-snd(pos))
    //Direction
    let direction = (speed/(fst(lenOfTravel) ** 2.0 + snd(lenOfTravel) ** 2.0))
    //Velocity
    let velocity = ((fst(lenOfTravel))*direction, (snd(lenOfTravel))*direction)

    //Properties
    member this.Position = pos
    member this.Speed = speed 
    member this.Destination = des 
    member this.IsFinished = 
        if pos = des then 
            true
        else false

    //Methods
    /// <summary>The function sets the new position of the drone after travelling with the speed in meters as input of one of the
    /// parameters. It calculates the distance to destination and then checks if the distance is less then the speed. If so the drone
    /// has landed and is no longer flying</summary>
    /// <param name="">Takes no arguments</param>
    /// <returns>Returns nothing</returns>
    member this.Fly () = 
        let startPos = pos
        let distanceToDes = sqrt(((fst(startPos)-fst(des))**2.0)+((snd(startPos)-snd(des))**2.0))
        let newPosition = (fst(velocity) + fst(pos), snd(velocity) + snd(pos))
        if distanceToDes < speed then pos <- des
        else 
            pos <- newPosition
    
    /// <summary>Crash sets the bool expresion isFlying to false which means the drone is no longer in the airspace</summary>
    /// <param name="">Takes no arguments</param>
    /// <returns>Returns nothing</returns>
    member this.Crash () =
        pos <- des 

    member this.Clone =
        new Drone (pos,des,speed)


type Airspace (x: Drone list) =
    let mutable x = x
    let mutable copyDronesLst = []

    //Properties
    member this.Drones = x

    //Methods
    /// <summary>Calculates the distance between two drones</summary>
    /// <param name="x0">Drone num 1</param>
    /// <param name="x1">Drone num 2</param>
    /// <returns>Returns a float</returns>
    member this.DroneDist (x0:Drone) (x1:Drone) : float = 
        let z1 = fst(x0.Position)
        let v1 = snd(x0.Position)
        let z2 = fst(x1.Position)
        let v2 = snd(x1.Position)
        sqrt((((z1-z2)**2.0)+((v1-v2)**2.0)))

    /// <summary>For every element in the x, drone list from the constructor, is Fly applied</summary>
    /// <param name="">Takes no arguments</param>
    /// <returns>Returns nothing</returns>
    member this.FlyDrones =
        for i=0 to x.Length-1 do x.[i].Fly()

     /// <summary>Copies the drones from the input to a different list</summary>
    /// <param name="">Takes no arguments</param>
    /// <returns>Returns nothing</returns>
    member this.copyDrones =
        for i=0 to x.Length-1 do
            copyDronesLst <- copyDronesLst@[x.[i]]

    /// <summary>For every element in the copyDronesLst, drone list from the constructor, is Fly applied</summary>
    /// <param name="">Takes no arguments</param>
    /// <returns>Returns nothing</returns>>
    member this.FlyCopys =
        for i=0 to copyDronesLst.Length-1 do copyDronesLst.[i].Fly()

    /// <summary>Adds a new drone to the drone list by appending it</summary>
    /// <param name="y">A drone</param>
    /// <returns>Returns nothing</returns>
    member this.AddDrones (y:Drone) =
        x <- x @ [y]
    

    //                                   !!!CRASHES ALL THREE DRONES IF MORE THAN TWO DRONES!!!
 
    /// <summary>Creates the copy drones so it won't affect the original drones when flying the drones to find out if the drones
    /// will collide. The end time and start time subtracted from each other to find the time interval and the function will fly the drones
    /// the number of minutes as in the time interval. It has two for-loops to check every element in the drone list and for every element
    /// the function checks if the distance between two drones are less than 5.0 meters. It makes sure that it is not the same drone it checks
    /// and then it checks if the drones are still flying. If all these conditions happens then the drones are appended the crash list
    /// as a tuple. Otherwise it returns an empty tuple. Then it checks if the crash list is empty and if it is true then it returns 
    /// an empty list otherwise it returns the crash list. Lastly, the function crashes as many drones as there are in the crash from
    /// the copy drone list, so it is no longer considered in the airspace</summary>
    /// <param name="v">Start time in interval</param>
    /// <param name="w">End time in interval</param>
    /// <Returns>Returns a tuple list where the ints represent the drones by their index in the drone list</returns>
    member this.WillCollide w =
        let clones = List.init x.Length (fun y -> x.[y].Clone) 
        let mutable crashes = []
        for i in 0..w do 
            List.iter (fun (y:Drone) -> y.Fly()) clones
            for m in 0..clones.Length-1 do
                for n in m+1..clones.Length-1 do
                    if this.DroneDist clones.[m] clones.[n] < 5.0 && clones.[m] <> clones.[n] && clones.[m].IsFinished = false && clones.[n].IsFinished = false then 
                        crashes <- crashes@[(m,n)]

                    else ()
            for i=0 to crashes.Length-1 do
                clones.[fst(crashes.[i])].Crash()
                clones.[snd(crashes.[i])].Crash()
        printfn "%A" crashes
        List.map (fun y -> (x.[fst(y)],x.[snd(y)])) crashes 


 