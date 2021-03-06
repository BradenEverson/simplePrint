﻿using PointsArray.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace pointArray.Data
{
    public class InMemoryPointLayerDatabase : IPointData
    {
        private List<List<Point>> points;
        public InMemoryPointLayerDatabase()
        {
            points = new List<List<Point>>();
        }
        public List<Point> add(List<Point> newLayer)
        {
            points.Add(newLayer);
            return newLayer;
        }
        public int getLength()
        {
            return points.Count;
        }
        public List<List<Point>> returnData()
        {
            return points;
        }
        public void deleteAll()
        {
            points = new List<List<Point>>();
        }
    }
}
