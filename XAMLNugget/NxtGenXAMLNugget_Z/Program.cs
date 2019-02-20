using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NxtGenXAMLNugget
{
    class Program
    {
        static void Main(string[] args)
        {
            Helper.RunTests(delegate(String s) { Console.WriteLine(s); }, delegate() { Console.ReadLine(); });
       }
    }
}