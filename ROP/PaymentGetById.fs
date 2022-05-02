namespace ROP

open System
open Microsoft.AspNetCore.Http
open Giraffe

type GetPaymentHttpHandler =
    {
        GetPayment: string -> HttpHandler
    }

module GetPaymentHttpHandler =
    let provide getPaymentHttpErrorHandler =
        let parsePaymentId (stringPaymentId:string) =
            match Guid.TryParse(stringPaymentId) with
            | true, parsedGuid ->  Ok parsedGuid
            | _ -> GetPaymentError.InvalidId |> Error

        let getPaymentById id =
            //todo call repository to get payment by id
            match id.ToString() with
            | "1d551eed-a974-4770-a824-635b4b72447f" ->
                {
                    PaymentAggregate.Id = id |> PaymentId
                    Status = PaymentStatus.Pending
                    IsOnHold = false
                    CreatedDate =DateTime.UtcNow
                }
                |> Some
            | _ -> None

        let verifyResultIsSome result =
            match result with
            | Some result -> Ok result
            | None -> Error GetPaymentError.PaymentNotFound

        let convertPaymentToDto payment =
            PaymentToDto.convertPaymentToDto payment

        let convertToResponse paymentDto =
            setStatusCode StatusCodes.Status200OK >=> json paymentDto //Kleisli composition (>=>) for composing monadic functions.

        //Railway Oriented Programming
        //https://fsharpforfunandprofit.com/rop/
        let getPayment paymentId =
            let result =
                parsePaymentId
                >> Result.map      getPaymentById
                >> Result.bind     verifyResultIsSome //“bind” (>>=) for integrating monadic functions into the pipeline.
                >> Result.map      convertPaymentToDto  // “map” (fmap) for integrating non-monadic functions into the pipeline.
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
