using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Huffman
{
    public partial class FHuffman : Form
    {
        public FHuffman()
        {
            InitializeComponent();
        }
        //http://habrahabr.ru/post/144200/
        readonly int[] _byteMap = new int[256];

        private void CodeBtn_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)// кодируем
            {
                FillByteMap(ofd.FileName); // заполняем карту байт (частоту появления байт)

                HuffmanTree.CreateHuffmanTree(_byteMap); // строим дерево
                HuffmanTree.ShowTree(OutputTextBox); // рисуем дерево
                var huffmapMap = HuffmanTree.GetMap(); // получаем карду кодировки

                CodeFile(ofd.FileName, ofd.FileName + "huf", huffmapMap, _byteMap); // кодируем
                OutputTextBox.AppendText("\nФайл успешно зашифрован. Новое имя файла: " + ofd.FileName + "5huf\n");
            }

        }

        private static void CodeFile(string fileName, string newFileName, Dictionary<byte, BitArray> huffmapMap, IEnumerable<int> map)
        {// кодируем тут
            Byte myByte;
            var code = new List<BitArray>();
            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                for (int i = 0; i < fs.Length; i++)
                {
                    myByte = (Byte)fs.ReadByte();
                    code.Add(huffmapMap[myByte]); //собираем закодированное сообщение
                }
            } 
            var count = code.Sum(bitArray => bitArray.Count);


            var additionBits = (byte)((8 - count % 8) % 8); // количесто доп бит (ибо может получиться н байт с хвостиком, хвостик нужно будет добить нулями до байта)
            var bitCount = (count + 7) / 8; // количество байт в закодрованном файле

            var bitarr = new BitArray(count + additionBits, false);
            var add = 0;
            foreach (var t in code)
            {
                for (var j = 0; j < t.Count; j++)
                {
                    bitarr[add + j] = t[t.Count-1-j];
                }
                add += t.Count;//собираем из массива поток
            }
            var bytes = new byte[bitCount];
            bitarr.CopyTo(bytes,0);

            using (var sw = new FileStream(newFileName, FileMode.Create))
            using (var bw = new BinaryWriter(sw))
            {
                foreach (var b in map)
                {
                    bw.Write(b);// сначала пишем в фаил карту баит
                }
                bw.Write(additionBits); // колличество доп бит
                foreach (var b in bytes)
                {
                    bw.Write(b); // и сам код
                }
               
            }

        }

        private void FillByteMap(String fileName)// заполняет карту байт
        {
            Byte myByte;
            for (int i = 0; i < _byteMap.Length; i++)
            {
                _byteMap[i] = 0;
            }
            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                for (int i = 0; i < fs.Length; i++)
                {
                    myByte = (Byte) fs.ReadByte();
                    _byteMap[myByte]++;
                }
            }
            
        }

        private void DecodeBtn_Click(object sender, EventArgs e) // декодируем
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var map = new int[256];
                using (var stream = new FileStream(ofd.FileName, FileMode.Open))
                using (var binaryReader = new BinaryReader(stream))
                {
                    for (int i = 0; i < map.Length; i++)
                    {
                        map[i] = binaryReader.ReadInt32();
                    }
                    HuffmanTree.CreateHuffmanTree(map); // читаем карту байт из файла и по ней строим дерево
                    int additionBits = binaryReader.ReadByte(); // читам сколько доп байт
                    

                    var bytes = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length-map.Length*4-1);
                    // читаем закод сообшение


                    var bitArray = new BitArray(bytes);
                    HuffmanTree currentNode = HuffmanTree.Root;


                    using (var sw = new FileStream(ofd.FileName.Substring(0, ofd.FileName.Length - 3), FileMode.Create))
                    using (var bw = new BinaryWriter(sw))
                    {




                        for (int i = 0; i < bitArray.Length - additionBits; i++) // расшивровка
                        {
                            var boolean = bitArray[i]; // получам очередной бит
                            currentNode = boolean ? currentNode.RightBranch : currentNode.LeftBranch; // 1 вправо 0 - влево 
                            if (currentNode.LeftBranch == null) // нет детей - значит лист
                            {
                                bw.Write(currentNode.Value); // пишем значение листа в фаил
                                currentNode = HuffmanTree.Root; // переходим к корню
                            }

                        }
                    }
                }
                OutputTextBox.AppendText("\nФайл успешно расшифрован. Новое имя файла: " + ofd.FileName.Substring(0, ofd.FileName.Length - 3)+'\n');
                // вот и все, ребята!


            }
        }
    }
}
