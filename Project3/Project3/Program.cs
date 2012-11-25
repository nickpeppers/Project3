using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Project3
{
    class Program
    {
        private const int BUFFER_MAX = 20;
        private static Queue<int> buffer = new Queue<int>();
        private static int count = 0;
        private static object locker = new object();

        static void Main(string[] args)
        {
            new Thread(Producer).Start();

            new Thread(Consumer).Start();
        }

        public static bool Produce(int x)
        {
            // determines whether a thread can go into the block
            lock (locker)
            {
                if (buffer.Count >= BUFFER_MAX)
                {
                    return false;
                }

                // adds the number produced to the buffer
                buffer.Enqueue(x);
                return true;
            }
        }

        public static int Consume()
        {
            // determines whether a thread can go into the block
            lock (locker)
            {
                if (buffer.Count == 0)
                {
                    return -1;
                }

                // removes number from the buffer
                return buffer.Dequeue();
            }
        }

        public static void Producer()
        {
            while (true)
            {
                // tells producer thread how long to wait
                Thread.Sleep(200);

                // adds to count to show the number produced
                bool worked = Produce(++count);
                if (!worked)
                {
                    // subtracts from count if queue is full to produce correct number
                    count--;
                }
                // prints full if the buffer is full
                string text = worked ? count.ToString() : "full";
                // prints the number produced
                Console.WriteLine("Producer: " + text);
            }
        }

        public static void Consumer()
        {
            while (true)
            {
                // tells consumer thread how long to wait
                Thread.Sleep(1000);

                int result = Consume();
                // prints empty if the the queue became empty
                string text = result == -1 ? "empty" : result.ToString();
                // prints the number consumed
                Console.WriteLine("Consumer: " + text);
            }
        }
    }
}
