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
    public Button _useWeapon;
    public Text _textCost;
    private void Awake()
    {
        /*   int oldmoney = PlayerPrefs.GetInt("NumberOfCost", 0);
           _textCost.text = oldmoney.ToString();
           PlayerPrefs.SetInt("NumberOfCost", money);*/
        PlayerPrefs.SetInt("NumberOfCost", money);
        _textCost.text = money.ToString();
    }
    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        foreach (WeaponBluePrint weapon in _weapons)
        {
            if (weapon._price == 0)
            {
                weapon._isUnlocked = true;
                _useWeapon.gameObject.SetActive(true);
            }
            else
            {
                weapon._isUnlocked = PlayerPrefs.GetInt(weapon._name, 0) == 0 ? false : true;
                _useWeapon.gameObject.SetActive(false);
            }
        }
        currentWeaponIndex = PlayerPrefs.GetInt(Constant.SELECT_WEAPON, 0);
        foreach (GameObject weapon in weaponModels)
        {
            weapon.SetActive(false);
        }
        weaponModels[currentWeaponIndex].SetActive(true);
    }
    private void Update()
    {
        UpdateUI();
    }
    public void ChangeNext()
    {
        weaponModels[currentWeaponIndex].SetActive(false);
        currentWeaponIndex++;
        if (currentWeaponIndex == weaponModels.Length)
        {
            currentWeaponIndex = 0;
        }
        weaponModels[currentWeaponIndex].SetActive(true);
        WeaponBluePrint c = _weapons[currentWeaponIndex];
        if (!c._isUnlocked)
        {
            return;
        }
        PlayerPrefs.SetInt(Constant.SELECT_WEAPON, currentWeaponIndex);
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
        PlayerPrefs.SetInt(Constant.SELECT_WEAPON, currentWeaponIndex);
    }
    public void UnLockWeapon()
    {
        WeaponBluePrint c = _weapons[currentWeaponIndex];
        PlayerPrefs.SetInt(c._name, 1);
        PlayerPrefs.SetInt(Constant.SELECT_WEAPON, currentWeaponIndex);
        c._isUnlocked = true;
        PlayerPrefs.SetInt("NumberOfCost", PlayerPrefs.GetInt("NumberOfCost", money) - c._price);
       int moneyNew = PlayerPrefs.GetInt("NumberOfCost", PlayerPrefs.GetInt("NumberOfCost", money) - c._price);
        PlayerPrefs.Save();
        _textCost.text = moneyNew.ToString();
    }
    public void ButtonSelectWeapon()
    {
        WeaponBluePrint c = _weapons[currentWeaponIndex];
        Debug.Log(c._index);
        LevelManager.instance.player.ChangeWeapon(c._index);
    }
    private void UpdateUI()
    {
        WeaponBluePrint c = _weapons[currentWeaponIndex];
        if (c._isUnlocked)
        {
            _buttonBuy.gameObject.SetActive(false);
            _useWeapon.gameObject.SetActive(true);
        }
        else
        {
            _buttonBuy.gameObject.SetActive(true);
            _buttonBuy.GetComponentInChildren<TextMeshProUGUI>().text = "Buy - " + c._price;
            if (c._price < PlayerPrefs.GetInt("NumberOfCost", 0))
            {
                _buttonBuy.interactable = true;
                _useWeapon.gameObject.SetActive(false);
            }
            else
            {
                _buttonBuy.interactable = false;
                _useWeapon.gameObject.SetActive(false);
            }
        }
    }
}
