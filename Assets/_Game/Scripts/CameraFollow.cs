using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [System.Serializable]
    public struct CameraData
    {
        public Vector3 _offSetStruct;
        public Vector3 _eulerAnglesStruct;
    }
    public enum State
    {
        MainMenu = 0,
        GamePlay = 1,
    }
    [SerializeField] private float _lerpTime = 10f;
    [SerializeField] private CameraData _mainMenuData;
    [SerializeField] private CameraData _gamePlayData;

    [SerializeField] private Player _player;
    private State _state;
    private Vector3 _targetOffset;
    private Vector3 _offset;
    private Vector3 _targetEulerAngles;
    private Vector3 _eulerAngles;
    private void Start()
    {
        ChangeState(State.MainMenu);
        _offset = _targetOffset;
        _eulerAngles= _targetEulerAngles;
    }
    private void LateUpdate()
    {
        if(_player == null )
        {
            _player = Player.Instance;
        }
        if(_state == State.GamePlay && _targetOffset != _gamePlayData._offSetStruct * _player.GetMultiplier())
        {
            _targetOffset= _gamePlayData._offSetStruct * _player.GetMultiplier();
        }
        _offset = Vector3.Lerp(_offset, _targetOffset, _lerpTime*Time.deltaTime);
        transform.position = _player.transform.position + _offset ;
        _eulerAngles = Vector3.Lerp(_eulerAngles,_targetEulerAngles,_lerpTime*Time.deltaTime);
        transform.rotation = Quaternion.Euler(_eulerAngles);
    }
    public void ChangeState(State _state)
    {
        this._state = _state;
        switch (this._state)
        {
            case (State)0:
           
                _targetOffset = _mainMenuData._offSetStruct;
                _targetEulerAngles = _mainMenuData._eulerAnglesStruct;
                break;
            case State.GamePlay:
                _targetOffset = _gamePlayData._offSetStruct;
                _targetEulerAngles = _gamePlayData._eulerAnglesStruct;
                break;
        }
    }
}
