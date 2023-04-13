using UnityEngine;
[CreateAssetMenu(menuName ="WeaponType")]
public class WeaponType : ScriptableObject
{
    public GameObject _weapon;
    public WeaponController _weaponPrefab;
    public TypeSpawnWeapon _typeSpawnWeapon;
}
public enum TypeSpawnWeapon
{
    FowardWeapon = 1,
    RotateWeapon = 2,
    Boomerang = 3
}