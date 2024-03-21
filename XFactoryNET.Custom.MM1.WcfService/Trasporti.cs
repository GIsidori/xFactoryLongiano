using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;

namespace XFactoryNET.Custom.MM1.WcfService
{
    public class Trasporti
    {
        public static Trasporti[] threadTrasps;
        private static Thread[] threads;

        public static void StartThread()
        {
            threads = new Thread[2];
            threadTrasps = new Trasporti[2];


            threadTrasps[0] = new Trasporti("CPUTrasp1",8,9);
            ThreadStart myThreadStart = new ThreadStart(threadTrasps[0].Run);
            threads[0] = new Thread(myThreadStart);
            threads[0].Start();


            threadTrasps[1] = new Trasporti("CPUTrasp2", 28, 29);
            myThreadStart = new ThreadStart(threadTrasps[1].Run);
            threads[1] = new Thread(myThreadStart);
            threads[1].Start();

        }

        public static void StopThread()
        {
            if (threads[0] != null)
            {
                threads[0].Abort();
                threads[0].Join();
            }

            if (threads[1] != null)
            {
                threads[1].Abort();
                threads[1].Join();
            }

        }

        private string device;
        private int sendDB;
        private int recDB;

        public Trasporti(string device,int SendDB,int RecDB)
        {
            this.device = device;
            this.sendDB = SendDB;
            this.recDB = RecDB;
        }

        public void Run()
        {


            try
            {
                lock (this)
                {
                    while (true)
                    {
                        Monitor.Wait(this, 500);
                    }
                }
            }
            catch (ThreadAbortException)
            {
                Thread.ResetAbort();
            }
            finally
            {

            }

        }

    }

}