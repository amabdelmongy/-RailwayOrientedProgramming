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
                    CreatedDate = DateTime.UtcNow
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

        let getInvoice invoiceId =
            let result =
                parseInvoiceId
                >> Result.map      getInvoiceById       // “map” (fmap) for integrating non-monadic functions into the pipeline.
                >> Result.bind     verifyResultIsSome   //“bind” for integrating monadic functions into the pipeline.
                >> Result.map      convertInvoiceToDto
                >> Result.map      convertToResponse
                >> Result.mapError getInvoiceHttpErrorHandler.ConvertErrorToResponse

            match result invoiceId with
            | Ok result -> result
            | Error result -> result

        {
            GetInvoice = getInvoice
        }
