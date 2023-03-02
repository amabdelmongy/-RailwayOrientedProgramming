namespace FSharpSyntax

module DiscriminatedUnions =

    // ======== Record types =====================
    // Record types have named fields. Semicolons are separators. //C# now has record
    type Person = { First:string; Last:string}
    let person = { Person.First = "john"; Last = "Doe" }


    // ======== Discriminated Unions ====================
    // Unions, or discriminated unions allows you to build up complex data structures representing well-defined set of choices.
    type Shape =
        | Rectangle of height : float * width : float
        | Circle of radius : float

    // ======== Pattern Matching ========
    // Match..with.. is a supercharged case/switch statement.
    // How do know the type of the shape?
    let matchShape shape =
        match shape with
        | Rectangle(height,_) -> printfn $"Rectangle with height %f{height}"
        | Circle(radius) -> printfn $"Circle with radius %f{radius}"
        | _ -> printfn " shape is something else"   // underscore matches anything

    let Run =
        let rectangle = Shape.Rectangle (200,50)
        let circle = Shape.Circle 100

        // We want to know the type of the shape
        matchShape rectangle
        matchShape circle
