// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;

public class NQueensSolver
{
    /// <summary>
    /// 回傳所有 N 皇后解，每個解是一個 string 陣列，'.' 表空格、'Q' 表皇后。
    /// 預設 n=8，即 8 皇后。
    /// </summary>
    public static IList<IList<string>> SolveNQueens(int n = 8)
    {
        var results = new List<IList<string>>();

        // 棋盤（用字元陣列好做原地改回 '.'
        var board = new char[n][];
        for (int r = 0; r < n; r++)
            board[r] = Enumerable.Repeat('.', n).ToArray();

        // 三種衝突紀錄：
        // col[c]    ：第 c 欄是否已有皇后
        // diag1[r+c]：左上到右下對角線是否已有皇后
        // diag2[r-c+n-1]：右上到左下對角線是否已有皇后
        var col = new bool[n];
        var diag1 = new bool[2 * n - 1];
        var diag2 = new bool[2 * n - 1];

        void Backtrack(int r)
        {
            // 走到第 n 列，代表前面 0..n-1 列都擺好，收集一個解
            if (r == n)
            {
                results.Add(board.Select(row => new string(row)).ToList());
                return;
            }

            // 嘗試把第 r 列的皇后放在每一欄
            for (int c = 0; c < n; c++)
            {
                int d1 = r + c;
                int d2 = r - c + n - 1;

                // 若同欄或任一對角線已有皇后，跳過
                if (col[c] || diag1[d1] || diag2[d2]) continue;

                // 放置
                col[c] = diag1[d1] = diag2[d2] = true;
                board[r][c] = 'Q';

                Backtrack(r + 1);

                // 還原
                board[r][c] = '.';
                col[c] = diag1[d1] = diag2[d2] = false;
            }
        }

        Backtrack(0);
        return results;
    }

    // 範例：列印前兩個解與總數
    public static void Main()
    {
        var solutions = SolveNQueens(8);
        Console.WriteLine($"Total solutions: {solutions.Count}");

        // 顯示前兩個解做驗證
        for (int i = 0; i < Math.Min(2, solutions.Count); i++)
        {
            Console.WriteLine($"// Solution {i + 1}");
            foreach (var row in solutions[i])
                Console.WriteLine(row);
            Console.WriteLine();
        }
    }
}
