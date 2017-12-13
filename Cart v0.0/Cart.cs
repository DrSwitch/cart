using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace Cart_v0._0
{
    class Cart
    {
        List<entity.Header> headers = new List<entity.Header>();
        List<entity.Data> data = new List<entity.Data>();
        List<string> resuts = new List<string>();
        entity.NextNode nextNode = new entity.NextNode(null, null, 0, 0, 0, "", "", "");

        List<entity.Node> nodes = new List<entity.Node>();

        entity.Node node = new entity.Node(null,null,"");

        StackPanel myStackPanel = new StackPanel();
        StackPanel hashSP = new StackPanel();

        public Cart(List<entity.Header> headers, List<entity.Data> data, List<string> resuts, entity.Node node)
        {
            this.headers = headers;
            this.data = data;
            this.resuts = resuts;
            this.node = node;
            List<string> distinctResult = SelectDistinctInColumn(resuts);

            Thickness thickness = new Thickness(5,5,5,5);
            myStackPanel.Margin = thickness;
            hashSP.Orientation = System.Windows.Controls.Orientation.Horizontal;
            hashSP.Margin = thickness;
            hashSP.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

            if (distinctResult.Count == 1) {
                //MessageBox.Show("Конечный узел" +
                //    "\n" + node.header.GetNameHeader() + " = " + node.result +
                //    "\nКоординаты:" +
                //    "\nWay = "+node.way +
                //    "\nРезультат = "+distinctResult[0]);
                node.decision = distinctResult[0];
                nodes.Add(node);
                myStackPanel.Children.Add(DrawNode(node.header.GetNameHeader()
                    + " = " + node.result
                    + "\nРешение = " + node.decision));
            }
            else
            {
                CreateTree();
            }
        }

        public void CreateTree() {
            List<string> dataInHeader = new List<string>();
            List<string> distinctResult = SelectDistinctInColumn(resuts);


            ///тут мы выясняем по какому из заголовков лучше сделать разделение ветвей
            ///результатом вычислений получим экземпляр класса MaxDifference (столбец, данные в столбце, разница)
            ///это типа максимальная разница результатов в стобце с определёнными данными.
            for (int i = 0; i < headers.Count-1; i++) {
                dataInHeader = new List<string>();
                dataInHeader = data[i].DataInColumn();

                List<string> distinctData = SelectDistinctInColumn(dataInHeader);
                List<int> hash = new List<int>();
                string mess=headers[i].GetNameHeader()+"\n";

                // если данные в колонке с данными разнообразные
                if (distinctData.Count > 1) {
                    for (int k = 0; k < distinctData.Count; k++) {
                        int count2 = 0;

                        int diff = 0;
                        for (int j = 0; j < distinctResult.Count; j++) {
                            //считаем колличество результатов в данных
                            int count = 0;
                            for (int n = 0; n < dataInHeader.Count; n++)
                            {
                                if ((distinctResult[j] == resuts[n]) && (distinctData[k] == dataInHeader[n]))
                                {
                                    count++;
                                }
                            }
                            mess += "Количество " + distinctResult[j] + " в " + distinctData[k] + " = " + count + "\n";
                            if (j == 0) {
                                count2 = count;
                            }
                            if (j == 1) {
                                diff = Math.Abs(count - count2);
                                if (nextNode.GetDifference() < diff) nextNode = new entity.NextNode(headers[i], dataInHeader, diff, count2, count, distinctResult[0], distinctResult[1], distinctData[k]);
                                mess += "Разница " + count + " и " + count2 + " = " + diff + "\n";
                            }
                        }
                    }
                }
               // MessageBox.Show(mess);
            }

            if (node.way == "S")
            {
                //MessageBox.Show("Стартовый узел\n" +
                //    "\nКоординаты:" +
                //    "\nWay = " + node.way +
                //    "\n" + nextNode.GetHeader().GetNameHeader() + " ?");
                node.header = nextNode.GetHeader();
                nodes.Add(node);
            }
            else {
                //MessageBox.Show("Разделяющийся узел\n" +
                //    "\n" + node.header.GetNameHeader() + " = " + node.result +
                //    "\nКоординаты:" +
                //    "\nWay = " + node.way +
                //    "\n" + nextNode.GetHeader().GetNameHeader() + " ?");
                nodes.Add(node);

            }
            

            ///начинается создание ветвей, тут создаются списки данных и результатов, 
            ///которые будут отправлены глубже
            List<entity.Data> dataLR = new List<entity.Data>();
            List<string> resutsLR = new List<string>();
            List<string> distData = SelectDistinctInColumn(nextNode.GetListDataInHeader());


            //данные для левой ветки 
            for (int i = 0; i < data.Count; i++)
            {
                List<string> hash = new List<string>();
                for (int k = 0; k < data[i].DataInColumn().Count; k++){
                    if (nextNode.GetListDataInHeader()[k] == distData[0]) {
                        hash.Add(data[i].DataInColumn()[k]);
                        if (i == 0) {
                            resutsLR.Add(resuts[k]);
                        }
                    }
                }
                dataLR.Add(new entity.Data(data[i].GetHeader(), hash));
            }

            if(node.way  == "S")
                myStackPanel.Children.Add(DrawNode(node.header.GetNameHeader()));
            else
                myStackPanel.Children.Add(DrawNode(node.header.GetNameHeader()
                + " = " + node.result));

            //левая ветка
            Cart left = new Cart(headers, dataLR, resutsLR, new entity.Node(node.way + "L", nextNode.GetHeader(), distData[0]));
            nodes.AddRange(left.GetNodes());
            hashSP.Children.Add(left.GetStackPanel());

            dataLR = new List<entity.Data>();
            resutsLR = new List<string>();
            //данные для правой ветки
            for (int i = 0; i < data.Count; i++)
            {
                List<string> hash = new List<string>();
                for (int k = 0; k < data[i].DataInColumn().Count; k++)
                {
                    if (nextNode.GetListDataInHeader()[k] == distData[1])
                    {
                        hash.Add(data[i].DataInColumn()[k]);
                        if (i == 0)
                        {
                            resutsLR.Add(resuts[k]);
                        }
                    }
                }
                dataLR.Add(new entity.Data(data[i].GetHeader(), hash));
            }

            // правая ветка
            Cart right = new Cart(headers, dataLR, resutsLR, new entity.Node(node.way + "R", nextNode.GetHeader(), distData[1]));
            nodes.AddRange(right.GetNodes());
            hashSP.Children.Add(right.GetStackPanel());

            myStackPanel.Children.Add(hashSP);

        }
        
        private List<string> SelectDistinctInColumn(List<string> dataInColumn) {

            List<string> hash = new List<string>();
            IEnumerable<string> distinctData = dataInColumn.Distinct();
            foreach (string data in distinctData) {
                hash.Add(data);
            }
            return hash;
        }

        public List<entity.Node> GetNodes() {
            return nodes;
        }

        public StackPanel GetStackPanel() {
            return myStackPanel;
        }

        private System.Windows.Controls.Label DrawNode(string text)
        {
            System.Windows.Controls.Label lbl = new System.Windows.Controls.Label();
            lbl.Name = "lbl";
            lbl.Content = text;
            lbl.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            Thickness thickness = new Thickness(5, 5, 5, 5);
            lbl.Margin = thickness;
            lbl.Background = Brushes.AliceBlue;

            return lbl;
        }
    }
}