using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WEP_Gun : MonoBehaviour
{

    public GameObject Bullet;

    public GameObject BulletSpawn;

    public GameObject MuzzelFlash;

    public float BulletDelay = .02f;

    public float ReloadDelay = 2;

    public int ClipSize = 30;

    public int BulletType = 0;

    public float _BulletDestroyTime = 1f; //Bullet time alive

    public float _BulletVelocity = 100f; //Bullet sppd

    public int _Damage = 30; //Bullet Damage On Hit

    public int WeaponID = 0;

}
