using System;
using UnityEngine;

public class IconSwapUI : MonoBehaviour
{
    [SerializeField] private GameObject activeIcon;
    [SerializeField] private GameObject inactiveIcon;

    public void Activate()
    {
        activeIcon.SetActive(true);
        inactiveIcon.SetActive(false);
    }

    public void Deactivate()
    {
        activeIcon.SetActive(false);
        inactiveIcon.SetActive(true);
    }
}