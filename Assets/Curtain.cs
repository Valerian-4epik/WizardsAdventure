using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Curtain : MonoBehaviour
{
    [SerializeField] private List<Sprite> _backgroundsSlot = new List<Sprite>();
    [SerializeField] private List<Sprite> _backgroundsLevelNumber = new List<Sprite>();
    [SerializeField] private List<ItemInfo> _items = new List<ItemInfo>();
    [SerializeField] private List<CurtainSlot> _slots = new List<CurtainSlot>();

    private void Start()
    {
        foreach (var slot in _slots)
        {
            slot.FillSlot(GetRandomSprite(_backgroundsSlot), GetRandomSprite(_backgroundsLevelNumber), GetRandomItem());
        }
    }

    private int GetRandomNumber(int maxValue)
    {
        var number = Random.Range(0, maxValue - 1);
        return number;
    }

    private Sprite GetRandomSprite(List<Sprite> sprites) => sprites[GetRandomNumber(sprites.Count)];

    private ItemInfo GetRandomItem() => _items[GetRandomNumber(_items.Count)];
}