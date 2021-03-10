namespace minesweeper.Test

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open Minesweeper

module MS = Minesweeper

[<TestClass>]
type TestClass () =

    [<TestMethod>]
    member this.Test_createGrid () =
        let dim = 5
        let (MS.Grid grid) = MS.createGrid dim
        Assert.AreEqual(dim * dim, Array.length grid);

    [<TestMethod>]
    member this.Test_uncoverGrid () =
        let grid = [|MS.Bomb; MS.Covered; MS.Covered;
                    MS.Covered; MS.Covered; MS.Covered;
                    MS.Covered; MS.Bomb; MS.Covered;|] |> MS.Grid
        let expected = [|MS.Bomb; 1; MS.Empty;
                        MS.Covered; 2; 1;
                        MS.Covered; MS.Bomb; MS.Covered;|] |> MS.Grid
        Assert.AreEqual(expected, MS.uncover grid 3 {MS.X = 0; MS.Y = 2})
                    
        
