namespace ROP

open System

//dto should contains only primitive types like string and bool
type PaymentDto =
    {
        Id: string
        Status: string
        IsOnHold: Boolean
        CreatedDate: DateTime
    }

module PaymentToDto =
    let private convertPaymentStatusToDtoValue status =
        match status with
        | Pending -> "Pending"
        | Declined -> "Declined"
        | Captured -> "Captured"

    let convertPaymentToDto (payment: PaymentAggregate) =
        {
            PaymentDto.Id = payment.Id |> PaymentId.value
            Status = payment.Status |> convertPaymentStatusToDtoValue
            IsOnHold = payment.IsOnHold
            CreatedDate = payment.CreatedDate
        }