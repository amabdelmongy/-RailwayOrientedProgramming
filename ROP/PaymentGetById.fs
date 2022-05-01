namespace ROP

open System
open Microsoft.AspNetCore.Http

type GetPaymentHttpHandler =
    {
        GetPayment: unit -> IResult
    }

module GetPaymentHttpHandler =
    let provide getPaymentHttpErrorHandler =
        let parsePaymentId stringPaymentId =
            match String.IsNullOrWhiteSpace stringPaymentId with
            | false -> stringPaymentId |> Ok
            | _ -> Error GetPaymentError.InvalidId

        let getPaymentById id =
            //todo call repository to get payment by id
            {
                PaymentAggregate.Id = id |> PaymentId
                Status = PaymentStatus.Pending
                IsOnHold = false
                CreatedDate =DateTime.UtcNow
            }
            |> Some

        let verifyResultIsSome result =
            match result with
            | Some result -> Ok result
            | None -> Error GetPaymentError.PaymentNotFound

        let convertPaymentToDto payment =
            PaymentToDto.convertPaymentToDto payment

        let convertToResponse paymentDto =
            Results.Json paymentDto

        //Railway Oriented Programming
        //https://fsharpforfunandprofit.com/rop/
        let getPayment () =
            let paymentId = "1" //Todo read from route value
            let result =
                parsePaymentId
                >> Result.map      getPaymentById
                >> Result.bind     verifyResultIsSome
                >> Result.map      convertPaymentToDto
                >> Result.map      convertToResponse
                >> Result.mapError getPaymentHttpErrorHandler.ConvertErrorToResponse

                //>> Function composition
                //Functions in F# can be composed from other functions.
                //the composition of two functions function1 and function2 is another
                //function that represents the application of function1 followed
                //the application of function2:

            match result paymentId with
            | Ok result -> result
            | Error result -> result

        {
            GetPayment = getPayment
        }
