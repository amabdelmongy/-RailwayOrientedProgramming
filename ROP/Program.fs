open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.Hosting
open ROP

[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)
    let app = builder.Build()

    app.MapGet("/",
            Func<string>(fun () -> "Hello Checkout!")
        )
        |> //Pipelines let result = 100 |> function1 |> function2
        ignore

    app.MapGet("/payments/{id}",
                Func<IResult>(fun () ->
                    let getPaymentHttpHandler =
                        GetPaymentHttpErrorHandler.provide()
                        |> GetPaymentHttpHandler.provide

                    getPaymentHttpHandler.GetPayment())

                ) |> ignore

    app.Run()

    0 // Exit code

