using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Demo.Model;

namespace WPF_Demo.Context
{
    public class dbContext
    {
        public static dbDemoEntities db = new dbDemoEntities();
    }
}
