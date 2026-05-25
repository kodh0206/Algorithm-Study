using System;
using System.Collections.Generic;
namespace Algorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BinaryTree tree = new BinaryTree();

            int[] values = { 5, 3, 7, 2, 4, 6, 8 };

            foreach (int value in values)
            {
                tree.Insert(value);
            }

            Console.WriteLine("Binary tree console output:");
            tree.Show();

            Console.WriteLine();
            Console.WriteLine("In-order: " + string.Join(", ", tree.InOrder()));
            Console.WriteLine("Pre-order: " + string.Join(", ", tree.PreOrder()));
            Console.WriteLine("Post-order: " + string.Join(", ", tree.PostOrder()));

            Console.WriteLine();
            Console.WriteLine("Contains 4? " + tree.Contains(4));
            Console.WriteLine("Contains 10? " + tree.Contains(10));

            Console.ReadLine();
        }
    }
}