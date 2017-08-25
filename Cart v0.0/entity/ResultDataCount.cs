using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart_v0._0.entity
{
    class ResultDataCount
    {
        string result;
        List<int> count = new List<int>();

        public ResultDataCount(string result, List<int> count) {
            this.result = result;
            this.count = count;
        }

    }
}
