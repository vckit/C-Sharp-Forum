using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf_1.Model;

namespace wpf_1.Context
{
    public class dbContext
    {
        // dbCourseEntities - это экземпляр нашей базы данных.
        // Копия нашей базы, сняв копию, мы можем обращаться на его внутренние объекты
        
        public static dbCourseEntities db = new dbCourseEntities();
    }
}
