// For more information see https://aka.ms/fsharp-console-apps

// "Variables" (but not really)
let myInt = 5.                              // The "let" keyword defines an (immutable) value
let myFloat = 3.14
let myString = "hello"                      //note that no types needed


// ======== Lists =====================
let twoToFive = [2;3;4;5]                   // Square brackets create a list with
let oneToFive = [1..5]
printfn $"list 1 to 5 = %A{oneToFive}"

// ========= Complex Data Types =========
// Tuple types are pairs, triples, etc. Tuples use commas.
let twoTuple = 1,2
let threeTuple = "a",2,true


// ======== Functions ========
// The "let" keyword also defines a named function.
let square x = x * x                        // Note that no parens are used.
let squareResult = square 3                 // Now run the function. Again, no parens.
printfn $"square 3 = %i{squareResult}"
let sumOfSquares n =
   [1..n] |> List.map square |> List.sum
printfn $"sumOfSquares 1 to 4 = %i{sumOfSquares 4}" // 1 + 4 + 9 + 16 = 30
let add x y = x + y                         // don't use add (x,y)! It means something completely different.
let addResult = add 2 3                     // Now run the function.
printfn $"add 2 3 = %i{addResult}"

// You can pipe the output of one operation to the next using "|>“
let sumOfSquaresTo100piped = 3 |> add 2 |> square             // output will be 25
printfn $"3 |> add 2 |> square = %i{sumOfSquaresTo100piped}"

// ======== Record types =====================
// Record types have named fields. Semicolons are separators. //C# now has record
type Person = { First:string; Last:string}
let person1 = { Person.First = "john"; Last = "Doe" }

// ======== Discriminated Unions =====================
// Union types have choices. Vertical bars are separators.
type Shape =
    | Rectangle of height : float * width : float
    | Circle of radius : float

let circle = Shape.Circle 100
let rectangle = Shape.Rectangle (200,50)

// ======== Pattern Matching ========
// Match..with.. is a supercharged case/switch statement.
let matchShape shape =
    match shape with
    | Rectangle(h,_) -> printfn $"Rectangle with length %f{h}"
    | Circle(r) -> printfn $"Circle with radius %f{r}"
    | _ -> printfn " shape is something else"   // underscore matches anything

// We want to know the type of the shape
matchShape rectangle
matchShape circle

// In F# returns are implicit -- no "return" needed. A function always
// returns the value of the last expression used.

