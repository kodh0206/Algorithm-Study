using System;
using System.Collections.Generic;

namespace Algorithm
{   ///<>summary>
/// A simple binary search tree for integers with insertion, traversal  
/// 이진트리 
/// Smaller one on the left, bigger one on the right
/// alway fill the left first, then the right
/// Binary Search Tree (BST) is a binary tree where each node has a value, and all values in the left subtree are less than the node's value, 
/// while all values in the right subtree are greater than or equal to the node's value. This property allows for efficient searching, insertion, 
/// \and deletion operations.
/// </summary>
   public class BinaryTree
    {
        public class Node
        {   
            // integer value stored in this node
            // 이 노드에 저장된 정수 값
            public int value;
            // left child (values less than value)
            // 왼쪽 자식 (value보다 작은 값)
            public Node Left;
            // right child (values greater than or equal to value)
            // 오른쪽 자식 (value보다 크거나 같은 값)
            public Node Right;
            
            //노드값 초기화 
            // Initialize node value
            public Node(int value)
            {
                this.value = value;
            }   

        }
        // The root node of the tree (null when empty).
        // 트리의 루트 노드 (비어 있을 때는 null).
        public Node Root; 

        // Inserts a value into the tree, maintaining the binary search tree property.
        // 트리에 값을 삽입하여 이진 탐색 트리 속성을 유지합니다
        public void Insert(int value)
        {
            Root = Insert(Root, value);
        }

        // Helper method to recursively insert a value starting from a given node.
        // 주어진 노드에서 시작하여 값을 재귀적으로 삽입하는 도우미 메서드입니다.
        public Node Insert(Node node, int value)
        {
            if (node == null)
                return new Node(value);

            if (value < node.value)
                node.Left = Insert(node.Left, value);
            else
                node.Right = Insert(node.Right, value);

            return node;
        }

        // Prints the tree structure to the console in a readable format.
        // 트리 구조를 읽기 쉬운 형식으로 콘솔에 출력합니다.
        public void Show()
        {
            if (Root == null)
            {
                Console.WriteLine("(empty tree)");
                return;
            }

            Print(Root, string.Empty, true);
        }   
        
        // Helper method to recursively print the tree structure starting from a given node.
        // 주어진 노드에서 시작하여 트리 구조를 재귀적으로 출력하는 도우미 메서드입니다.
        private void Print(Node node, string indent, bool isRight)
        {
            if (node == null)
            {
                Console.WriteLine(indent + (isRight ? "└─ " : "├─ ") + "null");
                return;
            }

            Console.WriteLine(indent + (isRight ? "└─ " : "├─ ") + node.value);
            indent += isRight ? "   " : "│  ";
            Print(node.Left, indent, false);
            Print(node.Right, indent, true);
        }


        /// <summary>
        /// Returns the values of the tree in-order (left, node, right).
        /// </summary>
        public IEnumerable<int> InOrder()
        {
            return InOrder(Root);
        }

        // Helper method to recursively traverse the tree in-order starting from a given node.
        // 주어진 노드에서 시작하여 트리를 중위 순회하는 도우미 메서드입니다.
        private IEnumerable<int> InOrder(Node node)
        {
            if (node == null) yield break;
            foreach (var v in InOrder(node.Left)) yield return v;
            yield return node.value;
            foreach (var v in InOrder(node.Right)) yield return v;
        }

        /// <summary>
        /// Returns the values of the tree pre-order (node, left, right).
        /// </summary>
        /// 트리의 값을 전위 순서로 반환합니다 (노드, 왼쪽, 오른쪽).
        /// Returns the values of the tree pre-order (node, left, right).
        public IEnumerable<int> PreOrder()
        {
            return PreOrder(Root);
        }

        private IEnumerable<int> PreOrder(Node node)
        {
            if (node == null) yield break;
            yield return node.value;
            foreach (var v in PreOrder(node.Left)) yield return v;
            foreach (var v in PreOrder(node.Right)) yield return v;
        }

        /// <summary>
        /// Returns the values of the tree post-order (left, right, node).
        /// </summary>
        /// 트리의 값을 후위 순서로 반환합니다 (왼쪽, 오른쪽, 노드).
        /// Returns the values of the tree post-order (left, right, node).
        public IEnumerable<int> PostOrder()
        {
            return PostOrder(Root);
        }

        private IEnumerable<int> PostOrder(Node node)
        {
            if (node == null) yield break;
            foreach (var v in PostOrder(node.Left)) yield return v;
            foreach (var v in PostOrder(node.Right)) yield return v;
            yield return node.value;
        }

        /// <summary>
        /// Returns true if the specified value exists in the tree.
        /// </summary>
        /// 트리에 지정된 값이 존재하면 true를 반환합니다.
        /// 탐색 방법은 이진 탐색 트리의 특성을 활용하여, 현재 노드의 값과 비교하여 왼쪽 또는 오른쪽 서브트리로 이동하는 방식입니다.
        /// Returns true if the specified value exists in the tree.
        /// The search method utilizes the properties of a binary search tree, where it compares the current node's value with the target value and decides to move left or right accordingly.
        public bool Contains(int value)
        {
            return Contains(Root, value);
        }

        private bool Contains(Node node, int value)
        {
            if (node == null) return false;
            if (node.value == value) return true;
            return value < node.value ? Contains(node.Left, value) : Contains(node.Right, value);
        }

    }
    
}
