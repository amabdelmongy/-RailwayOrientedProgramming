open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.DependencyInjection
open Giraffe
open ROP

let webApp =
        choose [
            GET >=> choose [ //Kleisli composition (>=>) for composing monadic functions.
                route "/ping" >=> text "pong"
                route "/" >=> text "Hello World!"
                routef "/invoices/%s" (fun id ->
                    let getInvoiceHttpHandler =
                        GetInvoiceHttpErrorHandler.provide()
                        |> GetInvoiceHttpHandler.provide

                    getInvoiceHttpHandler.GetInvoice id)
            ]
        ]

let configureApp (app : IApplicationBuilder) =
    // Add Giraffe to the ASP.NET Core pipeline
    app.UseGiraffe webApp

let configureServices (services : IServiceCollection) =
    // Add Giraffe dependencies
    services.AddGiraffe() |> ignore

[<EntryPoint>]
let main _ =
    Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults(
            fun webHostBuilder ->
                webHostBuilder
                    .Configure(configureApp)
                    .ConfigureServices(configureServices)
                    |> ignore)
        .Build()
        .Run()
    0


