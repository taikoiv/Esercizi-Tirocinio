using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpperCaser
{
    class Program
    {
        static void Main(string[] args)
        {
            Producer p = new Producer();
            Listener l = new Listener();
            l.listen(p);
        }
    }
}
