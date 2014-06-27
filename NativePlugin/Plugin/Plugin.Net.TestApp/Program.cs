using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plugin.Net.TestApp
{
    class Program
    {
        static void Main( string[] args )
        {
            Console.WriteLine( PluginNet.Plugin.PluginAdd( 1, 2 ) );
        }
    }
}
