using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager Playercontrol; //Do Not delete Controll
    //Gather Used Classes
    PlayerMovement playermover;
    PlayerWeaponsManager playercombat;
    PlayerRayCasting Target;
    PlayerInventory Inventory;
    PlayerUI ui;
    Health hp;

    void Awake()
    {
        //Keep control going always run GameManager
        if (Playercontrol == null)
        {
            DontDestroyOnLoad(gameObject);
            Playercontrol = this;
        }
        else if (Playercontrol != this)
        {
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Start ()
    {
        playermover = GetComponent<PlayerMovement>();
        playercombat = GetComponent<PlayerWeaponsManager>();
        Target = GetComponent<PlayerRayCasting>();
        Inventory = GetComponent<PlayerInventory>();
        ui = GetComponent<PlayerUI>();
        hp = GetComponent<Health>();

    }


    // Update is called once per frame
    void Update()
    {
        ui.UpdatePlayerUI(hp.CurrentHealth,playercombat.ClipAmount,Inventory.AmmoAMTs[playercombat.CurrentGunInfo.BulletType]);
        ProcessInputs();
    }


    void ProcessInputs()
    {
        //Movement While normal
        float Horizontal = Input.GetAxisRaw("Horizontal"); // Get A, D key
        float Vertical = Input.GetAxisRaw("Vertical"); // Get W, S key
        bool Sprint = Input.GetKey(KeyCode.LeftShift); //Get left shift key
        playermover.MoveCharacter(Horizontal, Vertical, Sprint); //Send to Movement Script

        //Input while normal
        bool Fire = Input.GetMouseButton(0); //Get Right Mouse Click
        bool Use = Input.GetKeyDown(KeyCode.F); //Get F key
        bool HideWep = Input.GetKeyDown(KeyCode.X); //Get x key
        bool NextWep = Input.GetKeyDown(KeyCode.Q);  //Get Q key

        bool ReLoad = Input.GetKeyDown(KeyCode.R); //Get R key

        //Everything in this if , else if section Will only trigger one at a time 
        if (Fire) // Shoot ( Right Mouse Click )
        {
            //Check if I have a weapons enabled
            if (playercombat.enabled) 
            {
                playercombat.Shoot(); // Then shoot with the WeaponManager
            }
        }
        else if (Use) //Clicking E for now will debug the output of the selected object PlayerRaycast will trigger object
        {
            string Targeting;
            Targeting = Target.ShootRayCast(100f);
            Debug.Log(Targeting);
        }
        else if (HideWep) //Press X to put weapon away
        {
            if (playercombat.enabled) //Basic Toggle WeaponManager Enabled disabled
            {
                playercombat.WeaponOut = false;
                playercombat.HideWeapon(playercombat.WeaponOut); //Put current weapon away
                playercombat.enabled = false; //Disable weapons
            }
            else
            {
                playercombat.enabled = true; //Enable it all back
                playercombat.WeaponOut = true;
                playercombat.HideWeapon(playercombat.WeaponOut);
            }
        }
        else if (NextWep) //Change weapon with Q
        {
            if (playercombat.enabled)
            {
                playercombat.NextWeapon(); 
            }
        }
        else if (ReLoad) //Reload with R
        {
            if (playercombat.enabled)
            {
                playercombat.ReloadClip(); 
            }
        }

    }

    public void CompileSaveInformation()
    {
        Inventory.CompileSaveinfo();
    }

    void OnDeath()
    {

    }
}
