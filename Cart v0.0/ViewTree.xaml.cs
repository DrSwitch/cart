using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cart_v0._0
{
    /// <summary>
    /// Логика взаимодействия для ViewTree.xaml
    /// </summary>
    public partial class ViewTree : Window
    {
        private List<entity.Node> nodes = new List<entity.Node>();

        public ViewTree()
        {
            InitializeComponent();
            DrawTree();
        }

        public void DrawTree() {

            Cart cart = new Cart(entity.Tree.headers,
                entity.Tree.data,
                entity.Tree.resuts,
                new entity.Node("S", null, ""));

            nodes = cart.GetNodes();
            entity.Tree.Nodes = nodes;

            nodes = entity.Tree.Nodes;
            StackPanel myStackPanel = new StackPanel();
            myStackPanel.Children.Add(cart.GetGridWithRows());

            this.Content = myStackPanel;
  
        }

        private void ShowNodes(){

            string str = "";
            for (int i = 0; i<nodes.Count; i++)
            {
                string pr = "";
                if (nodes[i].way.Length > 0)
                {
                    for (int j = 0; j<nodes[i].way.Length; j++)
                    {
                        pr += "  ";
                    }
                }
                    str += pr + nodes[i].header.GetNameHeader() + " = " + nodes[i].result + " way = " + nodes[i].way
                    + "\nРешение = " + nodes[i].decision
                    + "\n";
            }

            str += "Высота = " + entity.Tree.GetHeight();
            System.Windows.MessageBox.Show(str);
        }
    }
}
