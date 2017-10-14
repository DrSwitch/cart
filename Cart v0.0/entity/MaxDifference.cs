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
        List<string> datainheader;
        int difference=0;
        int leftcount;
        int rightcount;
        string leftResult;
        string rightResult;
        string data;
        
        public MaxDifference(Header header, List<string> datainheader, int difference, int leftcount, int rightcount, string leftResult, string rightResult, string data)
        {
            this.header = header;
            this.datainheader = datainheader;
            this.difference = difference;
            this.leftcount = leftcount;
            this.rightcount = rightcount;
            this.leftResult = leftResult;
            this.rightResult = rightResult;
            this.data = data;
        }

        public int GetDifference() {
            return this.difference;
        }

        public Header GetHeader() {
            return this.header;
        }

        public string GetLeftResult() {
            return this.leftResult;
        }

        public string GetRightResult()
        {
            return this.rightResult;
        }

        public List<string> GetListDataInHeader() {
            return this.datainheader;
        }

        public void SetDifference(int difference){
            this.difference = difference;
        }
        
        public string ToString() {

            string datalist = "{";
            for (int i = 0; i < datainheader.Count; i++) {
                datalist += datainheader[i] + ",";
            }

            return header.GetNameHeader() + datalist +"} = " + data + "\n" +
                "Результаты = \n" +
                leftResult+" = "+leftcount + "\n" +
                rightResult+" = "+rightcount + "\n" +
                "разница=" + difference + "\n";
        }
    }
}
