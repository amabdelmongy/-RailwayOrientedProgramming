namespace ROP

open System

type PaymentId = PaymentId of Guid // unique signature for PaymentId that depends on string
module PaymentId =
    let value (PaymentId paymentId) = paymentId // Deconstruct when we have PaymentId and needs to extract value as string

// Discriminated Unions
//https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/pattern-matching
type PaymentStatus =
    | Pending
    | Declined
    | Captured

//not used only as example of discriminated unions
//It shouldn't be all of the same string type
type Shape =
    | Rectangle of height : float * width : float
    | Circle of radius : float
module Shape =
    let matchShape shape =
        match shape with
        | Rectangle(height = h) -> printfn $"Rectangle with length %f{h}"
        | Circle(r) -> printfn $"Circle with radius %f{r}"

type PaymentAggregate =
    {
        Id: PaymentId
        Status: PaymentStatus
        IsOnHold: Boolean
        CreatedDate: DateTime
    }