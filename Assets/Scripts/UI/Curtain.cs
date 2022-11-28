using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class Curtain : MonoBehaviour
{
    [SerializeField] private List<Sprite> _backgroundsSlot = new List<Sprite>();
    [SerializeField] private List<Sprite> _backgroundsLevelNumber = new List<Sprite>();
    [SerializeField] private List<ItemInfo> _items = new List<ItemInfo>();
    [SerializeField] private List<CurtainSlot> _slots = new List<CurtainSlot>();
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _sliderText;

    private CanvasGroup _canvasGroup;
    private Camera _camera;
    private PlayableDirector _playableDirector;
    
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        foreach (var slot in _slots)
        {
            slot.FillSlot(GetRandomSprite(_backgroundsSlot), GetRandomSprite(_backgroundsLevelNumber), GetRandomItem());
        }
    }
    
    public void Show() => _canvasGroup.alpha = 1;

    public void Hide() =>
        StartCoroutine(Delay());

    private IEnumerator Delay()
    {
        while (_slider.value < 100)
        {
            var delay = Random.Range(0.05f, 0.1f);
            _slider.value += 2.1f;
            _sliderText.text = ($"Loading...{Math.Round(_slider.value, 1)}%");
            yield return new WaitForSeconds(delay);
        }
        
        _camera = Camera.main;
        _playableDirector = _camera.gameObject.GetComponent<PlayableDirector>();
        _playableDirector.Play();
        DisableCartain();
    }

    private void DisableCartain()
    {
        _slider.value = 0;
        _canvasGroup.alpha = 0;
        // gameObject.SetActive(false);
    }

    private int GetRandomNumber(int maxValue)
    {
        var number = Random.Range(0, maxValue - 1);
        return number;
    }

    private Sprite GetRandomSprite(List<Sprite> sprites) => sprites[GetRandomNumber(sprites.Count)];

    private ItemInfo GetRandomItem() => _items[GetRandomNumber(_items.Count)];
}