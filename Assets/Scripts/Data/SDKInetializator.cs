using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;

public class SDKInetializator : MonoBehaviour
{
    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif
        yield return YandexGamesSdk.Initialize();
        if(PlayerAccount.IsAuthorized && !PlayerAccount.HasPersonalProfileDataPermission)
            PlayerAccount.RequestPersonalProfileDataPermission();
    }
}
