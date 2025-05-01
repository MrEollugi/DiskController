using System;
using System.Collections.Generic;

public class Solution
{
    public int solution(int[,] jobs)
    {
        int answer = 0;
        int n = jobs.GetLength(0);
        int[][] jobList = new int[n][];

        for (int i = 0; i < n; i++)
        {
            jobList[i] = new int[] { jobs[i, 0], jobs[i, 1] };
        }

        answer = Solve(jobList);

        return answer;
    }

    private int Solve(int[][] jobs)
    {
        Array.Sort(jobs, (a, b) => a[0].CompareTo(b[0]));

        int time = 0;
        int totalTime = 0;
        int count = 0;
        int index = 0;

        List<int[]> waiting = new List<int[]>();

        while (count < jobs.Length)
        {
            while (index < jobs.Length && jobs[index][0] <= time)
            {
                waiting.Add(jobs[index]);
                index++;
            }

            if (waiting.Count == 0)
            {
                time = jobs[index][0];
                continue;
            }

            waiting.Sort((a, b) => a[1].CompareTo(b[1]));
            int[] job = waiting[0];
            waiting.RemoveAt(0);

            time += job[1];
            totalTime += time - job[0];
            count++;
        }


        return totalTime / jobs.Length;
    }
}