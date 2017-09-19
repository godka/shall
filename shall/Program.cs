using System;

namespace shall
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //simple test for bp
            mythFileReader fr = new mythFileReader("out.txt");
            var source = fr.StartLoop();
            mythBPNetworkWorker bw = new mythBPNetworkWorker(source);
            bw.AddHiddenLayers(3, 128);
            bw.StartLoop(10000);
            Console.ReadKey();
        }
    }
}
