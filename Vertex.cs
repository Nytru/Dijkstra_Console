using System;
using System.Collections.Generic;

namespace Subway_Vertex
{
    internal class Vertex
    {
        public Vertex()
        {
            Name = "none";
            Id = -1;
            Pin = (-1, double.PositiveInfinity);
            Visited = false;
        }

        public Vertex(string name, long id)
        {
            Name = name;
            Id = id;
            Pin = (-1, double.PositiveInfinity);
            Visited = false;
        }

        public bool Visited { get; set; }

        private List<(long id, double weight)> _route = new();

        public List<(long id, double weight)> Route { get => _route; set => _route = value; }

        private (long, double) _path;

        public (long id, double weight) Pin { get => _path; set => _path = value; }

        public string Name { get; set; }
        public long Id { get; set; }
    }
}
