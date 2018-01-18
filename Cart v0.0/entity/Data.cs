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

        //вытаскиваем данные соответствующие стобцу
        public List<string> DataInColumn()
        {
            return this.data;
        }

        // Возвращает лист индексов данных, которые меньше полученного значения
        public List<int> DataInColumnUnder(string N)
        {
            List<int> hash = new List<int>();
            for (int i = 0; i < data.Count; i++) {
                if (String.Compare(N, data[i]) > 0)
                    hash.Add(i);
            }
            return hash;
        }

        //вытаскиваем данные соответствующие стобцу, если он больше полученного значения
        public List<int> DataInColumnOver(string N)
        {
            List<int> hash = new List<int>();
            for (int i = 0; i < data.Count; i++)
            {
                if (String.Compare(N, data[i]) <= 0)
                    hash.Add(i);
            }
            return hash;
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
