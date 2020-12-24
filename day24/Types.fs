module day24.Types

type TileColor =
    | WHITE
    | BLACK

type Direction = EAST | WEST | NE | NW | SE | SW 
type RawPos = int*int 
    
type TilePos (x:int,y:int) as self =    
    override this.ToString () = sprintf "T(%d,%d)" x y 
    new() = TilePos(0,0)
    new(raw:RawPos) = TilePos(fst raw,snd raw)
    member this.ROW_INDENT = (abs(y)) % 2
    member this.Raw : RawPos = (x,y)
    member this.East () = TilePos(x+1,y)
    member this.West () = TilePos(x-1,y)
    member this.SouthEast () = TilePos(x + this.ROW_INDENT,y-1)
    member this.SouthWest () = TilePos(x - 1 + this.ROW_INDENT,y-1)
    member this.NorthEast () = TilePos(x + this.ROW_INDENT,y+1)
    member this.NorthWest () = TilePos(x - 1 + this.ROW_INDENT,y+1)
    member this.InDirection (dir:Direction) : TilePos =
//        printfn "ident at %A: %d" self this.ROW_INDENT 
        match dir with
        | EAST -> this.East ()
        | WEST -> this.West ()
        | SE -> this.SouthEast ()
        | SW -> this.SouthWest ()
        | NE -> this.NorthEast ()
        | NW -> this.NorthWest ()
    member this.neighbors () : TilePos[] =
        [|this.East();this.West();this.SouthEast();this.SouthEast();this.NorthEast();this.NorthWest()|]

type TileMap = Map<RawPos,TileColor> 
type Floor (tiles:Map<RawPos,TileColor>) as self =
    override this.ToString () = sprintf "Floor(%A)" tiles 
    new () = Floor(Map.empty)
    member this.tileAt (pos:TilePos) : TileColor =
        if tiles.ContainsKey pos.Raw then
            tiles.[pos.Raw]
        else
            WHITE
    member this.flip (pos:TilePos) : Floor =
        let tile = this.tileAt pos
        let newTile = match tile with
                      | WHITE -> BLACK
                      | BLACK -> WHITE
        let newTiles = tiles.Add (pos.Raw,newTile)
        Floor (newTiles)
    
    member this.countBlacks () =
        Map.toSeq tiles
        |> Seq.map (snd)
        |> Seq.filter (fun (c:TileColor) -> c = BLACK)
        |> Seq.length 
    member this.allRelevantPositions() : TilePos[] =
        Map.toSeq tiles
        |> Seq.map (fst)
        |> Seq.map (TilePos)
        |> Seq.map (fun (pos:TilePos) -> pos.neighbors())
        |> Seq.map (Array.toSeq)
        |> Seq.concat 
        |> Seq.map (fun (pos:TilePos) -> pos.Raw)
        |> Seq.distinct
        |> Seq.map (TilePos)
        |> Seq.toArray 
        
        
            
