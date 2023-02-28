namespace ROP

open System

type InvoiceId = InvoiceId of Guid // unique signature for InvoiceId that depends on string
module InvoiceId =
    let value (InvoiceId invoiceId) = invoiceId // Deconstruct when we have InvoiceId and needs to extract value as string

type Currency = Currency of string
module Currency =
    let value (Currency currency) = currency

// Discriminated Unions
//https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/pattern-matching
type InvoiceStatus =
    | Pending
    | Declined
    | Accepted

type InvoiceAggregate =
    {
        Id: InvoiceId
        Amount: Decimal
        Currency: Currency
        Status: InvoiceStatus
        IsOnHold: Boolean
        CreatedDate: DateTime
    }