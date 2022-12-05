using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTriggerObserver2D : MonoBehaviour
{
    public event Action BoughtTwoBranches;

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
