using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponsManager : MonoBehaviour {

    [SerializeField]
    GameObject[] Guns;

    [SerializeField]
    GameObject StabbingZone;

    bool ready = false;

    public int currentWeapon = 0;

    public bool WeaponOut = false; //For melee

    PlayerInventory Inventory;

    public WEP_Gun CurrentGunInfo;

    public int ClipAmount = 0;

    PlayerAnimations anim;
   
    // Use this for initialization
    void Start()
    {
        Inventory = GetComponent<PlayerInventory>();
        anim = GetComponent<PlayerAnimations>();
        FullAmmo();
        SelectWeapon(0);
    }

    public void HideWeapon(bool active)
    {
        WeaponOut = active;
        if (active)
        {
            Guns[currentWeapon].SetActive(true);
        }
        else
        {
            Guns[currentWeapon].SetActive(false);
        }
    }

    public void SelectWeapon(int GunIndex)
    {
        Inventory.Add(0, Guns[currentWeapon].GetComponent<WEP_Gun>().BulletType, ClipAmount); //Put Clip ammo back in inventory
        Guns[currentWeapon].SetActive(false);  //Make gun invisable
        ClipAmount = 0; //Clear clip
        Guns[currentWeapon].SetActive(true); //Set new weapon visable
        CurrentGunInfo = Guns[currentWeapon].GetComponent<WEP_Gun>(); //Set the current WEP_Gun info up
    }

    public void NextWeapon()
    {
        GetNextWeaponID(); //Increment Weapon index
        SelectWeapon(currentWeapon);
    }

    public void ReloadClip()
    {
        StartCoroutine(Reload());
    }

    public void Shoot()
    {
        if (ready)
        {
            if (Guns[currentWeapon].GetComponent<WEP_Gun>().MuzzelFlash != null) //if we have a muzzel flash setup
            {
                Guns[currentWeapon].GetComponent<WEP_Gun>().MuzzelFlash.SetActive(true); //Turn on muzzel Flash
            }

            if (Guns[currentWeapon].GetComponent<WEP_Gun>().BulletType != 6) //Not melee
            {
                anim.Shooting(); // Start Shooting Animation
                StartCoroutine(WaitForGun());
                ClipAmount--;
                //Initiate Bullet Object
                GameObject bullet = Guns[currentWeapon].GetComponent<WEP_Gun>().Bullet;
                //Setup bullet Variables   
                bullet.GetComponent<I_Bullet>().BulletVelocity = Guns[currentWeapon].GetComponent<WEP_Gun>()._BulletVelocity;
                bullet.GetComponent<I_Bullet>().Damage = Guns[currentWeapon].GetComponent<WEP_Gun>()._Damage;
                bullet.GetComponent<I_Bullet>().DestroyTime = Guns[currentWeapon].GetComponent<WEP_Gun>()._BulletDestroyTime;
                //Makes visable ammo disappear until reload
                Guns[currentWeapon].GetComponent<WEP_Gun>().BulletSpawn.SetActive(false);
                //Spawn Bullet
                Instantiate(bullet, Guns[currentWeapon].GetComponent<WEP_Gun>().BulletSpawn.transform.position, Guns[currentWeapon].GetComponent<WEP_Gun>().BulletSpawn.transform.rotation);
            }
            else
            {
                StabbingZone.SetActive(true);
            }
            
            //Start gun wait
            StartCoroutine(NextShot());
        }
    }

    IEnumerator WaitForGun()
    {
        yield return new WaitForSeconds(.5f); //Wait for gun to end
        anim.Idle();
        StopCoroutine(WaitForGun());
    }
    IEnumerator NextShot()
    {
        ready = false; //Stop from firing.

        
        

        if (Guns[currentWeapon].GetComponent<WEP_Gun>().MuzzelFlash != null) //if i have the muzzel Flash
        {

            yield return new WaitForSeconds(.15f); //Wait for flash to start before end
            Guns[currentWeapon].GetComponent<WEP_Gun>().MuzzelFlash.SetActive(false); //turn off muzzel flash
        }

        yield return new WaitForSeconds(Guns[currentWeapon].GetComponent<WEP_Gun>().BulletDelay); //Bullet Delay wait

        if (Guns[currentWeapon].GetComponent<WEP_Gun>().BulletType != 6) //Not melee
        {
            if (ClipAmount <= 0)
            {
                ReloadClip(); //Reload when trying to shoot with no ammo
            }
            else
            {
                Guns[currentWeapon].GetComponent<WEP_Gun>().BulletSpawn.SetActive(true); //Show Exposed Dummy Ammo
                ready = true;
            }
        }
        else
        {
            //Stab animation
            StabbingZone.SetActive(false);
            ready = true;
        }
        //Set ready to shoot
        StopCoroutine(NextShot());
    }

    IEnumerator Reload()
    {
        if (Guns[currentWeapon].GetComponent<WEP_Gun>().BulletType != 6)
        {
            ready = false; //Stop from firing.

            Guns[currentWeapon].GetComponent<WEP_Gun>().BulletSpawn.SetActive(false); //Hide Exposed Dummy Ammo

            yield return new WaitForSeconds(Guns[currentWeapon].GetComponent<WEP_Gun>().ReloadDelay); //Wait

            if (ClipAmount != Guns[currentWeapon].GetComponent<WEP_Gun>().ClipSize) //Check to see if Clip is full
            {
                if (ClipAmount > 0) //If the clip isn't empty
                {
                    if (CheckInventoryAmmo(Guns[currentWeapon].GetComponent<WEP_Gun>().BulletType, Guns[currentWeapon].GetComponent<WEP_Gun>().ClipSize - ClipAmount)) //First see if we have enough ammo 
                    {
                        TakeAmmoFromInventory(Guns[currentWeapon].GetComponent<WEP_Gun>().BulletType, Guns[currentWeapon].GetComponent<WEP_Gun>().ClipSize - ClipAmount); //Fill clip Take the ammo from the inventory if we have enough minus what I already have
                        ClipAmount = Guns[currentWeapon].GetComponent<WEP_Gun>().ClipSize; //Fill the clip
                    }
                    else
                    {
                        ClipAmount = TakeAmmoFromInventory(Guns[currentWeapon].GetComponent<WEP_Gun>().BulletType, Inventory.AmmoAMTs[Guns[currentWeapon].GetComponent<WEP_Gun>().BulletType]);  //Takes whats left from inventory
                    }
                }
                else
                {
                    ClipAmount = TakeAmmoFromInventory(Guns[currentWeapon].GetComponent<WEP_Gun>().BulletType, Guns[currentWeapon].GetComponent<WEP_Gun>().ClipSize); //If Clip is completely empty try to take an entire clip from the inventory
                }

            }
            if (ClipAmount > 0)
            {
                ready = true; //Set ready to shoot
            }
            Guns[currentWeapon].GetComponent<WEP_Gun>().BulletSpawn.SetActive(true); //Show Exposed Dummy Ammo
        }
        StopCoroutine(Reload()); //Terminate
    }

    private bool CheckInventoryAmmo(int AmmoTypeID, int Amount)
    {
        if (Inventory.AmmoAMTs[AmmoTypeID] >= Amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    //Get next weapon you have in inventory
    private int GetNextWeaponID()
    {
        for(int i = currentWeapon; i < Guns.Length; i++)
        {
            if (Inventory.Weapons[i]) //If you have it 
            {
                currentWeapon = i; //Make ID the current weapon
                return i; //output the ID
            }
        }

        return 0;
    }

    public void FullAmmo()
    {

        for(int i = 0; i < WeaponType.DiffAmountOfAmmoTypes; i++)
        {
            Inventory.Add(0, i, WeaponType.AmmoTypeMAx[i]);
        }

        for(int i = 0; i < WeaponType.DiffAmountOfGuns; i++)
        {
            Inventory.Add(2,i,1);
        }


    }

    //Subtract from inventory
    public int TakeAmmoFromInventory(int AmmoID, int Amount)
    {
        int ammo_now = 0;
        if (Inventory.AmmoAMTs[AmmoID] >= Amount)
        {
            Inventory.AmmoAMTs[AmmoID] -= Amount; // Subtract Ammo
            return Amount;
        }
        else if (Inventory.AmmoAMTs[AmmoID] < Amount && Inventory.AmmoAMTs[AmmoID] > 0)
        {
            ammo_now = Inventory.AmmoAMTs[AmmoID];
            Inventory.AmmoAMTs[AmmoID] = 0; // Set Ammo to zero
            return ammo_now;
        }
        else
        {
            return 0;
        }
    }
}
