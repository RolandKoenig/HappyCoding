using System.Collections.Generic;
using RolandK.Formats.Gpx;

namespace HappyCoding.AvaloniaWithMapsui.SelectionModule.Interface;

public interface IGpxFileSelectionManager
{
    void SetSelectedGpxFile(GpxFile? gpxFile);

    GpxFile? GetSelectedGpxFile();
}