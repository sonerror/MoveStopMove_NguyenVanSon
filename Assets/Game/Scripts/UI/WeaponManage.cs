using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;

public class WeaponManage : MonoBehaviour
{
    public int currentWeaponIndex;
    public GameObject[] weaponModels;
    public WeaponBluePrint[] _weapons;
    public Button _buttonBuy;
    public Button _useWeapon;
    public Text _textNameWeapon;
    public Transform _transform;

    private void Start()
    {
        currentWeaponIndex = PlayerPrefs.GetInt(Pref.SELECT_WEAPON, 0);
        foreach (GameObject weapon in weaponModels)
        {
            weapon.SetActive(false);
        }
        weaponModels[currentWeaponIndex].SetActive(true);
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
    }
    private void Update()
    {
        UpdateUI();
    }
    public void ChangeNext()
    {
        SoundManager.Ins.SfxPlay(Constant.SOUND_BUTTON);
        weaponModels[currentWeaponIndex].SetActive(false);
        currentWeaponIndex++;
        if (currentWeaponIndex == weaponModels.Length)
        {
            currentWeaponIndex = 0;
        }
        weaponModels[currentWeaponIndex].gameObject.SetActive(true);
        WeaponBluePrint c = _weapons[currentWeaponIndex];
        if (!c._isUnlocked)
        {
            return;
        }
        PlayerPrefs.SetInt(Pref.SELECT_WEAPON, currentWeaponIndex);
    }
    public void ChangeBack()
    {
        SoundManager.Ins.SfxPlay(Constant.SOUND_BUTTON);
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
        PlayerPrefs.SetInt(Pref.SELECT_WEAPON, currentWeaponIndex);
    }
    public void UnLockWeapon()
    {
        SoundManager.Ins.SfxPlay(Constant.SOUND_BUTTON);
        WeaponBluePrint c = _weapons[currentWeaponIndex];
        PlayerPrefs.SetInt(c._name, 1);
        PlayerPrefs.SetInt(Pref.SELECT_WEAPON, currentWeaponIndex);
        c._isUnlocked = true;
        Pref.Cost -= c._price;
    }
    public void ButtonSelectWeapon()
    {
        SoundManager.Ins.SfxPlay(Constant.SOUND_BUTTON);
        WeaponBluePrint c = _weapons[currentWeaponIndex];
        Debug.Log(c._index);
        LevelManager.Ins.player.ChangeWeapon(c._index);
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
            if (c._price <= Pref.Cost)
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
        _textNameWeapon.text = c._name;
    }
}
