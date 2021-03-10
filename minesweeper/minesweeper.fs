namespace Minesweeper
open System

module Minesweeper =

    [<Literal>]
    let Empty = 0
    [<Literal>]
    let Bomb = -1
    [<Literal>]
    let Covered = -2

    type Grid = Grid of int[]
    type Pos = {
        X: int
        Y: int
    }

    let parseNumbers (input: string) =
        let parts = input.Split(',')
        if parts.Length <> 2 then
            None
        else
            try
                parts |> List.ofArray |> List.map int |> Some
            with :? FormatException ->
                None

//TODO: return string
    let printGrid (Grid grid) dim =
        for x = 0 to dim-1 do
            for y = 0 to dim-1 do
                match grid.[x * dim + y] with
                    | Empty -> printf " "
                    | i when i > 0 -> printf "%i" i
                    | Covered -> printf "?"
                    | Bomb -> printf "b"
                    | _ -> printf "x"
            printf "\n"

    let createGrid dim =
        let grid = Array.create (dim * dim) Covered
        let rand = Random()
        for i=0 to (floor ((float (dim*dim))*0.2) |> int) do
            let mutable pos = rand.Next(0, Array.length grid)
            while grid.[pos] = Bomb do
                pos <- rand.Next(0, Array.length grid)
            grid.[pos] <- Bomb
        grid |> Grid

    let private getNeighbors dim x y =
        [(x-1, y-1); (x-1, y);(x-1, y+1);
         (x, y-1); (x, y+1);
         (x+1, y-1); (x+1, y);(x+1, y+1);]
         |> List.filter (fun (r,c) -> r >= 0 && c >= 0 && r < dim && c < dim)

    let private bombCount (grid: int[]) dim x y =
        getNeighbors dim x y
        |> List.sumBy (fun (r, c) ->
                       match grid.[r*dim + c] with
                       | Bomb -> 1
                       | _ -> 0)

    let rec private uncover_rec (grid: int[]) dim x y =
        match grid.[x * dim + y] with
            | Covered ->
                let count = bombCount grid dim x y
                grid.[x*dim + y] <- count
                if count = 0 then
                    for r, c in getNeighbors dim x y do
                        uncover_rec grid dim r c
            | _ -> ()

    let uncover (Grid grid) dim pos =
        let mutable grid_copy = Array.copy grid
        uncover_rec grid_copy dim pos.X pos.Y
        grid_copy |> Grid
