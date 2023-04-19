using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WeaponManage : MonoBehaviour
{
    public int money = 100;
    public int currentWeaponIndex;
    public GameObject[] weaponModels;
    public WeaponBluePrint[] _weapons;
    public Button _buttonBuy;
    public Text _textCost;
    private void Awake()
    {
    }
    private void Start()
    {
        foreach(WeaponBluePrint weapon in _weapons)
        {
            if(weapon._price == 0)
            {
                weapon._isUnlocked = true;

            }
            else
            {
                weapon._isUnlocked = PlayerPrefs.GetInt(weapon._name,0) == 0 ? false:true;
            }
        }
        currentWeaponIndex = PlayerPrefs.GetInt("SelectWeapon", 0);
        foreach(GameObject car in weaponModels)
        {
            car.SetActive(false);
        }
        weaponModels[currentWeaponIndex].SetActive(true);
    }
    private void Update()
    {
        UpdateUI();
        _textCost.text = money.ToString();
        PlayerPrefs.SetInt("NumberOfCost", money);
    }
    public void ChangeNext()
    {
        weaponModels[currentWeaponIndex].SetActive(false);
        currentWeaponIndex++;
        if(currentWeaponIndex == weaponModels.Length)
        {
            currentWeaponIndex = 0;
        }

        weaponModels[currentWeaponIndex].SetActive(true);
        WeaponBluePrint c = _weapons[currentWeaponIndex];
        if(!c._isUnlocked)
        {
            return;
        }
        PlayerPrefs.SetInt("SelectWeapon", currentWeaponIndex);
    }
    public void ChangeBack()
    {
        weaponModels[currentWeaponIndex].SetActive(false);
        currentWeaponIndex--;
        if (currentWeaponIndex == -1)
        {
            currentWeaponIndex = weaponModels.Length - 1;
        }
        weaponModels[currentWeaponIndex].SetActive(true);
        WeaponBluePrint c = _weapons[currentWeaponIndex];
        if (!c._isUnlocked)
        {
            return;
        }
        PlayerPrefs.SetInt("SelectWeapon", currentWeaponIndex);
    }
    public void UnLockWeapon()
    {
        WeaponBluePrint c = _weapons[currentWeaponIndex];
        PlayerPrefs.SetInt(c._name, 1);
        PlayerPrefs.SetInt("SelectWeapon", currentWeaponIndex);
        c._isUnlocked = true;
        PlayerPrefs.SetInt("NumberOfCost", PlayerPrefs.GetInt("NumberOfCost",0) - c._price);

    }
    private void UpdateUI()
    {
        WeaponBluePrint c = _weapons[currentWeaponIndex];
        if(c._isUnlocked)
        {
            _buttonBuy.gameObject.SetActive(false);
        }
        else
        {
            _buttonBuy.gameObject.SetActive(true);
            _buttonBuy.GetComponentInChildren<TextMeshProUGUI>().text = "Buy - " + c._price;
            if(c._price < PlayerPrefs.GetInt("NumberOfCost",0))
            {
                _buttonBuy.interactable = true;
            }
            else
            {
                _buttonBuy.interactable = false;
            }
        }
    }
}
