namespace ROP

open Microsoft.AspNetCore.Http

//Discriminated Unions
type GetPaymentError =
    | InvalidId
    | PaymentNotFound

type GetPaymentErrorHandler =
    {
        ConvertErrorToResponse: GetPaymentError -> IResult
    }

module GetPaymentHttpErrorHandler =
    let provide () =
        let convertErrorToResponse (error:GetPaymentError) =
            match error with
            | InvalidId ->
                Results.Json("invalid_id", statusCode = StatusCodes.Status400BadRequest)
            | PaymentNotFound -> 
                Results.Json("payment_not_found", statusCode = StatusCodes.Status404NotFound)

        {
            ConvertErrorToResponse = convertErrorToResponse
        }
