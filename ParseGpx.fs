module ParseGpx

open System
open FSharp.Data

let [<Literal>] samplePath = __SOURCE_DIRECTORY__ + @"\sample.gpx"

type Gpx = XmlProvider<samplePath>

let minimumRest = TimeSpan.FromSeconds(15.0)

type Interval = Gpx.Trkpt * Gpx.Trkpt

type Activity =
    | EffortIntervals of Interval list
    | RestInterval of Interval

type ActivitySummary =
    | Effort of TimeSpan * float // distance
    | Rest of TimeSpan

let private isRest (interval: Interval) =
    let point1, point2 = interval
    point2.Time - point1.Time > minimumRest

let private accumulateActivities activities interval =
    match isRest interval, activities with
    | true, _ -> RestInterval interval :: activities
    | false, EffortIntervals currentIntervals :: tail -> EffortIntervals (interval :: currentIntervals) :: tail
    | false, previousActivities -> EffortIntervals [ interval ] :: previousActivities

let private toSummary activity =
    match activity with
    | RestInterval (point1, point2) -> Rest (point2.Time - point1.Time)
    | EffortIntervals intervals ->

        let distance =
            intervals
            |> List.sumBy (fun (point1, point2) -> Geo.getDistanceInMetres (float point1.Lat, float point1.Lon) (float point2.Lat, float point2.Lon))

        let firstPoint, _ = intervals.Head
        let _, lastPoint = intervals |> List.last

        Effort (lastPoint.Time - firstPoint.Time, distance)

let parse (activity: Gpx.Gpx) =
    activity.Trk.Trkseg.Trkpts
    |> Array.toList
    |> List.pairwise
    |> List.fold accumulateActivities []
    |> List.map (fun activity ->
        match activity with
        | RestInterval _ -> activity
        | EffortIntervals intervals -> EffortIntervals (List.rev intervals))
    |> List.rev
    |> List.map toSummary
