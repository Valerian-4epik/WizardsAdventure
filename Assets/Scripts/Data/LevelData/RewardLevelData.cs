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
                { 0, 30 },
                { 1, 30 },
                { 2, 30 },
                { 3, 30 },
                { 4, 30 },
                { 5, 60 },
                { 6, 60 },
                { 7, 60 },
                { 8, 60 },
                { 9, 60 },
                { 10, 60 },
                { 11, 60 },
                { 12, 60 },
                { 13, 90 },
                { 14, 90 },
                { 15, 90 },
                { 16, 90 },
                { 17, 90 },
                { 18, 90 },
                { 19, 90 },
                { 20, 90 },
                { 21, 90 },
                { 22, 90 },
                { 23, 90 },
                { 25, 90 },
                { 26, 90 },
            };
        }
    }
}