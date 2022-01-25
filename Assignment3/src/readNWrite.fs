module readNWrite

open System
open System.IO

exception FileNotFoundException

/// <summary>Reads the content of a file</summary>
/// <param name="filename">Which is the name of the file, e.g. a.txt</param>
/// <returns>A string option. Either Some and then the constent of the file or None if the file can not be located
/// </returns>
let readFile (filename:string) : string option =
    let readText = File.ReadAllText(filename)
    try 
        Some(readText)
    with
        | FileNotFoundException -> None
        | _ -> None

/// <summary>Takes at least 2 files and reads and prints the constent of the files in the order of the 1st file
/// then 2nd and so on. The function uses a loop to take each file and reading them until all files are read
/// by using readFile</summary>
/// <param name="filenames">A list of filenames, e.g. a.txt b.txt</param>
/// <param name="files">The same as "filenames"</param>
/// <returns>Returns a string option. Either Some and then the content of the files or None if the files or one
/// of the files can not be located<returns>
let cat (filenames:string list) =
    let rec catLoop (files:string list) =
        match files with
            | x::xs -> (readFile x).Value + (catLoop xs)
            | _ -> ""
    try
        Some(catLoop filenames)
    with 
        | FileNotFoundException -> None
        | _ -> None

/// <summary>Takes at least 2 files and reads, reverses and prints the constent of the files in the order of the 1st file
/// then 2nd and so on. The function uses revFile to append the content of an file to an empty list. Then there is created a new list
/// newList where revFile are used on every file in filenames to make the newList with the content of the files. Then List.fold is used
/// on the reversed newList with a " " (which is a linebreak)</summary>
/// <param name="filenames">A list of filenames, e.g. a.txt b.txt</param>
/// <param name="filename">One file, e.g. a.txt</param>
/// <returns>Returns a string option. Either Some and then the content of the files reversed or None if the files or one
/// of the files can not be located<returns>
let tac (filenames:string list) : string option =  
    let revFile (filename:string) : string =
        let mutable n = ""
        let r = IO.File.OpenText filename
        while not(r.EndOfStream) do
            let y = string (char (r.Read()))
            n <- y+n
        n
    let newList = List.map revFile filenames
    let revString = List.fold (+) " " (List.rev newList)
    try 
        Some(revString)
    with 
        | FileNotFoundException -> None
        | _ -> None
 


    

