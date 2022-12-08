using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomItemADS : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private GameObject _levelPanel;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _doneImage;
    [SerializeField] private GameObject _description;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _successfuleSoundFx;

    private ItemInfo _item;
    
    public ItemInfo Item => _item;

    public event Action ButtonClick;

    public void SetItem(ItemInfo item)
    {
        _item = item;
        SetImage(_item.Icon);
        _levelText.text = _item.Level.ToString();
    }

    private void SetImage(Sprite sprite) => 
        _image.sprite = sprite;

    public void BlockButton()
    {
        PlaySoudFx();
        _button.enabled = false;
        _levelPanel.gameObject.SetActive(false);
        _image.gameObject.SetActive(false);
        _description.SetActive(false);
        _doneImage.SetActive(true);
    }

    private void PlaySoudFx()
    {
        _audioSource.clip = _successfuleSoundFx;
        _audioSource.Play();
    }
}
