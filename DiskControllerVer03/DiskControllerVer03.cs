using System;
using System.Collections.Generic;

public class Solution
{
    public int solution(int[,] jobs)
    {
        int n = jobs.GetLength(0);
        List<int[]> jobList = new List<int[]>();

        for (int i = 0; i < n; i++)
        {
            jobList.Add(new int[] { jobs[i, 0], jobs[i, 1] });
        }

        jobList.Sort((a, b) => a[0].CompareTo(b[0]));

        MinHeap heap = new MinHeap();

        int time = 0, idx = 0, totalTime = 0, count = 0;

        while (count < n)
        {
            while (idx < n && jobList[idx][0] <= time)
            {
                heap.Push(jobList[idx]);
                idx++;
            }

            if (!heap.Any())
            {
                time = jobList[idx][0];
                continue;
            }

            var job = heap.Pop();
            time += job[1];
            totalTime += time - job[0];
            count++;
        }

        return totalTime / n;
    }

    class MinHeap
    {
        private List<int[]> heap = new List<int[]>();

        public void Push(int[] job)
        {
            heap.Add(job);
            int c = heap.Count - 1;
            while (c > 0)
            {
                int p = (c - 1) / 2;
                if (heap[c][1] >= heap[p][1]) break;
                Swap(c, p);
                c = p;
            }
        }

        public int[] Pop()
        {
            int[] ret = heap[0];
            int last = heap.Count - 1;
            heap[0] = heap[last];
            heap.RemoveAt(last);

            int p = 0;
            while (true)
            {
                int left = 2 * p + 1, right = 2 * p + 2, min = p;

                if (left < heap.Count && heap[left][1] < heap[min][1]) min = left;
                if (right < heap.Count && heap[right][1] < heap[min][1]) min = right;
                if (min == p) break;

                Swap(p, min);
                p = min;
            }

            return ret;
        }

        public bool Any() => heap.Count > 0;

        private void Swap(int i, int j)
        {
            int[] tmp = heap[i];
            heap[i] = heap[j];
            heap[j] = tmp;
        }
    }
}
