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

type PaymentAggregate =
    {
        Id: PaymentId
        Status: PaymentStatus
        IsOnHold: Boolean
        CreatedDate: DateTime
    }