using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRayCasting : MonoBehaviour {

    public GameObject target;

    Ray ray;
    RaycastHit hit;


    public string ShootRayCast(float dist)
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

       if( Physics.Raycast(ray, out hit, dist))
        {
            if (!hit.collider.CompareTag("Untagged"))
            {
                target = hit.collider.gameObject;
                return target.tag;
            }
            return "Untagged";
        }

        return "None";
    }

}
