module day24.Task2

open day24.Types

let neighborColors (floor:Floor) (pos:TilePos) =
    pos.neighbors ()
    |> Array.map (floor.tileAt)

let flipFromNeighbors (floor:Floor) (pos:TilePos) : TileColor =
    let neighbors = neighborColors floor pos 
    let isBlack (color:TileColor) = color = BLACK 
    let isWhite (color:TileColor) = color = WHITE
    let blacks = neighbors |> Seq.filter (isBlack) |> Seq.length
    let whites = neighbors |> Seq.filter (isWhite) |> Seq.length
    let color = floor.tileAt pos 
    let newColor : TileColor =
                   match color with
                   | BLACK -> if blacks = 0 || blacks > 2
                              then WHITE
                              else BLACK  
                   | WHITE -> if blacks = 2
                              then BLACK
                              else WHITE
    newColor 

let flipFloor (floor:Floor) : Floor =
    let isBlack (posAndColor:TilePos*TileColor) = (snd posAndColor) = BLACK 
    let findColor (pos:TilePos) : TilePos*TileColor = pos,flipFromNeighbors floor pos 
    let allPos : TilePos[] = floor.allRelevantPositions ()
    let newFloor : Floor =
                 allPos
                 |> Array.map (findColor)
                 |> Array.filter (isBlack)
                 |> Seq.map (fun (pc:TilePos*TileColor) -> ((fst pc).Raw,snd pc))
                 |> Map.ofSeq
                 |> (Floor)
    newFloor 
                         
   
    
    
    