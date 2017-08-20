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
        List<entity.Headers> headers = new List<entity.Headers>();
        List<entity.Data> data = new List<entity.Data>();
        List<entity.Results> resuts = new List<entity.Results>();

        public Cart(List<entity.Headers> headers, List<entity.Data> data, List<entity.Results> resuts)
        {
            this.headers = headers;
            this.data = data;
            this.resuts = resuts;
        }

        public void CreateTree() {
            List<string> dataInHeader = new List<string>();
            for (int i = 0; i < headers.Count; i++) {
                dataInHeader = new List<string>();
                int maxDifference;
                int headerMaxDifference;
            }
        }

    }
}
