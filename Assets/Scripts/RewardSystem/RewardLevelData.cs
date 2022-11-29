using System.Collections.Generic;

namespace Data
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
                { 3, 600 },
                { 4, 600 },
                { 5, 600 },
                { 6, 900 },
                { 7, 900 },
                { 8, 900 },
                { 9, 900 },
                { 10, 900 },
            };
        }
    }
}