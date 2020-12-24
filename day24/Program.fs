// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open System.Diagnostics
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
  if true then 
    let input2 = readFile "/Users/xeno/projects/aoc2020/day24_fs/input.txt"
//     printfn "Input: %A" input2
    let dirs1 = toDirections "nwnwsese"
    let initialState = State(Floor(),TilePos(0,0))
    // let initialState = State(Floor(), TilePos(-4,-4))
    let endState = processInstructions initialState input2
    // let endState = flipTile initialState dirs1 
    printfn "%A" endState
    printfn "Answer 1: %d" (endState.Floor.countBlacks ())
    let flipped = flipTimes endState.Floor 100 
   
    printfn "Flip 1 %A" (flipped.countBlacks ())
    0
  else
    let floor = Floor((Map.empty.Add ((0,0),BLACK)).Add ((0,2),BLACK))
    let flipped = flipTimes floor 100 
    printfn "%d %A" (flipped.countBlacks ()) flipped
    
    0  