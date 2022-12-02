using System.Collections.Generic;

namespace Data.LevelData
{
    public class RewardLevelData
    {
        public Dictionary<int, int> Rewards { get; }

        public RewardLevelData()
        {
            Rewards = new Dictionary<int, int>()
            {
                { 0, 300 },
                { 1, 300 },
                { 2, 300 },
                { 3, 300 },
                { 4, 300 },
                { 5, 600 },
                { 6, 600 },
                { 7, 600 },
                { 8, 600 },
                { 9, 900 },
                { 10, 900 },
                { 11, 900 },
                { 12, 900 },
                { 13, 900 },
                { 14, 900 },
                { 15, 900 },
                { 16, 900 },
                { 17, 900 },
                { 18, 900 },
                { 19, 900 },
                { 20, 900 },
                { 21, 900 },
                { 22, 900 },
                { 23, 900 },
                { 25, 900 },
                { 26, 900 },
            };
        }
    }
}