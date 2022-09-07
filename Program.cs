using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lab_3
{
	class Program
	{
		static void Main(string[] args)
		{
			TaskGenerator taskGenerate = new();
			Task<string>[] tasks = new Task<string>[10];

			for (int i = 0; i < 10; i++)
			{
				tasks[i] = taskGenerate.create($"Task - {i + 1}");
			}
			Parallel.ForEach(tasks, task =>
			{
				task.Start();
			}
			);

			Console.WriteLine("\nИтог: " + tasks[Task.WaitAny(tasks)].Result);
		}
	}
	class TaskGenerator
	{
		private readonly Random random = new Random();
		private const int minDelay = 6, maxDelay = 222;


		public Task<string> create(string tag)
		{
			var delay = random.Next(minDelay, maxDelay);
			return getTag(tag, delay);
		}

		private Task<string> getTag(string tag, int delay)
		{
			Console.WriteLine($"---> {tag} has delay: {delay}");
			return new Task<string>(() =>
			{
				Thread.Sleep(delay);
				Console.WriteLine($"---> {tag} executed delay: {delay}");
				return tag;
			}
			);


		}
	}
}