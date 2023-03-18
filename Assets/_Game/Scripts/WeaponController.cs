using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : GameUnit
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Player _player;

    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_BOT) || other.CompareTag(Constant.TAG_BLOCK))
        {
            OnDespawn();
        }
    }
}