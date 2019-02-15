using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

namespace Nighday {

public class CreatorBalls : MonoBehaviour {

	[SerializeField] private BallEntity _ballPrefab;
	[SerializeField] private Transform _world;
	[SerializeField] private Transform _contentBalls;
	[SerializeField] private Transform _startPosition;

	[Range(0, 10)]
	[SerializeField] private float _minStartScale;
	[Range(0, 10)]
	[SerializeField] private float _maxStartScale;

	private Vector2 _angleRangomOnStartPosition = new Vector2(30, 150);

	private void Start() {
		InitStartPosition();
	}

	public BallEntity CreateBall() {
		var ball = Instantiate(_ballPrefab, _contentBalls.transform, true);
		ball.SetAngle(UnityEngine.Random.Range(_angleRangomOnStartPosition.x, _angleRangomOnStartPosition.y));

		ball.transform.position = GetStartPosition();

		ball.SetColor(GetRandomColor());

		float scale = UnityEngine.Random.Range(_minStartScale, _maxStartScale);
		ball.transform.localScale = new Vector3(scale, scale, scale);
		ball.gameObject.GetComponent<MeshRenderer>().enabled = true;
		return ball;
	}

	private Color GetRandomColor() {
		Color color = new Color(
			UnityEngine.Random.Range(0f, 1f),
			UnityEngine.Random.Range(0f, 1f),
			UnityEngine.Random.Range(0f, 1f)
		);
		return color;
	}

	private void InitStartPosition() {
		Vector3 pos = _world.position;
		pos.y += -_world.localScale.y / 2 - 1;
		_startPosition.position = pos;
	}

	private Vector3 GetStartPosition() {
		float halfWidth = _world.localScale.x / 2;
		halfWidth *= 0.8f;
		
		Vector3 pos = new Vector3(
			_startPosition.position.x + UnityEngine.Random.Range(-halfWidth, halfWidth),
			_startPosition.position.y,
			transform.position.z
		);

		return pos;
	}
	
}

}