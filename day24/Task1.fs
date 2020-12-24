module day24.Task1

open day24.Types

type State (floor:Floor, pos:TilePos) as self =
    override this.ToString () = sprintf "State(floor:%A, pos:%A)" floor pos 
    member this.Floor = floor
    member this.Pos = pos 

let flipTiles (state:State) (directions:List<Direction> : List<Direction>) : State =
    let accumulate (state:State) (dir:Direction) =
        let newPos = state.Pos.InDirection dir
//        let newFloor = state.Floor.flip newPos
        State(state.Floor,newPos)
    let state = directions |> List.fold accumulate state
    let floor = state.Floor.flip state.Pos
    State(floor,state.Pos)
    
let processInstructions (state:State) (directions:List<Direction>[]) : State =
    let accumulate (state:State) (dirs:List<Direction>) : State =
        let state = State(state.Floor,TilePos(0,0))
        flipTiles state dirs
    directions |> Array.fold accumulate state 
        
          