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
        List<string> resuts = new List<string>();

        public Cart(List<entity.Headers> headers, List<entity.Data> data, List<string> resuts)
        {
            this.headers = headers;
            this.data = data;
            this.resuts = resuts;
            CreateTree();
        }

        public void CreateTree() {
            List<string> dataInHeader = new List<string>();
            List<string> distinctResult = SelectDistinctInColumn(resuts);

            for (int i = 0; i < headers.Count-1; i++) {
                dataInHeader = new List<string>();
                int maxDifference=0;
                int headerMaxDifference=0;
                dataInHeader = data[i].DataInColumn();

                List<string> distData = SelectDistinctInColumn(dataInHeader);
                List<entity.ResultDataCount> diff = new List<entity.ResultDataCount>();
                List<int> hash = new List<int>();

                for (int j = 0; j < distinctResult.Count; j++) {
                    diff.Add(new entity.ResultDataCount(distData[j], hash));
                    for (int k = 0; k < dataInHeader.Count; k++) {
                        
                    }

                }
            }
        }

        private List<string> SelectDistinctInColumn(List<string> dataInColumn) {

            List<string> hash = new List<string>();
            IEnumerable<string> distinctData = dataInColumn.Distinct();
            foreach (string data in distinctData) {
                hash.Add(data);
            }
            return hash;
        }

    }
}
