using System;
using System.Collections.Generic;
using System.Linq;

public class Solution
{
    public int solution(int[,] jobs)
    {
        int answer = 0;
        List<Job> allJobs = new List<Job>();
        for (int i = 0; i < jobs.GetLength(0); i++)
        {
            allJobs.Add(new Job(jobs[i, 0], jobs[i, 1]));
        }

        int currentTime = 0;

        allJobs = allJobs.OrderBy(j => j.Start).ToList();

        List<Job> waiting = new List<Job>();
        int index = 0;
        int count = 0;

        while (count < jobs.GetLength(0))
        {
            while (index < allJobs.Count && allJobs[index].Start <= currentTime)
            {
                waiting.Add(allJobs[index++]);
            }

            if (waiting.Count == 0)
            {
                currentTime = allJobs[index].Start;
                continue;
            }

            var job = waiting.OrderBy(j => j.Len).First();
            waiting.Remove(job);

            currentTime += job.Len;
            answer += currentTime - job.Start;
            count++;
        }

        return answer / jobs.GetLength(0);
    }

    public class Job
    {
        public int Start { get; set; }
        public int Len { get; set; }

        public Job(int start, int len)
        {
            Start = start;
            Len = len;
        }
    }
}
