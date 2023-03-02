namespace FSharpSyntax

module VariablesAndList =
    let Run =
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
        printfn $"Three Tuple = %A{threeTuple}"

