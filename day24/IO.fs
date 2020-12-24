module day24.IO

open System
open System.IO

let readLines (filePath:String) = seq {
    use sr = new StreamReader(filePath)
    while not sr.EndOfStream do
        yield sr.ReadLine ()
}

let splitByBlanks (lines:List<String>) : String[][] = 
    let accumulate (acc:List<List<String>>) (line:String) : List<List<String>> =
        match acc,line with
        | [],"" -> []
        | []::tail,"" -> acc
        | acc,"" -> []::acc 
        | block::tail,line -> (line::block)::tail
    lines
    |> List.fold accumulate []
    |> List.map (List.rev)
    |> List.map (List.toArray)
    |> List.rev
    |> List.toArray 

let readFile (filePath:String): String[][] =
    let lines = readLines filePath |> Seq.toList 
    let blocks = splitByBlanks lines
    blocks 
            