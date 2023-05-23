using UnityEngine;
[CreateAssetMenu(menuName ="WeaponType")]
public class WeaponType : ScriptableObject
{
    public GameObject _weapon;
    public WeaponController _weaponPrefab;
    public PoolType poolTypeWeapon;
}