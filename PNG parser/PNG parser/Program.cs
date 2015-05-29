using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNG_parser
{
    class Program
    {
        static void Main(string[] args)
        {
            PNGParser parser = new PNGParser("C:\\Users\\Sean\\Pictures\\8008.PNG");
            Console.ReadLine();
        }
    }
}
