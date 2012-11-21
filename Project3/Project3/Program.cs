using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Project3
{
    class Program
    {
        private Queue<int> queue = new Queue<int>();
        private int count = 0;
        private object locker = new object();

        static void Main(string[] args)
        {

        }

        public void Produce(int x)
        {
            lock (locker)
            {
                queue.Enqueue(x);
            }
        }

        public int Consume()
        {
            lock (locker)
            {
                if (queue.Count == 0)
                {
                    return -1;
                }

                return queue.Dequeue();
            }
        }

        public void Producer()
        {
            while (true)
            {
                Thread.Sleep(200);

                Produce(++count);
                Console.WriteLine("Producer: " + count);
            }
        }

        public void Consumer()
        {
            while (true)
            {
                Thread.Sleep(100);

                Console.WriteLine("Consumer: " + Consume());
            }
        }
    }
}
