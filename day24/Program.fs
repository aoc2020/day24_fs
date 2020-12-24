// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open day24.Types 
open day24.Utils
open day24.Task1
open day24.Task2 
open day24.IO

// Define a function to construct a message to print
let from whom =
    sprintf "from %s" whom

[<EntryPoint>]
let main argv =
    let input2 = readLines "/Users/xeno/projects/aoc2020/day24_fs/input2.txt"
   
    printfn "Input: %A" input2
    0
    