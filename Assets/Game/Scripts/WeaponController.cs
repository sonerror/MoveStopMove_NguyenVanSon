using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : GameUnit
{
    [SerializeField] protected float weaponMoveSpeed = 5f;
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
    private void WeaponStop()
    {
        weaponMoveSpeed = 0f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constant.TAG_CUP))
        {
            WeaponStop();
        }
        if (other.CompareTag(Constant.TAG_CHARACTER) && other.GetComponent<Character>() != _character)
        {
            OnDespawn();
            other.GetComponent<Character>().isDead = true;
            _character.RemoveTarget(other.GetComponent<Character>());
            _isUpdatePosition = false;
            if (_character is Player)
            {
                LevelManager.Ins.player.ChangeSize(LevelManager.Ins.player.size + 0.1f);
                weaponMoveSpeed += 0.25f;
                LevelManager.Ins.player.moveSpeed += 0.1f;
                SoundManager.Ins.SfxPlay(Constant.SOUND_SIZE_UP);
                UIManager.Ins.numberLevel += 1;
            }
            else
            {
                _character.ChangeSize(_character.size + 0.1f);
            }
        }
    }
}