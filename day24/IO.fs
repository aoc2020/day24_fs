module day24.IO

open System
open System.IO
open day24.Types

let readLines (filePath:String) = seq {
    use sr = new StreamReader(filePath)
    while not sr.EndOfStream do
        yield sr.ReadLine ()
}

let toDirections (line:String) : List<Direction> =  
    let chars = line.ToCharArray () |> Array.toList
    let rec parseChars (chars: List<char>) : List<Direction> =
        match chars with
        | [] -> []
        | 'e'::tail -> EAST :: (parseChars tail)
        | 'w'::tail -> WEST :: (parseChars tail) 
        | 'n'::'e'::tail -> NE :: (parseChars tail)
        | 'n'::'w'::tail -> NW :: (parseChars tail)
        | 's'::'e'::tail -> SE :: (parseChars tail)
        | 's'::'w'::tail -> SW :: (parseChars tail)
    parseChars chars     

let readFile (filePath:String): List<Direction>[] =
    let lines = readLines filePath
    lines |> Seq.map (toDirections) |> Seq.toArray  
          
            