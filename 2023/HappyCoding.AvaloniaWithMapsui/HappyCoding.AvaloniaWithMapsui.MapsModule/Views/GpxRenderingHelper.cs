using System.Collections.Generic;
using Mapsui.Projections;
using Mapsui.Styles;
using NetTopologySuite.Geometries;
using RolandK.Formats.Gpx;

namespace HappyCoding.AvaloniaWithMapsui.MapsModule;

#pragma warning disable CS8670 // Object or collection initializer implicitly dereferences possibly null member.

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
    
    public static IStyle CreateLineStringStyle_Default()
    {
        return new VectorStyle
        {
            Fill = null,
            Outline = null,
            Line = { Color = Color.Black, Width = 8 }
        };
    }
    
    public static IStyle CreateLineStringStyle_Selected()
    {
        return new VectorStyle
        {
            Fill = null,
            Outline = null,
            Line = { Color = Color.Yellow, Width = 6 }
        };
    }
}
