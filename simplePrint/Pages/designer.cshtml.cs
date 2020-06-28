using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using pointArray.Data;
using PointsArray.Core;

namespace simplePrint
{
    public class designerModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string bedTemp { get; set; }
        [BindProperty(SupportsGet = true)]
        public string extTemp { get; set; }
        [BindProperty(SupportsGet = true)]
        public string zRate { get; set; }
        [BindProperty(SupportsGet = true)]
        public string eRate { get; set; }
        private readonly IPointData points;
        public designerModel(IPointData points)
        {
            this.points = points;
        }
        public IActionResult OnPost()
        {
            string fileName = Guid.NewGuid().ToString() + ".gcode";
            float currentZ = 0;
            float currentE = 0;
            List<string> gcodeFile = new List<string>()
            {
                ";Made With Simple Print :)",
                "M107",
                "M190 S" + bedTemp,
                "M104 S" + extTemp,
                "G28",
                "M109 S" + extTemp,
                "G21",
                "G90",
                "M82",
                "G92 E0"
            };
            foreach (List<Point> pointsInLayer in points.returnData())
            {
                foreach (Point point in pointsInLayer)
                {
                    gcodeFile.Add("G1 X" + point.x + " Y" + point.y + " Z" + currentZ + " E" + currentE);
                    currentE += float.Parse(eRate);
                }
                currentZ += float.Parse(zRate);
            }
            gcodeFile.Add("G92 E0");
            var gcode = System.IO.File.CreateText("wwwroot/gcode/" + fileName);
            foreach (string line in gcodeFile)
            {
                gcode.WriteLine(line);
            }
            gcode.Flush();
            gcode.Dispose();
            var net = new System.Net.WebClient();
            var data = net.DownloadData("wwwroot/gcode/" + fileName);
            var content = new System.IO.MemoryStream(data);
            System.IO.File.Delete("wwwroot/gcode/" + fileName);
            points.deleteAll();
            return File(content, "application/force-download", "your-simpleprint-design.gcode");
        }
    }
}