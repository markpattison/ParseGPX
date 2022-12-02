## Parsing a GPX file using FSharp.Data

This is part of the [F# Advent Calendar in English 2022](https://sergeytihon.com/2022/10/28/f-advent-calendar-in-english-2022/).

[GPX](https://en.wikipedia.org/wiki/GPS_Exchange_Format) is an XML schema commonly used for recording runs, bike rides and other activities.  If you have a sports watch or use your phone to record workouts, the chances are that you'll be able to export a GPX file.

Using [FSharp.Data](https://fsprojects.github.io/FSharp.Data/) we can easily pull out data we're interested in.  In this example, I extract timing and location data and use them to summarise a track workout.

GPX files typically contain location snapshots every second.  By treating larger gaps (i.e. rests) differently, I can break up a workout into its constituent parts.  Using the latitude and logitude data, I can estimate the distance covered in each part using a [formula from StackOverflow](https://stackoverflow.com/a/51839058/15519).

### Sample output

For the included [track session](track_session.gpx) (on a running track in Luton, UK) we were tasked with doing four sets of sprints, with two-minute rests in between.

Each set consisted of four 200m sprints alternated with 30 seconds rest 🥵.  Here's how we got on - you can see that some of the rests were a bit longer than scheduled...

	202m effort 0:37, rest 0:40
	190m effort 0:39, rest 0:42
	207m effort 0:43, rest 0:35
	196m effort 0:40, rest 2:06
	202m effort 0:40, rest 0:40
	195m effort 0:43, rest 0:37
	208m effort 0:44, rest 0:34
	196m effort 0:43, rest 2:07
	204m effort 0:42, rest 0:41
	189m effort 0:42, rest 0:34
	207m effort 0:45, rest 0:45
	196m effort 0:43, rest 2:03
	206m effort 0:45, rest 0:47
	195m effort 0:45, rest 0:40
	206m effort 0:46, rest 0:41
	198m effort 0:38

For any other F# runners out there, check out the [Strava group](https://www.strava.com/clubs/723616)!
