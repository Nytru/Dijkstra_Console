using System;
using System.Threading;
using Subway_Vertex;
using Subway_Dijkstra;
using System.Collections.Generic;
using Subway_Manager;
using System.Linq;

namespace Subway
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите начальную: ");
            var firstStation = Console.ReadLine();
            Console.Write("Введите конечную: ");
            var secondStation = Console.ReadLine();

            var array = FileManager.ReadFromFile(@"C:\Users\Lein\Downloads\metro.txt");

            long fistStationId;
            long secondStationId;
            List<string> list = new();
            try
            {
                fistStationId = (from v in array
                                      where v.Name == firstStation
                                      select v.Id).ToArray()[0];

                secondStationId = (from v in array
                                        where v.Name == secondStation
                                        select v.Id).ToArray()[0];
                list = Dijkstra.GetNames(fistStationId, secondStationId, array);
                list.Reverse();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            _ = Console.ReadKey();
        }
    }
}
