using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI.Roulette
{
    public class RewardRoulette : MonoBehaviour
    {
        [SerializeField] private Animation _winAnimations;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private GameObject _roulettePlate;
        [SerializeField] private Transform _needle;
        [SerializeField] private List<ItemInfo> _items = new ();
        [SerializeField] private List<Image> _displayItemSlot = new();
        [SerializeField] private List<TMP_Text> _levelText = new();
        [SerializeField] private Image _resultSprite;
        [SerializeField] private ParticleSystem _winEffect;

        private List<int> _startList = new List<int>();
        private List<ItemInfo> _itemListInDisplay = new List<ItemInfo>();
        private int _itemCount = 8;
        private ItemInfo _resultItem;

        public ItemInfo ResultItem => _resultItem;

        public event Action ItemWined;
        
        private void OnEnable()
        {
            for (int i = 0; i < _itemCount; i++)
            {
                _startList.Add(i);
            }
 
            for (int i = 0; i < _itemCount; i++)
            {
                var randomIndex = Random.Range(0, _items.Count);
                _itemListInDisplay.Add(_items[randomIndex]);
                _displayItemSlot[i].sprite = _items[randomIndex].Icon;
                _levelText[i].text = _items[randomIndex].Level.ToString();
            }
            
            Result();
        }

        public IEnumerator SpinRoulette(Action onComplete = null)
        {
            yield return StartCoroutine(StartRoulette());
            onComplete?.Invoke();
        }

        private IEnumerator StartRoulette()
        {
            var randomRotateSpeed = Random.Range(1f, 5f);   
            var rotateSpeed = 15f * randomRotateSpeed;

            while (true)
            {
                yield return null;
                if (rotateSpeed <= 0.01f) break;

                rotateSpeed = Mathf.Lerp(rotateSpeed, 0, Time.deltaTime * 0.9f);
                _roulettePlate.transform.Rotate(0,0,rotateSpeed);
                Result();
            }

            yield return new WaitForSeconds(0.3f);
            
            WinItemAnimationPlay();
            
            yield return new WaitForSeconds(2f);
            ItemWined?.Invoke();
        }

        private void WinItemAnimationPlay()
        {
            _audioSource.Play();
            _winAnimations.Play();
            _winEffect.Play();
        }

        private void Result()
        {
            var closeIndex = -1;
            float closeDistance = 500f;
            float currentDistance = 0f;

            for (int i = 0; i < _itemCount; i++)
            {
                currentDistance = Vector2.Distance(_displayItemSlot[i].transform.position, _needle.position);
                if (closeDistance > currentDistance)
                {
                    closeDistance = currentDistance;
                    closeIndex = i;
                }
                
                if(closeIndex == -1)
                    Debug.Log("Something is wrong!");

                _resultSprite.sprite = _displayItemSlot[closeIndex].sprite;
                _resultItem = _itemListInDisplay[closeIndex];
            }
        }
    }
}
