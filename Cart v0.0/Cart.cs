using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart_v0._0
{
    class Cart
    {
        public static string Asdasd { get; set; }
        private static List<string> headers = new List<string>();
        private static List<List<string>> data = new List<List<string>>();
        private static List<string> resuts = new List<string>();

        public Cart(List<string> headers, List<List<string>> data, List<string> resuts)
        {
            Cart.headers = headers;
            Cart.data = data;
            Cart.resuts = resuts;

        }

    }
}
