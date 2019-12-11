using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CS_Console_Wait_Multithread
{
    class Program
    {
        public static ArrayList[] ALCInupt;
        public static ArrayList[] ALCOutput;
        public static Thread[] th_C;
        public static int intAns;
        public static void Thread_MT_M(object arg=null)
        {
            int i = 0;
            int threadCount = 0;
            int threadAmount = 10;//執行序總量
            //---
            //ArrayList 物件陣列初始化
            ALCInupt = new ArrayList[threadAmount];
            ALCOutput = new ArrayList[threadAmount];
            th_C = new Thread[threadAmount];
            
            for (i=0;i< threadAmount; i++)
            {
                ALCInupt[i] = new ArrayList();
                ALCOutput[i] = new ArrayList();           
            }
            //---ArrayList 物件陣列初始化

            //---
            //把輸入資料放入對應變數中
            for (i = 0; i < threadAmount; i++)
            {
                int k = (i + 1) * 10;
                for (int j = 0; j < 10; j++)
                {
                    ALCInupt[i].Add((k - j) + "");
                }
            }
            //---把輸入資料放入對應變數中

            //---
            //依序兩兩建立子執行序，並等待執行完畢
            do
            {
                th_C[threadCount] = new Thread(Thread_MT_C);
                th_C[threadCount].Start(threadCount);

                threadCount++;

                if(threadCount< threadAmount)
                {
                    th_C[threadCount] = new Thread(Thread_MT_C);
                    th_C[threadCount].Start(threadCount);
                }


                do
                {
                    Thread.Sleep(100);
                } while ( (th_C[threadCount].IsAlive) && (th_C[(threadCount-1)].IsAlive) );

                string []buf01 = ALCOutput[threadCount][(ALCOutput[threadCount].Count - 1)].ToString().Split(';');
                string[] buf02 = ALCOutput[(threadCount - 1)][(ALCOutput[(threadCount - 1)].Count - 1)].ToString().Split(';');
                intAns += Convert.ToInt32(buf01[1]) + Convert.ToInt32(buf02[1]);
                Console.WriteLine("{0} threads finish...", (threadCount+1));

                threadCount++;
            } while (threadCount < threadAmount);
            //---依序兩兩建立子執行序，並等待執行完畢
        }
        public static void Thread_MT_C(object arg=null)
        {
            int index = Convert.ToInt32(arg);
            int sum, data;
            sum = 0;
            for(int i=0;i< ALCInupt[index].Count;i++)
            {
                data = Convert.ToInt32(ALCInupt[index][i].ToString());
                sum += data;
                ALCOutput[index].Add(data + ";" + sum);
                Thread.Sleep(100);
            }
        }
        public static void Pause()
        {
            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
        }
        public static void Wait()
        {
            Console.Clear();
            Console.Write("Wait: ");
            Thread.Sleep(500);

            Console.Clear();
            Console.Write("Wait: \\");
            Thread.Sleep(500);

            Console.Clear();
            Console.Write("Wait: |");
            Thread.Sleep(500);

            Console.Clear();
            Console.Write("Wait: /");
            Thread.Sleep(500);
            Console.Clear();
        }
        static void Main(string[] args)
        {
            intAns = 0;
            Thread th_M = new Thread(Thread_MT_M);
            th_M.Start();
            do
            {
                Wait();
            }while (th_M.IsAlive);
            Console.Clear();
            Console.WriteLine("1+2+3+...+100={0}", intAns);

            Pause();
        }
    }
}
