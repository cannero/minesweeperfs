open System
open Minesweeper

let runLoop dim =
    while true do
        printfn "enter x,y or q to quit"
        let userInput = Console.ReadLine()
        if userInput.ToUpper() = "Q" then
            printfn "goodbye"
            Environment.Exit(0)
        printfn "%A" (Minesweeper.parseNumbers userInput)
        

[<EntryPoint>]
let main argv =
    let dim = 3
    let grid = Minesweeper.createGrid dim
    Minesweeper.printGrid grid dim
    //runLoop 3
    0
