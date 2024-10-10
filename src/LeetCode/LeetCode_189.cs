using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeetCode
{
    public class LeetCode_189
    {
        public static void Rotate(int[] nums, int k)
      {
        int n = nums.Length;
        k = k % n; // 处理 k 大于数组长度的情况

        // 反转整个数组
        Reverse(nums, 0, n - 1);
        // 反转前 k 个元素
        Reverse(nums, 0, k - 1);
        // 反转剩余的元素
        Reverse(nums, k, n - 1);
    }

     private static void Reverse(int[] nums, int start, int end)
    {
        while (start < end)
        {
            int temp = nums[start];
            nums[start] = nums[end];
            nums[end] = temp;
            start++;
            end--;
        }
    }
     public void Test() {
        int[] nums1 = { 1, 2, 3, 4, 5, 6, 7 };
        int k1 = 3;
        Rotate(nums1, k1);
        Console.WriteLine(string.Join(", ", nums1)); // 输出: [5, 6, 7, 1, 2, 3, 4]

        int[] nums2 = { -1, -100, 3, 99 };
        int k2 = 2;
        Rotate(nums2, k2);
        Console.WriteLine(string.Join(", ", nums2)); // 输出: [3, 99, -1, -100]
      }
    }
}