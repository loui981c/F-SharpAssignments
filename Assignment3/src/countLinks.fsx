//countLinks.fsx (function countLinks) 9g3
open System
open System.Net
open System.IO

///<summary> Create a function that can read the URL(Uniform Resource Locator. </summary>
///<param name = "url"> String of the website address. </param>
///<returns> The information which the linked URL contains. </returns>

//fetchUrl : url:string -> string 
let fetchUrl (url:string) : string =     //set up a url as a stream
    let uri= System.Uri url
    let request = WebRequest.Create uri
    let response = request.GetResponse ()
    let stream = response.GetResponseStream ()
    let reader = new System.IO.StreamReader (stream)
    reader.ReadToEnd ()
printfn "The URL contains: %A" (fetchUrl "http://www.google.com")

///<summary> Create a function that can count the links that html-standard contains from the URL. (Here as substrings with "<a></a>tages").
///The program should take a url, pass it to the function and print the resulting count on the screen. </summary>
///<param name = "url"> String of the web address.</param>
///<param name = "lc"> Number of the place where contains "<a".</param>
///<param name = "webstring"> String of the website.</param>
/// <returns> The number of the links that webpage contains. </returns>

// countLinks : url:string -> int
let countLinks (url:string):int =
    let website = fetchUrl url //return the webpage's html as string
    
    let rec linkcounter (lc:int) (webstring:string) =
        if (webstring.Contains "<a") then
            let x = webstring.IndexOf ("<a")   
            linkcounter (lc + 1)  webstring.[ x+1 .. ] 
        else lc
    linkcounter 0 website  //start value as 0
printfn "The website contains %A links." (countLinks "http://www.google.com")
//As long as the website is accessible, there should not be an error.
