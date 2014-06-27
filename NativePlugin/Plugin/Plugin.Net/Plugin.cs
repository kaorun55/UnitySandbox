using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace PluginNet
{
    public class Plugin
    {
        [DllImport( "Plugin" )]
        public static extern int PluginAdd( int a, int b );
    }
}
