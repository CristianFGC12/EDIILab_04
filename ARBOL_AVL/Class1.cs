using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARBOL_AVL
{
    public class AVLTree<T>
    {
        private class Nodo
        {
            public T value { get; set; }
            public int height { get; set; }
            public Nodo left { get; set; }
            public Nodo right { get; set; }
            public Nodo(T _value)
            {
                value = _value;
                height = 1;
            }
        }
        private Nodo root;
        public delegate bool compareFuncion(T value, string boolOperator, T value2);
        public List<T> getAll()
        {
            List<T> newList = new List<T>();
            getInOrder(root, newList);
            return newList;
        }
        private void getInOrder(Nodo node, List<T> allData)
        {
            if (node != null)
            {
                getInOrder(node.left, allData);
                allData.Add(node.value);
                getInOrder(node.right, allData);
            }
        }
        public void insert(T value, compareFuncion compare)
        {
            root = insertNode(root, value, compare);
        }
        private int max(int a, int b)
        {
            return (a > b) ? a : b;
        }
        private int height(Nodo node)
        {
            if (node == null)
            {
                return 0;
            }
            return node.height;
        }
        private int getBalanceFactor(Nodo node)
        {
            if (node == null)
            {
                return 0;
            }
            return height(node.left) - height(node.right);
        }
        private Nodo rotateRight(Nodo y)
        {
            Nodo x = y.left;
            Nodo T2 = x.right;
            x.right = y;
            y.left = T2;
            y.height = max(height(y.left), height(y.right)) + 1;
            x.height = max(height(x.left), height(x.right)) + 1;

            return x;
        }
        private Nodo rotateLeft(Nodo x)
        {
            Nodo y = x.right;
            Nodo T2 = y.left;
            y.left = x;
            x.right = T2;
            x.height = max(height(x.left), height(x.right)) + 1;
            y.height = max(height(y.left), height(y.right)) + 1;

            return y;
        }
        private Nodo insertNode(Nodo node, T value, compareFuncion compare)
        {
            if (node == null)
            {
                return new Nodo(value);
            }

            if (compare(value, "<", node.value))
            {
                node.left = insertNode(node.left, value, compare);
            }
            else if (compare(value, ">", node.value))
            {
                node.right = insertNode(node.right, value, compare);
            }
            else
            {
                return node;
            }

            node.height = max(height(node.left), height(node.right)) + 1;
            int balanceFactor = getBalanceFactor(node);
            if (balanceFactor > 1)
            {
                if (compare(value, "<", node.left.value))
                {
                    return rotateRight(node);
                }
                else if (compare(value, ">", node.left.value))
                {
                    node.left = rotateLeft(node.left);
                    return rotateRight(node);
                }
            }
            if (balanceFactor < -1)
            {
                if (compare(value, ">", node.right.value))
                {
                    return rotateLeft(node);
                }
                else if (compare(value, "<", node.right.value))
                {
                    node.right = rotateRight(node.right);
                    return rotateLeft(node);
                }
            }

            return node;
        }

        private int depth(Nodo root)
        {
            if (root == null)
            {
                return 0;
            }

            if (root.left == null && root.right == null)
            {
                return 1;
            }

            return 1 + Math.Max(depth(root.left), depth(root.right));
        }

        public int getDepth()
        {
            return depth(root);
        }

        private T Busqueda(T value, compareFuncion compare, Nodo raiz)
        {
            if (raiz == null)
            {
                return default(T);
            }

            if (compare(value, "==", raiz.value))
            {
                return raiz.value;
            }

            if (compare(value, "<", raiz.value))
            {
                return Busqueda(value, compare, raiz.left);
            }

            if (compare(value, ">", raiz.value))
            {
                return Busqueda(value, compare, raiz.right);
            }

            return default(T);
        }

        public T Search(T value, compareFuncion compare)
        {
            return Busqueda(value, compare, root);
        }

        //Sort
        public void Sort(compareFuncion compare)
        {
            List<T> datosarbol = getAll();
            root = null;
            foreach (T value in datosarbol)
            {
                insert(value, compare);
            }
        }

        private Nodo BusquedaNodo(T value, compareFuncion compare, Nodo raiz)
        {
            if (raiz == null)
            {
                return null;
            }

            if (compare(value, "==", raiz.value))
            {
                return raiz;
            }

            if (compare(value, "<", raiz.value))
            {
                return BusquedaNodo(value, compare, raiz.left);
            }

            if (compare(value, ">", raiz.value))
            {
                return BusquedaNodo(value, compare, raiz.right);
            }

            return null;
        }
        private Nodo Replace(T value, T nuw, compareFuncion compare, Nodo raiz)
        {
            Nodo Cambio = BusquedaNodo(value, compare, root);
            if (Cambio == null)
            {
                return null;
            }
            Cambio.value = nuw;
            return Cambio;
        }
        public void Reemplazar(T value, T nuw, compareFuncion compare)
        {
            Replace(value, nuw, compare, root);
        }
    }
}
