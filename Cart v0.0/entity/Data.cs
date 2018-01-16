using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace Cart_v0._0.entity
{
    class Data
    {
        Header header;
        List<string> data;

        public Data(Header header, List<string> data) {
            this.header = header;
            this.data = data;
        }

        public void RemoveAtData(int N) {
            data.RemoveAt(N);
        }

        //вытаскиваем данные соответствующие стобцу с индексом indexheader
        public List<string> DataInColumn()
        {
            return this.data;
        }

        //вытаскиваем данные соответствующие строке
        public string DataInRow(int indexRow)
        {
            return data[indexRow];
        }

        public Header GetHeader() {
            return this.header;
        }

        public string ToString ()  {
            string str = "{";
            for (int i = 0; i < data.Count(); i++) {
                str += data[i];
                if (i + 1 != data.Count) {
                    str += ", ";
                }
            }
            return header.GetNameHeader() 
                + " = " 
                + str + "}";
        }
    }
}
