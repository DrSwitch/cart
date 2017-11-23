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
        entity.MaxDifference maxdiff = new entity.MaxDifference(null, null, 0, 0, 0, "", "", "");

        entity.LevelXY lvlXY = new entity.LevelXY(0,"",null,"");

        public Cart(List<entity.Header> headers, List<entity.Data> data, List<string> resuts, entity.LevelXY lvlXY)
        {
            this.headers = headers;
            this.data = data;
            this.resuts = resuts;
            this.lvlXY = lvlXY;
            List<string> distinctResult = SelectDistinctInColumn(resuts);

            if (distinctResult.Count == 1) {
                MessageBox.Show("Конечный узел" +
                    "\nЗаголовок = " + lvlXY.header.GetNameHeader() + " там где " + lvlXY.result +
                    "\nКоординаты:" +
                    "\nlevel = " + lvlXY.level +
                    "\nWay = "+lvlXY.way +
                    "\nРезультат = "+distinctResult[0]);
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
                                if (maxdiff.GetDifference() < diff) maxdiff = new entity.MaxDifference(headers[i], dataInHeader, diff, count2, count, distinctResult[0], distinctResult[1], distinctData[k]);
                                mess += "Разница " + count + " и " + count2 + " = " + diff + "\n";
                            }
                        }
                    }
                }
               // MessageBox.Show(mess);
            }

            MessageBox.Show("Разделяющийся узел\n" + 
                maxdiff.ToString());

            ///начинается создание ветвей, тут создаются списки данных и результатов, 
            ///которые будут отправлены глубже
            List<entity.Data> dataLR = new List<entity.Data>();
            List<string> resutsLR = new List<string>();
            List<string> distData = SelectDistinctInColumn(maxdiff.GetListDataInHeader());

            //данные для левой ветки 
            for (int i = 0; i < data.Count; i++)
            {
                List<string> hash = new List<string>();
                for (int k = 0; k < data[i].DataInColumn().Count; k++){
                    if (maxdiff.GetListDataInHeader()[k] == distData[0]) {
                        hash.Add(data[i].DataInColumn()[k]);
                        if (i == 0) {
                            resutsLR.Add(resuts[k]);
                        }
                    }
                }
                dataLR.Add(new entity.Data(data[i].GetHeader(), hash));
            }

            // string str = "Результаты = {";
            // for (int i = 0; i < resutsLR.Count; i++)
            // {
            //     str += resutsLR[i];
            // }
            // str += "}";
            // MessageBox.Show(maxdiff.ToString()+"\nLeft vetka"+str);
                
            //левая ветка
            Cart left = new Cart(headers, dataLR, resutsLR, new entity.LevelXY(lvlXY.level+1, lvlXY.way + " left", maxdiff.GetHeader(), maxdiff.GetLeftResult()));

            dataLR = new List<entity.Data>();
            resutsLR = new List<string>();
            //данные для правой ветки
            for (int i = 0; i < data.Count; i++)
            {
                List<string> hash = new List<string>();
                for (int k = 0; k < data[i].DataInColumn().Count; k++)
                {
                    if (maxdiff.GetListDataInHeader()[k] == distData[1])
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

            //string str = "Результаты = {";
            //for (int i = 0; i < resutsLR.Count; i++)
            //{
            //    str += resutsLR[i];
            //}
            //str += "}";
            //MessageBox.Show(maxdiff.ToString() + "\nRight vetka" + str);

            // правая ветка
            Cart right = new Cart(headers, dataLR, resutsLR, new entity.LevelXY(lvlXY.level + 1, lvlXY.way + " right", maxdiff.GetHeader(), maxdiff.GetRightResult()));

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
