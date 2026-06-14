using System;
using System.Collections.Generic;

class Program
{
    
    static List<int>[] graph;
    static bool[] visited;

    static void Main()
    {
        // 그래프 초기화
       int n =6;
       graph = new List<int>[n+1];

       for(int i=0; i<=n; i++)
       {
           graph[i] = new List<int>();
       }
    }
    static void AddEdge(int a, int b)
    {
        graph[a].Add(b);
        graph[b].Add(a);
    }

   
    static void DFS(int node) // 몇번 노드부터 탐색을 시작하겠다
    {   
        visited[node] = true; // 노드 방문처리
        Console.Write(node + " ");

        foreach(var next in graph[node])// 그래프를 돌아서 
        {
            if(!visited[next])// 방문이 안돼있으면 
            {
                DFS(next);// 재귀적으로 DFS 탐색
            }
        }
    }

    static void BFS(int start)
    {   // BFS에서 사용할 큐를 만든다.
        Queue<int> queue = new Queue<int>();

        // 시작 노드를 방분처리
        visited[start] =true;
        
        //시작 노드를 큐에 넣는다. 
        queue.Enqueue(start);

        //큐에 처리할 노드가 남아있는 동안 반복.
        while(queue.Count>0)
        {   
            // 큐에서 가장 먼저 들어온 노드를 꺼낸다.
            int node =queue.Dequeue();
            Console.Write(node + " ");

            //
            foreach(var next in graph[node])
            {
                if(!visited[next])//방문이 안됐으면
                {
                    visited[next] = true;
                    queue.Enqueue(next);
                }
            }
        }
    }
}