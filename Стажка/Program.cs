using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        int[] input = Console.ReadLine().Split().Select(int.Parse).ToArray();
        int r1 = input[0], s1 = input[1], r2 = input[2], s2 = input[3];

        List<string> grayPapers = new List<string>();
        for (int i = 0; i < r1 + s1; i++)
            grayPapers.Add(Console.ReadLine());

        List<string> whitePapers = new List<string>();
        for (int i = 0; i < r2 + s2; i++)
            whitePapers.Add(Console.ReadLine());

        double grayProb = CalculateProbability(grayPapers, r1, s1);
        double whiteProb = CalculateProbability(whitePapers, r2, s2);

        Console.WriteLine(Math.Max(grayProb, whiteProb).ToString("F10"));
    }

    static double CalculateProbability(List<string> papers, int r, int s)
    {

        HashSet<string> deck = new HashSet<string>();
        string[] suits = { "C", "D", "H", "S" };
        string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "T", "J", "Q", "K", "A" };
        foreach (var suit in suits)
            foreach (var rank in ranks)
                deck.Add(rank + suit);

        for (int i = 0; i < r; i++)
        {
            var subset = ParseSubset(papers[i]);
            deck.ExceptWith(subset);
            if (deck.Count == 0)
                return 0;
        }

        HashSet<string> winningCards = new HashSet<string>();
        for (int i = r; i < r + s; i++)
        {
            var subset = ParseSubset(papers[i]);
            winningCards.UnionWith(subset);
        }

        int winCount = deck.Intersect(winningCards).Count();
        return (double)winCount / deck.Count;
    }

    static HashSet<string> ParseSubset(string subsetStr)
    {
        HashSet<string> subset = new HashSet<string>();
        string[] suits = { "C", "D", "H", "S" };
        string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "T", "J", "Q", "K", "A" };

        var rankPart = new string(subsetStr.TakeWhile(c => "23456789TJQKA".Contains(c)).ToArray());
        var suitPart = new string(subsetStr.Skip(rankPart.Length).TakeWhile(c => "CDHS".Contains(c)).ToArray());

        if (rankPart.Length == 0)
        {
            foreach (var suit in suitPart)
                foreach (var rank in ranks)
                    subset.Add(rank + suit);
        }

        else if (suitPart.Length == 0)
        {
            foreach (var rank in rankPart)
                foreach (var suit in suits)
                    subset.Add(rank.ToString() + suit);
        }
        else

        {
            foreach (var rank in rankPart)
                foreach (var suit in suitPart)
                    subset.Add(rank.ToString() + suit);
        }

        return subset;
    }
}