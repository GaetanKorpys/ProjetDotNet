using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetDotNet.Configuration
{
    public static class Configuration
    {

        public static string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=PoleInvestigation;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public static int numInvestigation = 1;
    }
}
