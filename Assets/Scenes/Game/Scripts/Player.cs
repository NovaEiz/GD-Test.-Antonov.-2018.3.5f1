using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nighday {

public class Player : MonoBehaviour {

    [Header("Debug fields")]
    [SerializeField] private int _points;

    public int Points => _points;
    
    public void ChangePoints(int value) {
        _points += value;

        RunOnChangedPoints(value);
    }

    public void ClearPoints() {
        _points = 0;
            
        RunOnChangedPoints(0);
    }
    
#region Events

    private Action<int> _onChangedPoints;

    public void AddOnChangedPoints(Action<int> callback) {
        _onChangedPoints += callback;
    }
    public void RemoveOnChangedPoints(Action<int> callback) {
        if (_onChangedPoints != null) {
            _onChangedPoints += callback;
        }
    }
    private void RunOnChangedPoints(int addPoints) {
        if (_onChangedPoints != null) {
            _onChangedPoints(addPoints);
        }
    }

#endregion

}

}