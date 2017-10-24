using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSPF_V
{
    class Program
    {
        static void Main(string[] args)
        {

            OSPF myOSPF = new OSPF();
            myOSPF.Initialization(6);
            myOSPF.TimeOut = 1000;
            myOSPF.TimeNow = 0;
            

            while (myOSPF.TimeNow < myOSPF.TimeOut)
            {
                Event DoNow = new Event();
                if(DoNow.Type == (int)EventType.SendHello)
                {
                    int tmp = myOSPF.TimeNow;
                    myOSPF.SendHello(ref tmp);
                    myOSPF.TimeNow = tmp;
                    DoNow.Type = (int)EventType.ConfirmNeighbor;
                }

                if(DoNow.Type == (int)EventType.ConfirmNeighbor)
                {
                    int tmp = myOSPF.TimeNow;
                    myOSPF.ConfirmNeighbor(ref tmp);
                    myOSPF.TimeNow = tmp;
                }
            }
            Console.ReadKey();
        }
    }
}
