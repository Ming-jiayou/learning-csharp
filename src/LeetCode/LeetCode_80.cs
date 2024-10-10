using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeetCode
{
    public class LeetCode_80
    {
         public int RemoveDuplicates(int[] nums) {
            if (nums.Length <= 2) {
                return nums.Length;
            }

            int i = 2; // 慢指针，指向当前已经处理好的数组的最后一个位置
            for (int j = 2; j < nums.Length; j++) {
                if (nums[j] != nums[i - 2]) {
                    nums[i] = nums[j];
                    i++;
                }
            }

            return i;
        }

        public void Test() {
            int[] nums1 = {1, 1, 1, 2, 2, 3};
            int len1 = RemoveDuplicates(nums1);
            Console.WriteLine("New length: " + len1);
            Console.Write("Modified array: " );
            for (int k = 0; k < len1; k++) 
            {
               Console.Write(nums1[k] + " ");
            }
            Console.WriteLine();
      }
    }
}