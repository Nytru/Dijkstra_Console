using Subway_Vertex;
using System.Collections.Generic;
using System.Linq;

namespace Subway_Dijkstra
{
    internal static class Dijkstra 
    {
        public static List<string> GetNames(long startPosId, long finishPosId, List<Vertex> vertices)
        {
            var id = Solve(startPosId, finishPosId, vertices);

            var ans = new List<string>();
            for (int i = 0; i < id.Count; i++)
            {
                foreach (var item in vertices)
                {
                    if (item.Id == id[i])
                    {
                        ans.Add(item.Name);
                    }
                }
            }
            return ans;
        }

        private static List<long> Solve(long startPosId, long finishPosId, List<Vertex> arr)
        {
            Vertex[] temp = new Vertex[arr.Count];
            arr.CopyTo(temp);
            var vertices = temp.ToDictionary(ver => ver.Id);

            Vertex currentVertex = (from v in vertices
                                   where v.Key == startPosId
                                   select v.Value).ToArray()[0];
            currentVertex.Pin = (startPosId, 0);

            while ((from v in vertices.Values
                    where v.Visited == false
                    select v).Any())
            {
                var order = (from o in currentVertex.Route
                             orderby o.weight
                             select o).ToList();
                foreach (var item in order)
                {
                    if (vertices[item.id].Pin.weight > item.weight + currentVertex.Pin.weight)
                    {
                        vertices[item.id].Pin = (currentVertex.Id, item.weight + currentVertex.Pin.weight);
                    }
                }
                currentVertex.Visited = true;

                var next = (from v in vertices.Values
                            where !v.Visited
                            orderby v.Pin.weight
                            select v).ToList();
                if (next.Any())
                {
                    currentVertex = (from n in vertices
                                     where n.Key == next[0].Id
                                   select n.Value).ToList()[0];
                }
                else
                {
                    continue;
                }
            }

            List<long> ansVertices = new List<long>();
            var currentId = finishPosId;
            while (currentId != startPosId)
            {
                ansVertices.Add(currentId);
                currentId = vertices[currentId].Pin.id;
            }
            ansVertices.Add(startPosId);


            return ansVertices;
        } 
    }
}
