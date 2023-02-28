namespace ROP

open Microsoft.AspNetCore.Http
open Giraffe

//Discriminated Unions
type GetInvoiceError =
    | InvalidId
    | InvoiceNotFound

type GetInvoiceErrorHandler =
    {
        ConvertErrorToResponse: GetInvoiceError -> HttpHandler
    }

module GetInvoiceHttpErrorHandler =
    let provide () =
        let convertErrorToResponse (error:GetInvoiceError) =
            match error with
            | InvalidId ->
                 setStatusCode StatusCodes.Status400BadRequest
                >=> json "invalid_id"
            | InvoiceNotFound ->
                  setStatusCode StatusCodes.Status404NotFound
                >=> json "invoice_not_found"

        {
            ConvertErrorToResponse = convertErrorToResponse
        }
