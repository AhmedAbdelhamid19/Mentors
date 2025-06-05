using System;
using System.Diagnostics;

namespace Helper
{
    public static class RecordHelper
    {
        static Stopwatch Stopwatch = new Stopwatch();
        static long BytesPhysicalBefore;
        static long BytesVirtualBefore;
        static long GCCollectionCountGen0Before;
        static long GCCollectionCountGen1Before;
        static long GCCollectionCountGen2Before;


        public static void Start()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            BytesPhysicalBefore = Process.GetCurrentProcess().WorkingSet64;
            BytesVirtualBefore = Process.GetCurrentProcess().VirtualMemorySize64;

            GCCollectionCountGen0Before = GC.CollectionCount(0);
            GCCollectionCountGen1Before = GC.CollectionCount(1);
            GCCollectionCountGen2Before = GC.CollectionCount(2);

            Stopwatch.Restart();
        }

        public static void Stop()
        {
            Stopwatch.Stop();

            long BytesPhysicalAfter = Process.GetCurrentProcess().WorkingSet64;
            long BytesVirtualAfter = Process.GetCurrentProcess().VirtualMemorySize64;

            Console.WriteLine($"{(BytesPhysicalAfter - BytesPhysicalBefore):N} Physical Bytes Used");
            Console.WriteLine($"{(BytesVirtualAfter - BytesVirtualBefore):N} Virtual Bytes Used");

            Console.WriteLine($"Time Ellapsed {Stopwatch.Elapsed} , Total MilliSeconds {Stopwatch.ElapsedMilliseconds}");

            Console.WriteLine($"Gen 0 Counter : {GC.CollectionCount(0) - GCCollectionCountGen0Before}");
            Console.WriteLine($"Gen 1 Counter : {GC.CollectionCount(1) - GCCollectionCountGen1Before}");
            Console.WriteLine($"Gen 2 Counter : {GC.CollectionCount(2) - GCCollectionCountGen2Before}");
        }
    }
}
