namespace FSharpSyntax

module Functions =
    let Run =

       // ======== Functions ========
       // The "let" keyword also defines a named function.
       // In F# returns are implicit -- no "return" needed. A function always
       // returns the value of the last expression used.
       let square x = x * x                        // Note that no parens are used.

       let squareResult = square 3                 // Now run the function. Again, no parens.
       printfn $"square 3 = %i{squareResult}"

       let add x y = x + y                         // don't use add (x,y)! It means something completely different.
       let addResult = add 2 3                     // Now run the function.
       printfn $"add 2 3 = %i{addResult}"

       // You can pipe the output of one operation to the next using "|>“
       let sumOfSquare = 3 |> add 2 |> square      // output will be 25
       printfn $"3 |> add 2 |> square = %i{sumOfSquare}"
