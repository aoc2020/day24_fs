module day24.Types

type TileColor =
    | WHITE
    | BLACK

type Direction = EAST | WEST | NE | NW | SE | SW 
type RawPos = int*int 
    
type TilePos (x:int,y:int) as self =
    override this.ToString () = sprintf "T(%d,%d)" x y 
    new() = TilePos(0,0)
    member this.Raw : RawPos = (x,y)
    member this.East () = TilePos(x+1,y)
    member this.West () = TilePos(x-1,y)
    member this.SouthEast () = TilePos(1 - (abs(y-1) % 2),y-1)
    member this.SouthWest () = TilePos(-(abs(x-1) % 2),y-1)
    member this.NorthEast () = TilePos(1 - ((y+1)%2),y+1)
    member this.NorthWest () = TilePos(-((y+1)%2),y+1)
    member this.InDirection (dir:Direction) : TilePos =
        match dir with
        | EAST -> this.East ()
        | WEST -> this.West ()
        | SE -> this.SouthEast ()
        | SW -> this.SouthWest ()
        | NE -> this.NorthEast ()
        | NW -> this.NorthWest ()

type TileMap = Map<RawPos,TileColor> 
type Floor (tiles:Map<RawPos,TileColor>) as self =
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
        
            
