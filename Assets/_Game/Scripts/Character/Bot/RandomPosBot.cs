using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosBot : MonoBehaviour
{
    [SerializeField] private Transform _transformPlayer;
    [SerializeField] private Transform _transformBot;
    [SerializeField] private float _about;

    private void Start()
    {
        RandomPositonBot();
    }
    void RandomPositonBot()
    {
        float RandomX = Random.Range(-_about, _about);
        float RandomZ = Random.Range(-_about, _about);

        _transformBot.position = new Vector3(RandomX, 0, RandomZ);
        while(Vector3.Distance(_transformPlayer.position,_transformBot.position) < 5f)
        {
            RandomX = Random.Range(-_about, _about);
            RandomZ = Random.Range(-_about, _about);
            _transformBot.position = new Vector3(RandomX, 0, RandomZ);
        }
    }
}
