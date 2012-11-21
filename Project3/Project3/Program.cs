using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Project3
{
    class Program
    {
        private Stack<int> stack = new Stack<int>();
        private int count = 0;
        private object locker = new object();

        static void Main(string[] args)
        {

        }

        public void Push(int x)
        {
            lock (locker)
            {
                stack.Push(x);
            }
        }

        public int Pop()
        {
            lock (locker)
            {
                if (stack.Count == 0)
                {
                    return -1;
                }

                return stack.Pop();
            }
        }

        public void Producer()
        {
            while (true)
            {
                Thread.Sleep(200);

                Push(++count);
                Console.WriteLine("Producer: " + count);
            }
        }

        public void Consumer()
        {
            while (true)
            {
                Thread.Sleep(100);

                Console.WriteLine("Consumer: " + Pop());
            }
        }
    }
}
