open ParseGpx

let path = __SOURCE_DIRECTORY__ + @"\track_session.gpx"

let summary =
    Gpx.Load(path)
    |> ParseGpx.parse

summary
|> List.iter (fun s ->
    match s with
    | Effort (t, distance) -> printf "%.0fm effort %i:%02i" distance t.Minutes t.Seconds
    | Rest t -> printfn ", rest %i:%02i" t.Minutes t.Seconds)

printfn ""
