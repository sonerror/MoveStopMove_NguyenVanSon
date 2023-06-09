using System.Collections;
using System.Collections.Generic;
using UIExample;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
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
        Shop = 2,
    }
    [SerializeField] private float _lerpTime = 10f;
    [SerializeField] private CameraData _mainMenuData;
    [SerializeField] private CameraData _gamePlayData;
    [SerializeField] private CameraData _shopData;

    [SerializeField] private Player _player;
    private State _state;
    private Vector3 _targetOffset;
    private Vector3 _offset;
    private Vector3 _targetEulerAngles;
    private Vector3 _eulerAngles;
    [SerializeField] Vector3 offsetMax;
    [SerializeField] Vector3 offsetMin;
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
    public void SetRateOffset(float rate)
    {
        _targetOffset = Vector3.Lerp(offsetMin, offsetMax, rate);
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
            case State.Shop:
                _targetOffset = _shopData._offSetStruct;
                _targetEulerAngles = _shopData._eulerAnglesStruct;
                break;
        }
    }
}
