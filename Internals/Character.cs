using UnityEngine;
using System.Collections;

[System.Serializable] // You need this part, so that you class can be 'serialized', meaning turned into 1s and 0s. Binary baby!
public class Character {

    
    public int hour;
    public int min;
    public int day;
    public int month;
    public int money;

    public int[] ammoamts;
    public int banaids;
    public bool[] weapons;

    //Add Variables To save on Disk here

    public Character ()
    {

        this.ammoamts = new int[2];
        this.weapons = new bool[2];

        this.banaids = 0;

        //Time Saving
        this.hour = 0;
        this.min = 0;
        this.day = 0;
        this.month = 0;

        //Money Saving
        this.money = 0;
	}

}
