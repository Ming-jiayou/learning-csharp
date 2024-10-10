using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeetCode
{
    public class LeetCode_122
    {
         public int MaxProfit(int[] prices) {
                if (prices == null || prices.Length == 0) {
                return 0;
            }
            
            int minPrice = int.MaxValue;
            int maxProfit = 0;
            
            foreach (int price in prices) {
                if (price < minPrice) {
                    minPrice = price;
                } else {
                    int currentProfit = price - minPrice;
                    if (currentProfit > maxProfit) {
                        maxProfit = currentProfit;
                    }
                }
            }
            
            return maxProfit;
         }
        


    public void Test() {
        int[] prices1 = {7, 1, 5, 3, 6, 4};
        Console.WriteLine(MaxProfit(prices1)); 
    }

    }
}