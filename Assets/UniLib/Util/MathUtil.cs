using System;
using System.Collections.Generic;

namespace UniLib.Util
{
    /// <summary>
    /// 計算ユーティリティ
    /// </summary>
    public static class MathUtil
    {
        /// <summary>
        /// 対象値の各桁ごとの数を配列として取得
        /// </summary>
        /// <param name="targetNum"></param>
        /// <returns></returns>
        public static int[] GetNumParDigits(int targetNum)
        {
            var numList = new List<int>();
            var digit = targetNum.ToString().Length;
            for(int i = digit - 1; i >= 0; i--)
            {
                var split = targetNum / (int)(Math.Pow(10, i)) % 10;
                numList.Add(split);
            }

            return numList.ToArray();
        }
    }
}
