namespace ROP

open System

type InvoiceCreated =
    {
        Id: InvoiceId
        Amount: Decimal
        Currency: Currency
        Status: InvoiceStatus
        IsOnHold: Boolean
        CreatedDate: DateTime
    }

type InvoiceSetOnHold =
    {
        InvoiceId: InvoiceId
    }

type InvoiceReleasedFromHold =
    {
        InvoiceId: InvoiceId
    }

type InvoiceAccepted =
    {
        InvoiceId: InvoiceId
    }

type InvoiceEvent =
    | InvoiceCreated of InvoiceCreated
    | InvoiceSetOnHold of InvoiceSetOnHold
    | InvoiceReleasedFromHold of InvoiceReleasedFromHold
    | InvoiceAccepted of InvoiceAccepted

