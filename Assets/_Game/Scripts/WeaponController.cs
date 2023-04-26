using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : GameUnit
{
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected Character _character;
    public bool _isUpdatePosition = false;

    public virtual void WeaponInit(Character character, Vector3 target)
    {
        this._character = character;
        TF.forward = (target - TF.position).normalized;
    }
    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
    private void OnTriggerEnter(Collider other)
    {
        //SoundManager.Ins.SfxPlay(Constant.SOUND_COLLIDE);
        if (other.CompareTag(Constant.TAG_CHARACTER) && other.GetComponent<Character>() != _character)
        {
            OnDespawn();
            other.GetComponent<Character>()._isDead = true;
            _character.RemoveTarget(other.GetComponent<Character>());
            _isUpdatePosition = false;
        }
    }
}