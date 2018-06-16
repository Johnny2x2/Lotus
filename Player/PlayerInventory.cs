using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int[] AmmoAMTs;
    public int Aids;
    public bool[] Weapons;
    
    void Start()
    {
        AmmoAMTs = Game.current.MainPlayer.ammoamts;
        Weapons = Game.current.MainPlayer.weapons;
    }

    public void Add(int ItemTypeId, int ItemId, int Amount)
    {
        switch (ItemTypeId)
        {
            case 0:
                AmmoAMTs[ItemId] += Amount;
                AmmoAMTs[ItemId] = Mathf.Clamp(AmmoAMTs[ItemId], 0, WeaponType.AmmoTypeMAx[ItemId]);
                break;
            case 1:
                Aids += Amount;
                Aids = Mathf.Clamp(Aids, 0, 10);
                break;
            case 2:
                Weapons[ItemId] = true;
                break;
            default:
                break;
        }
    }

    public void Remove(int ItemTypeId, int ItemId, int Amount)
    {
        switch (ItemTypeId)
        {
            case 0:
                AmmoAMTs[ItemId] -= Amount;
                AmmoAMTs[ItemId] = Mathf.Clamp(AmmoAMTs[ItemId], 0, WeaponType.AmmoTypeMAx[ItemId]);
                break;
            case 1:
                Aids -= Amount;
                Aids = Mathf.Clamp(Aids, 0, 10);
                break;
            case 2:
                Weapons[ItemId] = false;
                break;
            default:
                break;
        }
    }

    public void CompileSaveinfo()
    {
        Game.current.MainPlayer.ammoamts = AmmoAMTs;
        Game.current.MainPlayer.weapons = Weapons;
    }

    public void OverrideInfo()
    {
        AmmoAMTs = Game.current.MainPlayer.ammoamts;
         Weapons = Game.current.MainPlayer.weapons;
    }
}
