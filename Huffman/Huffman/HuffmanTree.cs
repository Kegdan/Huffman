using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Huffman
{// дерево
    class HuffmanTree
    {
        public static HuffmanTree Root;
        public HuffmanTree LeftBranch;
        public HuffmanTree RightBranch;
        private HuffmanTree _parent;
        public byte Value;
        private int _weight;
        private static HuffmanTree[] _nodes;

        // построение
        public static void CreateHuffmanTree(int[] byteMap)
        {
            var nodes = new List<HuffmanTree>(256); 
            for (int i = 0; i < 256; i++)
                if (byteMap[i]>0)
                    nodes.Add(new HuffmanTree { Value = (byte)i, _weight = byteMap[i]}); // добавляем все узлы
            _nodes = nodes.ToArray();

            while (nodes.Count>1)
            {
                var first = nodes[0];
                var index = 0;
                for (int i = 1; i < nodes.Count; i++)
                {
                    if (first._weight <= nodes[i]._weight) continue;
                    first = nodes[i];
                    index = i;
                }
                nodes.RemoveAt(index);
                var second = nodes[0];
                index = 0;
                for (int i = 1; i < nodes.Count; i++)
                {
                    if (second._weight <= nodes[i]._weight) continue;
                    second = nodes[i];
                    index = i;
                } // ищем 2 самых легких
                nodes.RemoveAt(index);
                var newNode = new HuffmanTree { LeftBranch = first, RightBranch = second, _weight = first._weight + second._weight };
                first._parent = newNode;
                second._parent = newNode;
                nodes.Add(newNode);
                // заменяем 2 самых легких на на один с весом = сумме
            }
            Root = nodes[0];
            // в итоге - дерево, корень в руте
        }

        private static void ShowTree(RichTextBox outputTextBox, int i, HuffmanTree node)
        {
            // краски в рисовании дерева
            if (node.LeftBranch == null)
            {
               
                outputTextBox.AppendText(" Value: " + node.Value+" Count: " + node._weight+"\n");

            }
            else
            {
                outputTextBox.AppendText("\n");
                for (int j = 0; j < i; j++)
                    outputTextBox.AppendText("\t");
                outputTextBox.AppendText("(" + (i + 1) + ")");
                ShowTree(outputTextBox, i + 1, node.LeftBranch);
                outputTextBox.AppendText("\n");
                for (int j = 0; j < i; j++)
                    outputTextBox.AppendText("\t");
                outputTextBox.AppendText("(" + (i+1) + ")");
                ShowTree(outputTextBox, i + 1, node.RightBranch);
            }
        }

        public static void ShowTree(RichTextBox outputTextBox)
        {
            ShowTree(outputTextBox,0,Root);
        }

        public static Dictionary<byte, BitArray> GetMap()
        {// получаем карту для кодирования
            var output = new Dictionary<byte, BitArray>(_nodes.Length);
            var sb = new StringBuilder();
            foreach (HuffmanTree t in _nodes) // для каждого узла стоим код
            {
                var huffmanTree = t;
                sb.Clear();
            
                while (huffmanTree._parent!=null)
                {
                    sb.Append(huffmanTree == huffmanTree._parent.LeftBranch ? "0" : "1"); // если ветвь левая - добавляем 0, если правая -1
                    huffmanTree = huffmanTree._parent;
                }
                var bitArray = new BitArray(sb.Length);
                for (int j = 0; j < sb.Length; j++)
                {
                    bitArray[j] = sb[j] - '0' == 1; // собираем в битареи и добавляем в карту
                }
                output.Add(t.Value,bitArray);
            }

            return output;
        }
    }
}
