namespace ROP

open System

//dto should contains only primitive types like string and bool
type InvoiceDto =
    {
        Id: Guid
        Status: string
        Amount: Decimal
        Currency: string
        IsOnHold: Boolean
        CreatedDate: DateTime
    }

module InvoiceToDto =
    let private convertInvoiceStatusToDtoValue status =
        match status with
        | Pending -> "Pending"
        | Declined -> "Declined"
        | Accepted -> "Accepted"

    let convertInvoiceToDto (invoice: InvoiceAggregate) =
        {
            InvoiceDto.Id = invoice.Id |> InvoiceId.value
            Amount = invoice.Amount
            Currency = invoice.Currency |> Currency.value
            Status = invoice.Status |> convertInvoiceStatusToDtoValue
            IsOnHold = invoice.IsOnHold
            CreatedDate = invoice.CreatedDate
        }