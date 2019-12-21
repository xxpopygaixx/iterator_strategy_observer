using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesko
{
    class Node<T>
    {
        public T data;
    }
    class Graph<T> : IEnumerable, IObservable
    {
        private Dictionary <T, List<Node<T>>> adj;
        List<Node<T>> queue;
        private Dictionary<T, bool> state;
        Node<T> curnode;
        public IBypass<T> bypassType { private get; set; }
        public List<IObserver> observers;
        public Graph(IBypass<T> bypass)
        {
            adj = new Dictionary<T , List<Node<T>>>();
            bypassType = bypass;
            state = new Dictionary<T, bool>();
            queue = new List<Node<T>>();
            observers = new List<IObserver>();
        }

        private void clearstate()
        {
            List<T> temp = new List<T>();
            temp.AddRange(state.Keys);
            foreach (var item in temp)
            {
                state[item] = false;
            }
        }

        public IEnumerator GetEnumerator()
        {
            clearstate();
            queue.Add(new Node<T> { data = adj.First().Key });
            state[queue[0].data] = true;
            
            while(queue.Count!=0)
            {
                curnode = bypass();
                NotifyObservers();
                foreach (var item in adj[curnode.data])
                {

                    if(!queue.Contains(item) && state[item.data] == false)
                    {
                        queue.Add(item);
                        state[item.data] = true;
                        //break;
                    }
                }
                yield return curnode.data;
            }
            

        }

        public void AddEdge(T source, T distanation)
        {
            
            Node<T> s = new Node<T> { data = source };
            Node<T> d = new Node<T> { data = distanation };
            if(!adj.Keys.Contains(s.data))
            {
                adj.Add(s.data, new List<Node<T>>());
            }
            if (!adj.Keys.Contains(d.data))
            {
                adj.Add(d.data, new List<Node<T>>());
            }
            if (!adj[s.data].Contains(d))
            {
                adj[s.data].Add(d);
            }
            //adj[d].Add(s);
            if(!state.Keys.Contains(source))
            {
                state.Add(source, false);
            }
            if (!state.Keys.Contains(distanation))
            {
                state.Add(distanation, false);
            }
        }

        public Node<T> bypass()
        {
            return bypassType.bypass(queue);
        }

        public void RegisterObserver(IObserver o)
        {
            observers.Add(o);
        }

        public void RemoveObserver(IObserver o)
        {
            observers.Remove(o);
        }

        public void NotifyObservers()
        {
            foreach (IObserver o in observers)
            {
                o.Update(curnode.data);
            }
        }
    }

    interface IBypass<T>
    {
        Node<T> bypass(List<Node<T>> queue);
    }

    class BFS<T> : IBypass<T>
    {
        public Node<T> bypass(List<Node<T>> queue)
        {
            //Console.WriteLine("bfs");
            Node<T> res = queue[0];
            queue.RemoveAt(0);
            return res;
        }
    }

    class DFS<T> : IBypass<T>
    {
        public Node<T> bypass(List<Node<T>> queue)
        {
            //Console.WriteLine("dfs");
            Node<T> res = queue[queue.Count-1];
            queue.RemoveAt(queue.Count - 1);
            return res;
        }
    }

    interface IObserver
    {
        void Update(Object ob);
    }

    interface IObservable
    {
        void RegisterObserver(IObserver o);
        void RemoveObserver(IObserver o);
        void NotifyObservers();
    }
}
