using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart_v0._0.entity
{
    class MaxDifference
    {
        Header header;
        int difference=0;
        int leftcount;
        int rightcount;
        string leftResult;
        string rightResult;

        public MaxDifference(Header header, int difference, int leftcount, int rightcount, string leftResult, string rightResult)
        {
            this.header = header;
            this.difference = difference;
            this.leftcount = leftcount;
            this.rightcount = rightcount;
            this.leftResult = leftResult;
            this.rightResult = rightResult;
        }

        public int GetDifference() {
            return this.difference;
        }

        public void SetDifference(int difference){
            this.difference = difference;
        }

        public string ToString() {

            return "";
        }
    }
}
