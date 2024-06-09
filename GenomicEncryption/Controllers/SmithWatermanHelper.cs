using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenomicEncryption.Controllers
{
    public class SmithWatermanHelper
    {
        private const int MatchScore = 2;
        private const int MismatchScore = -1;
        private const int GapScore = -1;

        public static (int score, string align1, string align2, double percentage) CalculateSmithWaterman(string seq1, string seq2)
        {
            int m = seq1.Length;
            int n = seq2.Length;

            int[,] scoreMatrix = new int[m + 1, n + 1];
            int maxScore = 0;
            int maxI = 0, maxJ = 0;

            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    int match = scoreMatrix[i - 1, j - 1] + (seq1[i - 1] == seq2[j - 1] ? MatchScore : MismatchScore);
                    int delete = scoreMatrix[i - 1, j] + GapScore;
                    int insert = scoreMatrix[i, j - 1] + GapScore;
                    scoreMatrix[i, j] = Math.Max(0, Math.Max(match, Math.Max(delete, insert)));

                    if (scoreMatrix[i, j] > maxScore)
                    {
                        maxScore = scoreMatrix[i, j];
                        maxI = i;
                        maxJ = j;
                    }
                }
            }

            string align1 = string.Empty;
            string align2 = string.Empty;
            int x = maxI, y = maxJ;

            while (x > 0 && y > 0 && scoreMatrix[x, y] > 0)
            {
                if (scoreMatrix[x, y] == scoreMatrix[x - 1, y - 1] + (seq1[x - 1] == seq2[y - 1] ? MatchScore : MismatchScore))
                {
                    align1 = seq1[x - 1] + align1;
                    align2 = seq2[y - 1] + align2;
                    x--;
                    y--;
                }
                else if (scoreMatrix[x, y] == scoreMatrix[x - 1, y] + GapScore)
                {
                    align1 = seq1[x - 1] + align1;
                    align2 = "-" + align2;
                    x--;
                }
                else
                {
                    align1 = "-" + align1;
                    align2 = seq2[y - 1] + align2;
                    y--;
                }
            }

            int longestSeqLength = Math.Max(seq1.Length, seq2.Length);
            double percentage = ((double)maxScore / (longestSeqLength * MatchScore)) * 100;

            return (maxScore, align1, align2, percentage);
        }
    }

}