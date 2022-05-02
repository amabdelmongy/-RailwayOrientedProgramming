namespace ROP

open Microsoft.AspNetCore.Http
open Giraffe

//Discriminated Unions
type GetPaymentError =
    | InvalidId
    | PaymentNotFound

type GetPaymentErrorHandler =
    {
        ConvertErrorToResponse: GetPaymentError -> HttpHandler
    }

module GetPaymentHttpErrorHandler =
    let provide () =
        let convertErrorToResponse (error:GetPaymentError) =
            match error with
            | InvalidId ->
                 setStatusCode StatusCodes.Status400BadRequest
                >=> json "invalid_id"
            | PaymentNotFound ->
                  setStatusCode StatusCodes.Status404NotFound
                >=> json "payment_not_found"

        {
            ConvertErrorToResponse = convertErrorToResponse
        }
