module Geo

open System

let getDistanceInMetres (lat1, long1) (lat2, long2) =

    // https://stackoverflow.com/a/51839058/15519

    let d1 = lat1 * (Math.PI / 180.0)
    let num1 = long1 * (Math.PI / 180.0)
    let d2 = lat2 * (Math.PI / 180.0)
    let num2 = long2 * (Math.PI / 180.0) - num1
    let d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0)
    
    6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)))
