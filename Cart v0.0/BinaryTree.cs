using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart_v0._0
{
    class BinaryTree
    {
        public class BinaryNode //узел дерева
        {
            public BinaryNode left { get; set; } //указатели узла
            public BinaryNode right { get; set; }
            public int value; //вставляемое значение

            public BinaryNode(int val)
            {
                value = val; //конструктор заполняет узел значением
                left = null;
                right = null;
            }
        }

        public BinaryNode root; //корень дерева
        public BinaryTree() //конструктор (по умолчанию) создания дерева
        {
            root = null; //при создании корень не определен
        }

        public BinaryTree(int value)
        {
            root = new BinaryNode(value); //если изначально задаём корневое значение
        }

        //нерекурсивное добавление
        public void Add(int value) //узел и его значение
        {
            if (root == null)  //если корня нет
            {
                root = new BinaryNode(value); //добавляем элемент как корневой
                return;
            }

            BinaryNode current = root; //текущий равен корневому
            bool added = false;
            //обходим дерево
            do {
                if (value >= current.value)  //идём вправо
                {
                    if (current.right == null) {
                        current.right = new BinaryNode(value);
                        added = true;
                    } else {
                        current = current.right;
                    }
                }
                if (value < current.value) //идём влево
                {
                    if (current.left == null) {
                        current.left = new BinaryNode(value);
                        added = true;
                    }
                    else  {
                        current = current.left;
                    }
                } else {
                    current = current.left;
                }
            }
            while (!added);
        }
    }
}
