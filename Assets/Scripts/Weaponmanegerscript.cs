using System;
using UnityEngine;
using UnityEngine.UI;
public class Weaponmanegerscript : MonoBehaviour
{
    public GameObject[] weapons;
    public Image[] slotHighlights;
    private int currentweapon = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SelectWeapon(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectWeapon(2);
        }
    }
    void SelectWeapon(int index)
    {
        if(index >= weapons.Length) { return; }
        for (int i = 0; i < weapons.Length; i++)
            weapons[i].SetActive(false);
        weapons[index].SetActive(true);
        currentweapon = index;
        for (int i = 0; i < slotHighlights.Length; i++)
            slotHighlights[i].gameObject.SetActive(false);

        slotHighlights[index].gameObject.SetActive(true);
    }
}
