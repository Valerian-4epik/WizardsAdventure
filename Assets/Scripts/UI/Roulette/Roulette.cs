using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI.Roulette
{
    public class Roulette : MonoBehaviour
    {
        [SerializeField] private GameObject _roulettePlate;
        [SerializeField] private GameObject _roulettePanel;
        [SerializeField] private Transform _needle;
        [SerializeField] private List<ItemInfo> _itemSprites;
        [SerializeField] private List<Image> _displayItemSlot;
        [SerializeField] private Image _resultSprite;
        [SerializeField] private ParticleSystem _winEffect;

        private List<int> _startList = new List<int>();
        private List<int> _resultIndexList = new List<int>();
        private int _itemCount = 8;

        private ItemInfo _item;

        private void OnEnable()
        {
            for (int i = 0; i < _itemCount; i++)
            {
                _startList.Add(i);
            }
 
            for (int i = 0; i < _itemCount; i++)
            {
                var randomIndex = Random.Range(0, _itemSprites.Count);
                _displayItemSlot[i].sprite = _itemSprites[randomIndex].Icon;
            }
            
            Result();
        }

        public void SpinRoulite() => 
            StartCoroutine(StartRoulette());

        private IEnumerator StartRoulette()
        {
            var randomRotateSpeed = Random.Range(1f, 5f);   
            var rotateSpeed = 20f * randomRotateSpeed;

            while (true)
            {
                yield return null;
                if (rotateSpeed <= 0.01f) break;

                rotateSpeed = Mathf.Lerp(rotateSpeed, 0, Time.deltaTime * 0.6f);
                _roulettePlate.transform.Rotate(0,0,rotateSpeed);
                Result();
            }

            yield return new WaitForSeconds(1f);
            WinItemAnimationPlay();
        }

        private void WinItemAnimationPlay()
        {
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
            }
        }
    }
}
