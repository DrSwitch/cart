using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace Cart_v0._0
{
    class ClfTree
    {
        List<entity.Header> headers = new List<entity.Header>();
        List<entity.Data> data = new List<entity.Data>();
        List<string> resuts = new List<string>();

        // данные для левой ветки
        List<entity.Data> leftdata = new List<entity.Data>();
        List<string> leftresuts = new List<string>();

        // данные для правой ветки
        List<entity.Data> rightdata = new List<entity.Data>();
        List<string> rightresuts = new List<string>();

        // Для рисования дерева и вывода на форму
        Grid gridWithRows = new Grid();
        Grid gridWithColumns = new Grid();
        RowDefinition rowDefinition0 = new RowDefinition();
        RowDefinition rowDefinition1 = new RowDefinition();
        ColumnDefinition columnDefinition0 = new ColumnDefinition();
        ColumnDefinition columnDefinition1 = new ColumnDefinition();


        public ClfTree(List<entity.Header> headers, List<entity.Data> data, List<string> resuts)
        {
            this.headers = headers;
            this.data = data;
            this.resuts = resuts;

            gridWithRows.RowDefinitions.Add(rowDefinition0);
            gridWithRows.RowDefinitions.Add(rowDefinition1);
            gridWithColumns.ColumnDefinitions.Add(columnDefinition0);
            gridWithColumns.ColumnDefinitions.Add(columnDefinition1);

            gridWithColumns.SetValue(Grid.RowProperty, 1);
            Thickness thickness = new Thickness(0, 0, 0, 0);
            gridWithRows.Margin = thickness;

            IEnumerable<string> disresult = resuts.Distinct();
            int k = 0;
            foreach (string dsr in disresult) {
                k++;
            }
            if (k <= 1)
            {
                gridWithRows.Children.Add(DrawNode("Результат = " + resuts[0]));
            }
            else
            {
                CreateTree();
            }
        }

        // возвращает всё дерево что ниже этой ветки
        public Grid GetGridWithRows()
        {
            return gridWithRows;
        }

        // строим дерево 
        private void CreateTree() {

            // это пара данных по которым будет делиться дерево (содержит заголовок и значение)
            entityClf.Para minPara = new entityClf.Para();

            string str = "";
            double Q, L, R;
            float G, pastG = 23232323232323; 

            // Проверить какие пары в G проходят проверку
            // цикл по заголовкам (Header)  кроме последнего, последний это заголовок результатов
            for (int index = 0; index < headers.Count-1; index++)
            {
                entity.Header header = headers[index]; 
                //str += header.GetNameHeader() + " = {";
                // цикл по данным в заголовке
                IEnumerable<string> data = this.data[index].DataInColumn().Distinct();
                {
                    foreach (string dat in data)
                    {
                        Left(index, dat);
                        Right(index, dat);
                        IEnumerable<string> disresult = leftresuts.Distinct();
                        int flag = 0;
                        int k = 0;
                        foreach (string dsr in disresult)
                        {
                            k++;
                        }
                        if (k <= 1) flag = 1;
                        k = 0;
                        disresult = rightresuts.Distinct();
                        foreach (string dsr in disresult)
                        {
                            k++;
                        }
                        if (k <= 1) flag = 1;

                        // если левый или правый результат содержит только 1 значение, то выйти из цикла
                        if (flag != 1)
                        {
                            G = ((leftresuts.Count / resuts.Count) * H(leftresuts))
                                + ((rightresuts.Count / resuts.Count) * H(rightresuts));
                            if (pastG > G)
                            {
                                pastG = G;
                                minPara.SetIdHeader(index);
                                minPara.SetZnach(dat);
                            }
                        }
                        //str += dat + " ";
                    }
                }
                //str += "}\n";
            }

            //MessageBox.Show("минимум там где G("+ minPara.GetIdHeader()+", "+minPara.GetZnach()+")");

            // записываем результат в грид, что бы было что возвращать
            gridWithRows.Children.Add(DrawNode("Где "+ headers[minPara.GetIdHeader()].GetNameHeader() + " > "+ minPara.GetZnach()));

            // записываем в левый и правый данные
            Left(minPara.GetIdHeader(), minPara.GetZnach());
            Right(minPara.GetIdHeader(), minPara.GetZnach());

            // создаём левую ветку
            ClfTree clfTreeLeft = new ClfTree(headers, leftdata, leftresuts);
            clfTreeLeft.GetGridWithRows().SetValue(Grid.ColumnProperty, 0);
            gridWithColumns.Children.Add(clfTreeLeft.GetGridWithRows());

            // правая ветка
            ClfTree clfTreeRight = new ClfTree(headers, rightdata, rightresuts);
            clfTreeRight.GetGridWithRows().SetValue(Grid.ColumnProperty, 1);
            gridWithColumns.Children.Add(clfTreeRight.GetGridWithRows());
        }

        private float H(List<string> results) {
            IEnumerable<string> disresult = results.Distinct();
            int k = 0;
            float h = 0;
            foreach (string disres in disresult) {
                int kol=0;
                for (int i = 0; i < results.Count; i++) {
                    if(results[i] == disres)
                        kol++;
                }
                h += (kol / results.Count) * (1 - (kol / results.Count));
                k++;
            }
            return h;
        }

        // записываем данные для левой ветки (<)
        private void Left(int idHeader, string znach) {

            leftdata = new List<entity.Data>();
            leftresuts = new List<string>();

            List<int> indexes = data[idHeader].DataInColumnOver(znach);
            List<string> hash = new List<string>();

            // цикл по заголовкам
            for (int i = 0; i < data.Count; i++) {
                hash = new List<string>();
                for (int k=0; k<indexes.Count; k++) {
                    if (i == 0)  {
                        leftresuts.Add(resuts[indexes[k]]);
                    }
                    hash.Add(data[i].DataInColumn()[indexes[k]]);
                }
                leftdata.Add(new entity.Data(headers[i],hash));
            }

        }

        // записываем данные для правой ветки (>=)
        private void Right(int idHeader, string znach) {
            rightdata = new List<entity.Data>();
            rightresuts = new List<string>();

            List<int> indexes = data[idHeader].DataInColumnUnder(znach);
            List<string> hash = new List<string>();

            // цикл по заголовкам
            for (int i = 0; i < data.Count; i++) {
                hash = new List<string>();
                for (int k = 0; k < indexes.Count; k++)
                {
                    if (i == 0) {
                        rightresuts.Add(resuts[indexes[k]]);
                    }
                    hash.Add(data[i].DataInColumn()[indexes[k]]);
                }
                rightdata.Add(new entity.Data(headers[i], hash));
            }
        }

        // рисуем label
        private System.Windows.Controls.Label DrawNode(string text)
        {
            System.Windows.Controls.Label lbl = new System.Windows.Controls.Label();
            lbl.Name = "lbl";
            lbl.Content = text;
            lbl.ToolTip = text;
            lbl.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            Thickness thickness = new Thickness(2, 2, 2, 2);
            lbl.Margin = thickness;
            lbl.SetValue(Grid.RowProperty, 0);
            lbl.Background = Brushes.Tomato;

            return lbl;
        }
    }
}
