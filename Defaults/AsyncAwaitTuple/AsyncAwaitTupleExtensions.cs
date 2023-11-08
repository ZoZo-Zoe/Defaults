using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Defaults;
public static class AsyncAwaitTupleExtensions {
	public static TaskAwaiter<(T1, T2)> GetAwaiter<T1, T2>(this(Task<T1>, Task<T2>) tasks) {
		async Task<(T1, T2)> CreateTaskTuple() {
			var (task1, task2) = tasks;
			await Task.WhenAll(task1, task2);
			return (task1.Result, task2.Result);
		}

		return CreateTaskTuple().GetAwaiter();
	}

	public static TaskAwaiter<(T1, T2, T3)> GetAwaiter<T1, T2, T3>(this (Task<T1>, Task<T2>, Task<T3>) tasks) {
		async Task<(T1, T2, T3)> CreateTaskTuple() {
			var (task1, task2, task3) = tasks;
			await Task.WhenAll(task1, task2, task3);
			return (task1.Result, task2.Result, task3.Result);
		}

		return CreateTaskTuple().GetAwaiter();
	}

	public static TaskAwaiter<(T1, T2, T3, T4)> GetAwaiter<T1, T2, T3, T4>(this (Task<T1>, Task<T2>, Task<T3>, Task<T4>) tasks) {
		async Task<(T1, T2, T3, T4)> CreateTaskTuple() {
			var (task1, task2, task3, task4) = tasks;
			await Task.WhenAll(task1, task2, task3, task4);
			return (task1.Result, task2.Result, task3.Result, task4.Result);
		}

		return CreateTaskTuple().GetAwaiter();
	}

	public static TaskAwaiter<(T1, T2, T3, T4, T5)> GetAwaiter<T1, T2, T3, T4, T5>(this (Task<T1>, Task<T2>, Task<T3>, Task<T4>, Task<T5>) tasks) {
		async Task<(T1, T2, T3, T4, T5)> CreateTaskTuple() {
			var (task1, task2, task3, task4, task5) = tasks;
			await Task.WhenAll(task1, task2, task3, task4, task5);
			return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result);
		}

		return CreateTaskTuple().GetAwaiter();
	}

	public static TaskAwaiter<(T1, T2, T3, T4, T5, T6)> GetAwaiter<T1, T2, T3, T4, T5, T6>(this (Task<T1>, Task<T2>, Task<T3>, Task<T4>, Task<T5>, Task<T6>) tasks) {
		async Task<(T1, T2, T3, T4, T5, T6)> CreateTaskTuple() {
			var (task1, task2, task3, task4, task5, task6) = tasks;
			await Task.WhenAll(task1, task2, task3, task4, task5, task6);
			return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result);
		}

		return CreateTaskTuple().GetAwaiter();
	}

	public static TaskAwaiter<(T1, T2, T3, T4, T5, T6, T7)> GetAwaiter<T1, T2, T3, T4, T5, T6, T7>(this (Task<T1>, Task<T2>, Task<T3>, Task<T4>, Task<T5>, Task<T6>, Task<T7>) tasks) {
		async Task<(T1, T2, T3, T4, T5, T6, T7)> CreateTaskTuple() {
			var (task1, task2, task3, task4, task5, task6, task7) = tasks;
			await Task.WhenAll(task1, task2, task3, task4, task5, task6, task7);
			return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result);
		}

		return CreateTaskTuple().GetAwaiter();
	}

	public static TaskAwaiter<(T1, T2, T3, T4, T5, T6, T7, T8)> GetAwaiter<T1, T2, T3, T4, T5, T6, T7, T8>(this (Task<T1>, Task<T2>, Task<T3>, Task<T4>, Task<T5>, Task<T6>, Task<T7>, Task<T8>) tasks) {
		async Task<(T1, T2, T3, T4, T5, T6, T7, T8)> CreateTaskTuple() {
			var (task1, task2, task3, task4, task5, task6, task7, task8) = tasks;
			await Task.WhenAll(task1, task2, task3, task4, task5, task6, task7, task8);
			return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result, task8.Result);
		}

		return CreateTaskTuple().GetAwaiter();
	}

	public static TaskAwaiter<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> GetAwaiter<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this (Task<T1>, Task<T2>, Task<T3>, Task<T4>, Task<T5>, Task<T6>, Task<T7>, Task<T8>, Task<T9>) tasks) {
		async Task<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> CreateTaskTuple() {
			var (task1, task2, task3, task4, task5, task6, task7, task8,task9) = tasks;
			await Task.WhenAll(task1, task2, task3, task4, task5, task6, task7, task8, task9);
			return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result, task8.Result, task9.Result);
		}

		return CreateTaskTuple().GetAwaiter();
	}

	public static TaskAwaiter<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)> GetAwaiter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this (Task<T1>, Task<T2>, Task<T3>, Task<T4>, Task<T5>, Task<T6>, Task<T7>, Task<T8>, Task<T9>, Task<T10>) tasks) {
		async Task<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)> CreateTaskTuple() {
			var (task1, task2, task3, task4, task5, task6, task7, task8, task9, task10) = tasks;
			await Task.WhenAll(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10);
			return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result, task8.Result, task9.Result, task10.Result);
		}

		return CreateTaskTuple().GetAwaiter();
	}
}
