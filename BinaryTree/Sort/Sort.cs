using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Algorithm
{
    public static class SortAlgorithms
    {
        public static void MeasureAndPrintAll(int[] values)
        {
            var sorts = new List<(string Name, Func<int[], int[]> Sort)>
            {
                ("Bubble Sort", BubbleSort),
                ("Selection Sort", SelectionSort),
                ("Insertion Sort", InsertionSort),
                ("Shell Sort", ShellSort),
                ("Merge Sort", MergeSort),
                ("Quick Sort", QuickSort),
                ("Heap Sort", HeapSort),
                ("Counting Sort", CountingSort),
                ("Radix Sort", RadixSort),
                ("Bucket Sort", BucketSort),
            };

            foreach (var (name, sort) in sorts)
            {
                var result = MeasureSort(name, sort, values);
                Console.WriteLine($"{name}: {string.Join(", ", result.SortedArray)}");
                Console.WriteLine($"{name} elapsed: {result.Elapsed.TotalMilliseconds:F3} ms\n");
            }
        }

        public static (int[] SortedArray, TimeSpan Elapsed) MeasureSort(string name, Func<int[], int[]> sort, int[] values)
        {
            int[] copy = (int[])values.Clone();
            var stopwatch = Stopwatch.StartNew();
            int[] sorted = sort(copy);
            stopwatch.Stop();
            return (sorted, stopwatch.Elapsed);
        }

        public static int[] BubbleSort(int[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                bool swapped = false;
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        Swap(array, j, j + 1);
                        swapped = true;
                    }
                }

                if (!swapped)
                {
                    break;
                }
            }

            return array;
        }

        public static int[] SelectionSort(int[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (array[j] < array[minIndex])
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    Swap(array, i, minIndex);
                }
            }

            return array;
        }

        public static int[] InsertionSort(int[] array)
        {
            int n = array.Length;
            for (int i = 1; i < n; i++)
            {
                int key = array[i];
                int j = i - 1;
                while (j >= 0 && array[j] > key)
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = key;
            }

            return array;
        }

        public static int[] ShellSort(int[] array)
        {
            int n = array.Length;
            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < n; i++)
                {
                    int temp = array[i];
                    int j = i;
                    while (j >= gap && array[j - gap] > temp)
                    {
                        array[j] = array[j - gap];
                        j -= gap;
                    }
                    array[j] = temp;
                }
            }

            return array;
        }

        public static int[] MergeSort(int[] array)
        {
            if (array.Length <= 1)
            {
                return array;
            }

            int mid = array.Length / 2;
            int[] left = new int[mid];
            int[] right = new int[array.Length - mid];
            Array.Copy(array, 0, left, 0, mid);
            Array.Copy(array, mid, right, 0, right.Length);

            return Merge(MergeSort(left), MergeSort(right));
        }

        private static int[] Merge(int[] left, int[] right)
        {
            int[] merged = new int[left.Length + right.Length];
            int i = 0, j = 0, k = 0;
            while (i < left.Length && j < right.Length)
            {
                merged[k++] = left[i] <= right[j] ? left[i++] : right[j++];
            }
            while (i < left.Length)
            {
                merged[k++] = left[i++];
            }
            while (j < right.Length)
            {
                merged[k++] = right[j++];
            }
            return merged;
        }

        public static int[] QuickSort(int[] array)
        {
            QuickSortInternal(array, 0, array.Length - 1);
            return array;
        }

        private static void QuickSortInternal(int[] array, int low, int high)
        {
            if (low >= high)
            {
                return;
            }

            int pivot = array[(low + high) / 2];
            int i = low;
            int j = high;

            while (i <= j)
            {
                while (array[i] < pivot) i++;
                while (array[j] > pivot) j--;
                if (i <= j)
                {
                    Swap(array, i, j);
                    i++;
                    j--;
                }
            }

            if (low < j) QuickSortInternal(array, low, j);
            if (i < high) QuickSortInternal(array, i, high);
        }

        public static int[] HeapSort(int[] array)
        {
            int n = array.Length;
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Heapify(array, n, i);
            }
            for (int i = n - 1; i >= 0; i--)
            {
                Swap(array, 0, i);
                Heapify(array, i, 0);
            }
            return array;
        }

        private static void Heapify(int[] array, int heapSize, int rootIndex)
        {
            int largest = rootIndex;
            int left = 2 * rootIndex + 1;
            int right = 2 * rootIndex + 2;

            if (left < heapSize && array[left] > array[largest])
            {
                largest = left;
            }
            if (right < heapSize && array[right] > array[largest])
            {
                largest = right;
            }
            if (largest != rootIndex)
            {
                Swap(array, rootIndex, largest);
                Heapify(array, heapSize, largest);
            }
        }

        public static int[] CountingSort(int[] array)
        {
            if (array.Length == 0)
            {
                return array;
            }

            int min = array[0];
            int max = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < min) min = array[i];
                if (array[i] > max) max = array[i];
            }

            int range = max - min + 1;
            int[] counts = new int[range];
            foreach (int value in array)
            {
                counts[value - min]++;
            }

            int index = 0;
            for (int i = 0; i < counts.Length; i++)
            {
                while (counts[i]-- > 0)
                {
                    array[index++] = i + min;
                }
            }

            return array;
        }

        public static int[] RadixSort(int[] array)
        {
            if (array.Length == 0)
            {
                return array;
            }

            int[] positives = Array.FindAll(array, x => x >= 0);
            int[] negatives = Array.FindAll(array, x => x < 0);

            if (positives.Length > 0)
            {
                RadixSortNonNegative(positives);
            }

            if (negatives.Length > 0)
            {
                for (int i = 0; i < negatives.Length; i++)
                {
                    negatives[i] = -negatives[i];
                }
                RadixSortNonNegative(negatives);
                for (int i = 0; i < negatives.Length; i++)
                {
                    negatives[i] = -negatives[i];
                }
                Array.Reverse(negatives);
            }

            int[] result = new int[array.Length];
            Array.Copy(negatives, 0, result, 0, negatives.Length);
            Array.Copy(positives, 0, result, negatives.Length, positives.Length);
            return result;
        }

        private static void RadixSortNonNegative(int[] array)
        {
            int maxValue = 0;
            foreach (int value in array)
            {
                if (value > maxValue) maxValue = value;
            }

            int place = 1;
            int[] output = new int[array.Length];
            while (maxValue / place > 0)
            {
                int[] counts = new int[10];
                foreach (int value in array)
                {
                    counts[(value / place) % 10]++;
                }
                for (int i = 1; i < 10; i++)
                {
                    counts[i] += counts[i - 1];
                }
                for (int i = array.Length - 1; i >= 0; i--)
                {
                    int digit = (array[i] / place) % 10;
                    output[--counts[digit]] = array[i];
                }
                Array.Copy(output, array, array.Length);
                place *= 10;
            }
        }

        public static int[] BucketSort(int[] array)
        {
            if (array.Length == 0)
            {
                return array;
            }

            int min = array[0];
            int max = array[0];
            foreach (int value in array)
            {
                if (value < min) min = value;
                if (value > max) max = value;
            }

            int bucketCount = Math.Max(1, array.Length / 2);
            List<List<int>> buckets = new List<List<int>>(bucketCount);
            for (int i = 0; i < bucketCount; i++)
            {
                buckets.Add(new List<int>());
            }

            double range = max - min + 1;
            foreach (int value in array)
            {
                int bucketIndex = (int)((value - min) / range * bucketCount);
                if (bucketIndex == bucketCount) bucketIndex--;
                buckets[bucketIndex].Add(value);
            }

            int index = 0;
            foreach (var bucket in buckets)
            {
                if (bucket.Count > 0)
                {
                    int[] bucketArray = bucket.ToArray();
                    InsertionSort(bucketArray);
                    foreach (int value in bucketArray)
                    {
                        array[index++] = value;
                    }
                }
            }

            return array;
        }

        private static void Swap(int[] array, int i, int j)
        {
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}
