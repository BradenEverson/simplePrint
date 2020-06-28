using PointsArray.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace pointArray.Data
{
    public interface IPointData
    {
        List<Point> add(List<Point> newLayer);
        int getLength();
    }
}
