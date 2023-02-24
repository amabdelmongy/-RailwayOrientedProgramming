// For more information see https://aka.ms/fsharp-console-apps
// "Variables" (but not really)
let myInt = 5.                              // The "let" keyword defines an (immutable) value
let myFloat = 3.14
let myString = "hello"                      //note that no types needed

// ======== Lists ============
let twoToFive = [2;3;4;5]                   // Square brackets create a list with

// ========= Complex Data Types =========
// Tuple types are pairs, triples, etc. Tuples use commas.
let twoTuple = 1,2
let threeTuple = "a",2,true

// ======== Functions ========
// The "let" keyword also defines a named function.
let square x = x * x                        // Note that no parens are used.
let squareResult = square 3                 // Now run the function. Again, no parens.
printfn $"square 3 = %i{squareResult}"

let add x y = x + y                         // don't use add (x,y)! It means something completely different.
let addResult = add 2 3                     // Now run the function.
printfn $"add 2 3 = %i{addResult}"

// You can pipe the output of one operation to the next using "|>“
let sumOfSquaresTo100piped = 3 |> add 2 |> square             // output will be 25
printfn $"3 |> add 2 |> square = %i{sumOfSquaresTo100piped}"

// Record types have named fields. Semicolons are separators. //C# now has record
type Person = { First:string; Last:string}
let person1 = {
                    Person.First = "john";
                    Last = "Doe"
                }

// Discriminated Unions
// Union types have choices. Vertical bars are separators.
type Shape =
    | Rectangle of height : float * width : float
    | Circle of radius : float

// ======== Pattern Matching ========
// Match..with.. is a supercharged case/switch statement.
let matchShape shape =
        match shape with
        | Rectangle(height = h) -> printfn $"Rectangle with length %f{h}"
        | Circle(r) -> printfn $"Circle with radius %f{r}"
        | _ -> printfn " shape is something else"   // underscore matches anything

let bigCircle = Shape.Circle 100
let bigRectangle = Shape.Rectangle (100,200)

matchShape bigCircle
matchShape bigRectangle

// In F# returns are implicit -- no "return" needed. A function always
// returns the value of the last expression used.

