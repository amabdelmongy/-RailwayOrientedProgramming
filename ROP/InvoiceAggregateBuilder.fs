namespace ROP

module InvoiceAggregateBuilder =
    let applyEventTo invoice event =
        match invoice with
        | None ->
            match event with
            | InvoiceCreated event ->
                {
                    InvoiceAggregate.Id = event.Id
                    Status = InvoiceStatus.Pending
                    IsOnHold = false
                    Amount = event.Amount
                    Currency = event.Currency
                    CreatedDate = event.CreatedDate
                }
                |> Some
            | _ ->
                failwithf $"Attempted to apply %A{event} on invoice %A{invoice}"
        | Some invoice ->
            match event with
            | InvoiceCreated _ ->
                failwithf $"Attempted to apply %A{event} on invoice %A{invoice}"
            | InvoiceSetOnHold _ ->
                Some { invoice with IsOnHold = true }
            | InvoiceReleasedFromHold _ ->
                Some { invoice with IsOnHold = false }
            | InvoiceAccepted _ ->
                Some { invoice with Status = InvoiceStatus.Accepted }

    //fold: Applies a function to each element of the collection
    //fold:- the accumulator was initialized at the top level,
    // and was passed down to each lower level until the lowest and last level was reached
    let getCurrentStateFrom events =
        events
        |> Seq.fold applyEventTo None