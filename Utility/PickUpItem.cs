using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour {

    [SerializeField]
    int ItemtypeID = 0;

    [SerializeField]
    int ItemID = 0;

    int amount = 1;

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerInventory>() != null)
        {
            other.GetComponent<PlayerInventory>().Add(ItemtypeID, ItemID, amount);
        }
    }
}
