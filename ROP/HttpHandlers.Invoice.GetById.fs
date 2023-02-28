namespace ROP

open System
open Microsoft.AspNetCore.Http
open Giraffe

type GetInvoiceHttpHandler =
    {
        GetInvoice: string -> HttpHandler
    }

module GetInvoiceHttpHandler =
    let provide getInvoiceHttpErrorHandler =
        let parseInvoiceId (stringInvoiceId:string) =
            match Guid.TryParse(stringInvoiceId) with
            | true, parsedGuid ->  Ok parsedGuid
            | _ -> GetInvoiceError.InvalidId |> Error

        let getInvoiceById id =
            //todo call repository to get invoice by id
            match id.ToString() with
            | "1d551eed-a974-4770-a824-635b4b72447f" ->
                {
                    InvoiceAggregate.Id = id |> InvoiceId
                    Currency = "Euro" |> Currency
                    Amount = 100m
                    Status = InvoiceStatus.Pending
                    IsOnHold = false
                    CreatedDate =DateTime.UtcNow
                }
                |> Some
            | _ -> None

        let verifyResultIsSome result =
            match result with
            | Some result -> Ok result
            | None -> Error GetInvoiceError.InvoiceNotFound

        let convertInvoiceToDto invoice =
            InvoiceToDto.convertInvoiceToDto invoice

        let convertToResponse invoiceDto =
            setStatusCode StatusCodes.Status200OK >=> json invoiceDto //Kleisli composition (>=>) for composing monadic functions.

        //Railway Oriented Programming
        //https://fsharpforfunandprofit.com/rop/
        let getInvoice invoiceId =
            let result =
                parseInvoiceId
                >> Result.map      getInvoiceById
                >> Result.bind     verifyResultIsSome //“bind” (>>=) for integrating monadic functions into the pipeline.
                >> Result.map      convertInvoiceToDto  // “map” (fmap) for integrating non-monadic functions into the pipeline.
                >> Result.map      convertToResponse
                >> Result.mapError getInvoiceHttpErrorHandler.ConvertErrorToResponse

                //>> Function composition
                //Functions in F# can be composed from other functions.
                //the composition of two functions function1 and function2 is another
                //function that represents the application of function1 followed
                //the application of function2:

            match result invoiceId with
            | Ok result -> result
            | Error result -> result

        {
            GetInvoice = getInvoice
        }
