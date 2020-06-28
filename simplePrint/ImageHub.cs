using Microsoft.AspNetCore.SignalR;
using pointArray.Data;
using PointsArray.Core;
using System;
using System.Collections.Generic;
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
            Console.WriteLine(pointArray);
            List<Point> pointsOfCurrentLayer = new List<Point>();
            points.add(pointsOfCurrentLayer);
            Console.WriteLine(points.getLength());
            await Clients.Caller.SendAsync("ReceiveMessage", pointArray);
        }
    }
}