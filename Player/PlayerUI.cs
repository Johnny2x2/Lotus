using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    GameObject playerui;

    //User Specific UI
    [SerializeField]
    Slider HealthSlider;

    [SerializeField]
    Text AmmoText;

    //Not implemented yet
    [SerializeField]
    Image GunIcon;


    public void UpdatePlayerUI(int hp, int clipAmount, int AmmoLeft)
    {
        HealthSlider.value = hp;
        AmmoText.text = clipAmount.ToString() + "/" + AmmoLeft.ToString();
    }
}
