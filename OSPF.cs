using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSPF_V
{

    enum EventType
    {
        SendHello, SendLSA, BuidLSDB, Dijkstra, BuidRouteTable, ConfirmNeighbor
    }
    class Event
    {
        public int Eventt { get; set; }
        public int Time { get; set; }

        public int Type { get; set; }

        //public Event (int _type , int _time)
        //{
        //    Type = _type;
        //    Time = _time;
        //}

        //public Event();


    }
    class OSPF
    {
        public int TimeOut { get; set; }
        public int TimeNow { get; set; }
        public List<Router> Topology = new List<Router>();

        public int[,] myMap { get; set; }

        public void SetMap(int[,] Map)
        {
            myMap = Map;
        }
        
        public void Initialization(int NumberNode)
        {
           for(int i =0 ; i < NumberNode ; i++)
           {
               Router tmp = new Router();
               tmp.ID = i;
               Topology.Add(tmp);
           }
           myMap = new int[6, 6] { { 0, 1, -1, -1, -1, -1 }, { 1, 0, 7, 15, 3, -1 }, { 0, 7, 0, -1, 4, 21 }, { -1, 15, -1, 0, 25, -1 }, { -1, 3, 4, 25, 0, 6 }, { -1, -1, 21, -1, 6, 0 } };

           for(int i = 0 ; i < 6 ; i++)
           {
               for(int j = 0 ; j < 6 ; j++)
               {
                   if(myMap[i,j] > 0)
                   {
                       Topology[i].ConectTo(Topology[j]);
                   }
               }
           }    

            for(int i = 0 ; i < 6 ; i++)
            {
                for(int j = 0 ; j < 6 ; j++)
                {
                    Console.Write("  {0}  ", myMap[i, j]);
                }
                Console.WriteLine();
                Console.WriteLine("------------------------------------");
            }
        }

        public void SendHello(ref int TimeNow)
        {
            Random rdn = new Random();
            for(int i = 0 ; i < Topology.Count ; i++)
            {
                for(int j = 0 ; j < Topology.Count ; j++)
                {
                    if(i != j)
                    {
                        Topology[i].SendHello(Topology[j]);
                        
                        TimeNow += 2;
                        Console.WriteLine("Router [{0}] send hello to Router [{1}]    Time : {2}ms", Topology[i].ID, Topology[j].ID, TimeNow);
                        if(Topology[i].HelloResponse(Topology[j]))
                        {
                            int tmp = rdn.Next(11);
                            Console.WriteLine("Router[{0}] responsed Router [{1}] at Time {2}ms : ", j, i, TimeNow + tmp);
                            TimeNow += tmp;
                        }
                        else
                        {
                            Console.WriteLine("Request time out - 30ms has left, Router[{0}] not responsed Router [{1}]", j, i);
                            TimeNow += 30;
                        }
                    }
                }
            }
        }

        public void ConfirmNeighbor(ref int  TimeNow)
        {
            for(int i = 0 ; i < Topology.Count ; i++)
            {
                for(int j = 0 ; j < Topology.Count; j++)
                {
                    if(myMap[i,j] > 0)
                    {
                        Console.WriteLine("Router {0} is neighbor Router {1} --- Metric = {2}", i, j, myMap[i, j]);
                        TimeNow++;
                    }
                }
            }
            
        }

        ////////////

        
        

    }
}
