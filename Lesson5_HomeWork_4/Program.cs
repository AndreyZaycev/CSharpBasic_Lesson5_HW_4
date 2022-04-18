using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Lesson5_HomeWork_4
{
    internal class Program
    {
        //ввод корневой директории
        private static string GetRootDirectory()
        {
            Console.Write("Введите корневую директорию [шаблон: c://RootFolder// ] : ");
            return Console.ReadLine();
        }

        //рекурсивный метод получения дерева каталогов и файлов
        private static List<string> GetTreeRecursiv(string rootDirectory)
        {
            //
            List<string> listTree = new List<string>();

            //
            string[] arrayDirectory = Directory.GetDirectories(rootDirectory); //список каталогов корневой директории rootDirectory
            foreach (string currentDir in arrayDirectory)
            {
                listTree.Add("Папка: " + currentDir);           //добавление в список каталогов 
                listTree.AddRange(GetTreeRecursiv(currentDir)); //рекурсия
            }
            foreach (string fileName in Directory.GetFiles(rootDirectory)) listTree.Add("Файл: " + fileName); //добавление в список файлов 

            //
            return listTree;
        }

        //нерекурсивный метод получения дерева каталогов и файлов
        private static List<string> GetTreeNotRecursiv(string rootDirectory)
        {
            //
            List<string> listTree = new List<string>();

            //
            Stack<string> stackDirectories = new Stack<string>();
            stackDirectories.Push(rootDirectory); //добавляем в стек rootDirectory
            while (stackDirectories.Count > 0)
            {
                //извлекаем и возвращаем первый элемент из стека, далее этот элемент удаляется
                string currentDir = stackDirectories.Pop();

                //получение списка поддиректорий и файлов в текущей директории
                List<string> arraySubDirs = Directory.GetDirectories(currentDir).ToList();
                List<string> arrayFiles = Directory.GetFiles(currentDir).ToList();

                //
                foreach (string sSubDir in arraySubDirs)
                {
                    stackDirectories.Push(sSubDir);    //добавление в стек каталогов 
                    listTree.Add("Папка: " + sSubDir); //добавление в возвращаемый список каталогов 
                }

                //добавление в возвращаемый список файлов
                foreach (string fileName in Directory.GetFiles(currentDir)) listTree.Add("Файл: " + fileName);
            }

            //
            return listTree;
        }

        //запись данных в файл
        private static void WriteDataToFile(string fileName, List<string> vData)
        {
            try
            {
                File.WriteAllLines(fileName, vData);
                Console.WriteLine($"\nДанные записаны в файл {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}");
            }
        }

        //считать данные файла и вывести на консоль
        private static void ReadAndPrintFileData(string fileName, bool bRecursiv)
        {
            //считать данные
            string[] vData = File.ReadAllLines(fileName);

            //вывод данных на консоль
            string sRecursiv = (bRecursiv) ? "с использованием рекурсии" : "без использования рекурсии";
            Console.WriteLine($"\n -- Данные файла {fileName} : Дерево каталогов и файлов полученных {sRecursiv} -- ");
            foreach (string s in vData) Console.WriteLine(s);
        }


        static void Main(string[] args)
        {
            //
            Console.WriteLine("----- Урок № 5, задание № 4 -----");
            Console.WriteLine();

            //имя записываемых файлов
            string fileNameTreeRecursiv = "treeRecursiv.txt";        //дерево с рекурсией
            string fileNameTreeNotRecursiv = "treeNotRecursiv.txt";  //дерево без рекурсии

            //ввод корневой директории
            string rootDirectory = GetRootDirectory();

            //получение рекурсивным методом дерева каталогов и файлов и его запись в файл 
            WriteDataToFile(fileNameTreeRecursiv, GetTreeRecursiv(rootDirectory));

            //получение нерекурсивным методом дерева каталогов и файлов и его запись в файл
            WriteDataToFile(fileNameTreeNotRecursiv, GetTreeNotRecursiv(rootDirectory));

            //считать данные файла "treeRecursiv.txt" и вывести на консоль
            ReadAndPrintFileData(fileNameTreeRecursiv, true);

            //считать данные файла "treeNotRecursiv.txt" и вывести на консоль
            ReadAndPrintFileData(fileNameTreeNotRecursiv, false);

            //
            Console.ReadLine();
        }
    }
}
