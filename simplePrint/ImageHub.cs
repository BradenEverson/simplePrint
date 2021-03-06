﻿using Microsoft.AspNetCore.SignalR;
using pointArray.Data;
using PointsArray.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace simplePrint
{
    public class ImageHub : Hub
    {
        private readonly IPointData points;
        public ImageHub(IPointData points)
        {
            this.points = points;
        }
        public async Task SendMessage(string pointArray)
        {
            if (pointArray == "repeat")
            {
                if (points.returnData().Count > 0)
                {
                    points.add(points.returnData()[points.returnData().Count - 1]);
                }
            }
            else
            {
                List<Point> pointsOfCurrentLayer = new List<Point>();
                int id = 0;
                foreach (string pointString in pointArray.Split(','))
                {
                    Point point = new Point();
                    string[] xyPoints = pointString.Split('&');
                    point.x = Int32.Parse(xyPoints[0]);
                    point.y = Int32.Parse(xyPoints[1]);
                    point.id = id;
                    pointsOfCurrentLayer.Add(point);
                    id++;
                }
                //Todo: Clean the data better by distincting it.
                pointsOfCurrentLayer = pointsOfCurrentLayer.GroupBy(f => f.id).Select(r => r.FirstOrDefault()).ToList();
                points.add(pointsOfCurrentLayer);
                foreach (Point p in pointsOfCurrentLayer)
                {
                    Console.Write("(" + p.x + "," + p.y + ") ");
                }
                Console.WriteLine();
                Console.WriteLine(points.getLength());
                await Clients.Caller.SendAsync("ReceiveMessage", pointArray);
            }
        }
    }
}