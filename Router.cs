using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSPF_V
{
    class Neighbor : Router
    {
        //public int IDNB { get; set; }
        public int Metric { get; set; }

        public Neighbor(int id )
        {
            this.ID = id;
            Random rnd = new Random();
            this.Metric = rnd.Next(1, 50);
        }
    }

    class Link
    {
        public int First { get; set; }
        public int End { get; set; }
        public int Metric { get; set; }
    }
    class Router
    {
        // ID Router
        public int ID { get; set; }

        // Link state database
        public int[,] LSDB { get; set; }

        // List neighbor
        static public List<Neighbor> MyNeighbor = new List<Neighbor>();

        public List<Router> ListConected = new List<Router>();

        public Link[]  myLSA { get; set; }

        public void BuidLSA()
        {
            for(int i =0 ; i < MyNeighbor.Count ; i++)
            {
                Link tmp = new Link();
                tmp.End = MyNeighbor[i].ID;
                tmp.First = this.ID;
                tmp.Metric = MyNeighbor[i].Metric;
            }
        }

  

        public int GetNumberNeighbor()
        {
            return MyNeighbor.Count;
        }
        // Route Table
        int[,] RouteTable;

        List<Router> Conected = new List<Router>();

        // Conect to

        public void ConectTo(Router Destination)
        {
            Conected.Add(Destination);
        }

        public void NewNeighbor(Neighbor Newneighbor)
        {
            MyNeighbor.Add(Newneighbor);
        }

        public void BuidLSDB(Router R)
        {
            for(int i = 0 ; i < R.GetNumberNeighbor() ; i++)
            {
                LSDB[R.ID, R.myLSA[i].End] = R.myLSA[i].Metric;
                LSDB[i, R.myLSA[i].End] = R.myLSA[i].Metric;
            }
        }

        public bool HelloResponse(Router Destination)
        {
            for(int i = 0 ; i < Conected.Count ; i++)
            {
                if (Destination.ID == Conected[i].ID)
                    return true;
            }
            return false;
        }
        public void SendHello(Router Destination)
        {

        }
        public void SendLSA(Router Destination)
        {

        }

        public void BuidRouteTable()
        {

        }

        
    }
}
