using System;
using System.Collections.Generic;
using System.Text;

namespace ComicDatabaseProject
{
    class ComicBookQueries
    {
        //SELECT cb.title, cb.issue,cv.orginalPrice,cv.currentValue
        public string title { get; set; }
        public int issue { get; set; }
        public decimal orginalPrice { get; set; }
        public string publisher { get; set; }
        public string detail { get; set; }
        public decimal currentValue { get; set; }
        public string cbcondtition { get; set; }
        public decimal totalValue { get; set; }


    }

}
