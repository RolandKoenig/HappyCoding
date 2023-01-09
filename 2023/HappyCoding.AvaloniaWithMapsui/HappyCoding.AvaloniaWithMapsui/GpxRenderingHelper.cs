using System.Collections.Generic;
using Mapsui.Projections;
using NetTopologySuite.Geometries;
using RolandK.Formats.Gpx;

namespace HappyCoding.AvaloniaWithMapsui;

internal static class GpxRenderingHelper
{
    public static LineString? GpxWaypointsToMapsuiGeometry(this IEnumerable<GpxWaypoint> waypoints)
    {
        var linePoints = new List<Coordinate>();
        foreach (var actPoint in waypoints)
        {
            var point = SphericalMercator.FromLonLat(actPoint.Longitude, actPoint.Latitude);
            linePoints.Add(new Coordinate(point.x, point.y));
        }
        if (linePoints.Count < 2) { return null; }

        return new LineString(linePoints.ToArray());
    }
}
