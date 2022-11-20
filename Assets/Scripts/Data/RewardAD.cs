using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;

public class RewardAD : MonoBehaviour
{
    private void Show()
    {
        //pause
        VideoAd.Show(onRewardedCallback:Reward, onCloseCallback:null, onErrorCallback:null);
    }

    private void Reward()
    {
        
    }
}
