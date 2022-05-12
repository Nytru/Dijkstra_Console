using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subway_Vertex;

namespace Subway_Manager
{
    internal static class FileManager
    {
        internal static List<Vertex> ReadFromFile(string path)
        {
            bool isWeights = false;
            string str;
            var reader = new StreamReader(path);
            var vertices = new List<Vertex>();
            var weights = new Dictionary<(long, long), double>();
            while (!reader.EndOfStream)
            {
                str = reader.ReadLine();
                if (str == "=")
                {
                    isWeights = true;
                    continue;
                }
                if (!isWeights)
                {
                    string id = str.Substring(0, str.IndexOf(' '));
                    str = str.Substring(str.IndexOf(' ') + 1);
                    vertices.Add(new Vertex(str, Convert.ToInt64(id)));
                }
                else
                {
                    (long firstStation, long secondStation) way;
                    way.firstStation = Convert.ToInt64(str.Substring(0, str.IndexOf(' ')));
                    str = str.Remove(0, str.IndexOf(' ') + 1);
                    way.secondStation = Convert.ToInt64(str.Substring(0, str.IndexOf(' ')));

                    weights.Add(way, (double)Convert.ToDecimal(str.Substring(str.IndexOf(' ') + 1)));
                }
            }

            return SolveFile(vertices, weights);
        }

        private static List<Vertex> SolveFile(List<Vertex> vertices, Dictionary<(long, long), double> weights)
        {
            foreach (var item in weights)
            {
                foreach (var v in vertices)
                {
                    if (v.Id == item.Key.Item1 | v.Id == item.Key.Item2)
                    {
                        if (v.Id == item.Key.Item1)
                        {
                            v.Route.Add((item.Key.Item2, item.Value));
                        }
                        else if (v.Id == item.Key.Item2)
                        {
                            v.Route.Add((item.Key.Item1, item.Value));
                        }
                        else
                        {
                            throw new Exception("gg");
                        }
                    }
                }
            }
            return vertices;
        }
    }
}
