using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class RandomGenerator
    {
        private int _upToLevelNumber = 6;
        private List<ItemInfo> _items;
        
        public ItemInfo GetRandomItem(List<ItemInfo> items)
        {
            _items = items;
            var maxPriority = 3;
            var mediumPriority = 2;
            var lowPriority = 1;
            List<ItemInfo> sampleNumbers = new List<ItemInfo>();

            foreach (var item in _items)
            {
                if (item.Level <= 3)
                {
                    for (int i = 0; i < maxPriority; i++)
                    {
                        sampleNumbers.Add(item);
                    }
                }
                else if (item.Level <= 5 && item.Level > 3)
                {
                    for (int i = 0; i < mediumPriority; i++)
                    {
                        sampleNumbers.Add(item);
                    }
                }
                else if (item.Level == _upToLevelNumber)
                {
                    sampleNumbers.Add(item);
                }
            }

            int number = Random.Range(0, sampleNumbers.Count - 1);
            return sampleNumbers[number];
        }
    }
}