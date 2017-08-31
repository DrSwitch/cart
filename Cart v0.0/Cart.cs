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
        List<entity.Header> headers = new List<entity.Header>();
        List<entity.Data> data = new List<entity.Data>();
        List<string> resuts = new List<string>();

        public Cart(List<entity.Header> headers, List<entity.Data> data, List<string> resuts)
        {
            this.headers = headers;
            this.data = data;
            this.resuts = resuts;
            CreateTree();
        }

        public void CreateTree() {
            List<string> dataInHeader = new List<string>();
            List<string> distinctResult = SelectDistinctInColumn(resuts);
            entity.MaxDifference maxdiff = new entity.MaxDifference(null, 0, 0, 0, "", "");
            for (int i = 0; i < headers.Count-1; i++) {
                dataInHeader = new List<string>();
                dataInHeader = data[i].DataInColumn();

                List<string> distinctData = SelectDistinctInColumn(dataInHeader);
                List<int> hash = new List<int>();
                string mess=headers[i].GetNameHeader()+"\n";
                
                for (int k = 0; k < distinctData.Count; k++) {
                    int count2 = 0;
                    
                    int diff = 0;
                    for (int j = 0; j < distinctResult.Count; j++) {
                        int count = 0;
                        for (int n = 0; n < dataInHeader.Count; n++) {
                            if ((distinctResult[j] == resuts[n])&&(distinctData[k] == dataInHeader[n])) {
                                count++;
                            }
                        }
                        mess += "Количество " + distinctResult[j] + " в " + distinctData[k] +" = "+ count + "\n";
                        if (j == 0) {
                            count2 = count;
                            
                        }
                        if (j == 1) {
                            diff = Math.Abs(count - count2);
                            if (maxdiff.GetDifference() < diff) maxdiff = new entity.MaxDifference(headers[i], diff, count, count2, distinctResult[0], distinctResult[1]); 
                            mess += "Разница "+ count + " и "+count2+" = "+ diff + "\n";
                        }
                    }
                    
                }
                MessageBox.Show(mess+"\nМакс. разница ="+maxdiff.GetDifference());
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
