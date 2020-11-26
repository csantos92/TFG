using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public int activeWeapon;
    private List<GameObject> weapons;

    public List<GameObject> GetAllWeapons()
    {
        return weapons;
    }

    public WeaponDamage GetWeaponAt(int pos)
    {
        return weapons[pos].GetComponent<WeaponDamage>();
    }
    // Start is called before the first frame update
    void Start()
    {
        activeWeapon = 0;
        weapons = new List<GameObject>();
        foreach(Transform weapon in transform)
        {
            weapons.Add(weapon.gameObject);
        }

        for(int i = 0; i < weapons.Count; i++)
        {
            weapons[i].SetActive(i == activeWeapon); 
        }
    }

    public void ChangeWeapon(int newWeapon)
    {
        weapons[activeWeapon].SetActive(false);
        weapons[newWeapon].SetActive(true);
        activeWeapon = newWeapon;
    }
}
