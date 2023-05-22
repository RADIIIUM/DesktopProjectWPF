using DesktopProject_V3.DataBaseClass;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopProject_V3
{
    partial class Initial
    {
        public static List<Products> Cart { get; set; }
        public static string login { get; set; }

        public static string OldLogin { get; set; }

        public static bool ShowProfile { get; set; } 

        public static int NumberOfNews {get;set;}

        public static string NameOfCategoryOfProduct { get; set; }

        public static int IDProduct { get;set; }

        public static int NumberOfOrder { get; set; }


    }
}
