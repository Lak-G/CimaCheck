using System;
using System.Collections.Generic;
using System.Text;
using CimaCheck.Controllers;

namespace CimaCheck
{
    class AppSettings
    {
        public required SupabaseSettings Supabase { get; set; }
        public required string AppName { get; set; }
        public required string Version { get; set; }
    }
}
