#r "readNWrite.dll"

open readNWrite

[<EntryPoint>]
let main (args: string array) =
 try
  let x = (Array.toList args |> tac)
  printfn "%A" x.Value
  0
 with 
  | _ -> 1