using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cart_v0._0
{
    class Cart
    {
        private static List<string> headers = new List<string>();

        private static List<List<string>> data = new List<List<string>>();
        private static List<string> resuts = new List<string>();

        public Cart(List<string> headers, List<List<string>> data, List<string> resuts)
        {
            Cart.headers = headers;
            Cart.data = data;
            Cart.resuts = resuts;
        }

        public void CreateTree() {
            List<string> dataInHeader = new List<string>();
            for (int i = 0; i < headers.Count; i++) {
                dataInHeader = new List<string>();
                dataInHeader = DataInHeader(i);

            }
        }

        //вытаскиваем данные соответствующие стобцу с индексом indexheader
        public List<string> DataInHeader(int indexheader) {
            List<string> hash;
            List<string> dataInHeader = new List<string>();
            string mess = headers[indexheader]+":\n";
            for (int i = 0; i < data.Count; i++)
            {
                hash = data[i];
                dataInHeader.Add(hash[indexheader]);
                mess += hash[indexheader]+"; ";
            }
            MessageBox.Show(mess);
            return dataInHeader;
        }

    }
}
