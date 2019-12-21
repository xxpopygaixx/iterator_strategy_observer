using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesko
{
    class odds : IObserver
    {
        IObservable graph;
        public odds(IObservable obs)
        {
            graph = obs;
            graph.RegisterObserver(this);
        }
        public void Update(Object ob)
        {
            if(int.Parse(ob.ToString())%2==1)
            {
                Console.Write("\nodds\t");
            }
        }
    }

    class evens : IObserver
    {
        IObservable graph;
        public evens(IObservable obs)
        {
            graph = obs;
            graph.RegisterObserver(this);
        }
        public void Update(Object ob)
        {
            if (int.Parse(ob.ToString()) % 2 == 0)
            {
                Console.Write("\nevens\t");
            }
        }
    }
}
