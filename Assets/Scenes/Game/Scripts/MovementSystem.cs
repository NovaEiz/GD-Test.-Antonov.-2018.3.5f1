using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Entities;
using Unity.Collections;

namespace Nighday {

public class MovementSystem : MonoBehaviour {

    [SerializeField] private Transform _world;
    [SerializeField] private float _startSpeed = 1f;
    [SerializeField] private float _speedByTimeFactor = 1f;
    [SerializeField] private float _speedByScaleFactor = 1f;
    
    private List<BallEntity> _transforms;
    public void Add(BallEntity value) {
        _transforms.Add(value);
    }
    public void Remove(BallEntity value) {
        if (_transforms.Contains(value)) {
            _transforms.Remove(value);
        }
    }
    

    private void Awake() {
        _transforms = new List<BallEntity>();
    }

    private void Update() {
        foreach (BallEntity item in _transforms) {
            Execute(item);
        }
    }

    private float GetSpeedByScale(float scale) {
        float res = 1;
        
        res *= (1 - (GameManager.Instance.GetTimeLeftGame() / GameManager.Instance.GetMaxPlayTime())) * _speedByTimeFactor;
        res /= _speedByScaleFactor * scale;

        res += _startSpeed;

        return res;
    }

    private void Execute(BallEntity ball) {
        float rad = ball.Angle * Mathf.Deg2Rad;
        float distance = 1;
        Vector3 offset = new Vector3(
            (float)(Math.Cos(rad) * distance),
            (float)(Math.Sin(rad) * distance),
            0
        );

        offset *= GetSpeedByScale(ball.transform.localScale.x);
        
        Vector3 newPos = ball.transform.localPosition + (offset * Time.deltaTime);
        ball.transform.localPosition = newPos;
        
        newPos = ball.transform.position;
        newPos.z = transform.position.z;
        ball.transform.position = newPos;

        if (ball.transform.localPosition.x > 0 && ball.transform.localPosition.x + ball.transform.localScale.x/2 > _world.localScale.x/2) {
            //Вылет справа
            if (ball.Angle < 90 || ball.Angle > 270) {
                ball.SetAngle(180 - ball.Angle);
            }
        }
        if (ball.transform.localPosition.x < 0 && ball.transform.localPosition.x - ball.transform.localScale.x/2 < - _world.localScale.x/2) {
            //Вылет слева
            if (ball.Angle > 90 && ball.Angle < 270) {
                ball.SetAngle(180 - ball.Angle);
            }
        }
        if (ball.transform.localPosition.y < 0 && ball.transform.localPosition.y - ball.transform.localScale.y/2 < - _world.localScale.y/2) {
            //Вылет снизу
            if (ball.Angle > 180 && ball.Angle < 360) {
                ball.SetAngle(360 - ball.Angle);
            }
        }
        
        
        
        if (ball.transform.localPosition.y > 0 && ball.transform.localPosition.y + ball.transform.localScale.y/2 > _world.localScale.y/2) {
            //Вылет сверху
            if (ball.Angle > 0 && ball.Angle < 180) {
                if (ball.transform.localPosition.y > 0 && ball.transform.localPosition.y - ball.transform.localScale.y > _world.localScale.y / 2) {
                    Destroy(ball.gameObject);
                }

                //ball.SetAngle(360 - ball.Angle);
            }
        }
    }

    
}

}