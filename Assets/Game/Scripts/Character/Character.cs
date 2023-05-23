using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEditor.LightingExplorerTableColumn;
using static UnityEngine.GraphicsBuffer;

public class Character : GameUnit
{
    [SerializeField] public Animator animator;
    [SerializeField] GameObject mask;
    [SerializeField] public List<Character> listTarget = new List<Character>();
    [SerializeField] public WeaponType weaponType;
    [SerializeField] public Transform weaponTransform;
    [SerializeField] public int indexWeapon = 0;
    private GameObject modelWeapon;
    public GameObject _modelPant;

    private GameObject hatType;
    public Transform hatTransform;

    private GameObject accessoryType;
    public Transform accessoryTF;

    public float attRange;

    string currentAnim;
    private float multiplier = 1.0f;
    public const float MAX_SIZE = 1.5f;
    public const float MIN_SIZE = 1f;
    public float size = 1f;
    public bool isDead { get; set; }

   

    public virtual void OnInit()
    {
        isDead = false;
    }
    public void OnEnableWeapon(WeaponType weaponType)
    {
        if (modelWeapon != null)
        {
            Destroy(modelWeapon);
        }
        if (weaponType._weapon != null)
        {
            modelWeapon = Instantiate(weaponType._weapon);
            modelWeapon.transform.SetParent(weaponTransform, false);
        }

    }
    public void SetActiveWeapon()
    {
        modelWeapon.SetActive(false);
        Invoke(nameof(IsHaveWeapon), 1f);
    }
    public void IsHaveWeapon()
    {
        modelWeapon.SetActive(true);
    }

    public Vector3 GetDirectionTaget()
    {
        Vector3 closestTarget = listTarget[0].transform.position;
        float closestDistance = Vector3.Distance(TF.position, closestTarget);
        for (int i = 0; i < listTarget.Count; i++)
        {
            float distance = Vector3.Distance(TF.position, listTarget[i].transform.position);
            if (distance < closestDistance)
            {
                closestTarget = listTarget[i].transform.position;
                closestDistance = distance;
            }
        }
        Vector3 directionToTarget = closestTarget - TF.position;
        Vector3 normalizedDirection = directionToTarget.normalized;
        return normalizedDirection;
    }
    public Vector3 GetClosestTarget()
    {
        Vector3 closestTarget = new Vector3();
        if (listTarget.Count > 0)
        {
            closestTarget = listTarget[0].transform.position;
        }
        else return closestTarget;
        float closestDistance = Vector3.Distance(TF.position, closestTarget);
        for (int i = 0; i < listTarget.Count; i++)
        {
            float distance = Vector3.Distance(TF.position, listTarget[i].transform.position);
            if (distance < closestDistance)
            {
                closestTarget = listTarget[i].transform.position;
                closestDistance = distance;
            }
        }
        return closestTarget;
    }
    public void SetMask(bool active)
    {
        mask.SetActive(active);
    }
    public virtual void OnAttack()
    {
        LookBot();
        ChangeAnim(Constant.ANIM_ATTACK);
        SetActiveWeapon();
    }
    public virtual void AddTarget(Character character)
    {
        this.listTarget.Add(character);

    }
    public virtual void RemoveTarget(Character character)
    {
        this.listTarget.Remove(character);
    }
    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            animator.ResetTrigger(animName);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }
    }
    public void LookBot()
    {
        if (listTarget.Count > 0)
        {
            Vector3 direction = GetDirectionTaget();
            direction.y = 0f;
            TF.rotation = Quaternion.LookRotation(direction);
        }
    }
    public virtual void ThrowWeapon()
    {
        Vector3 target = GetClosestTarget();
        if (this.listTarget.Count > 0)
        {
            WeaponController weapon = SimplePool.Spawn<WeaponController>(weaponType.poolTypeWeapon, weaponTransform.position, Quaternion.identity);
            weapon.WeaponInit(this, target);
        }
    }
    public virtual void OnDead()
    {
        ChangeAnim(Constant.ANIM_DEAD);
    }
    public void ResetAnim()
    {
        ChangeAnim("");
    }
    public float GetMultiplier()
    {
        return multiplier;
    }
    public virtual void OnDespawn()
    {

    }
    public virtual void ChangeHair(int index)
    {
        if (hatType != null)
        {
            Destroy(hatType);
        }
        hatType = Instantiate(ShopManage.Ins._hair[index], Vector3.zero, Quaternion.identity);
        hatType.transform.SetParent(hatTransform, false);
    }
    public virtual void ChangePant(int index)
    {
        _modelPant.transform.GetComponent<Renderer>().material = ShopManage.Ins._pantTypes[index];
    }
    public virtual void ChangeAccessory(int index)
    {

        if (accessoryType != null)
        {
            Destroy(accessoryType);
        }
        accessoryType = Instantiate(ShopManage.Ins._accessory[index], Vector3.zero, Quaternion.identity);
        accessoryType.transform.SetParent(accessoryTF, false);
    }
    public virtual void ChangeSkin(int index)
    {

    }
    public virtual void ChangeSize(float size)
    {
        size = Mathf.Clamp(size, MIN_SIZE, MAX_SIZE);
        this.size = size;
        TF.localScale = size * Vector3.one;
    }
    public virtual void ChangeWeapon(int index)
    {

    }
    public virtual void ChangeSkin()
    {

    }
}