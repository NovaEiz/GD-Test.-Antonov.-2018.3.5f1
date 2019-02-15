using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nighday {

public class BallEntity : MonoBehaviour {
	
	[Header("Settable fields")]
	[SerializeField] private MeshRenderer _meshRenderer;
	
	[Header("Debug fields")]
	[SerializeField] private float _angle;
	[SerializeField] private Color _color;

	public float Angle => _angle;
	public Color Color => _meshRenderer.material.color;

	public void SetColor(Color value) {
		_meshRenderer.material.color = value;
	}
	
#region Methods set fields
	
	public void SetAngle(float value) {
		if (value > 359) {
			value = value % 360;
		} else if (value < 0) {
			value = (value % 360) + 360;
		}
		_angle = value;
	}
	
#endregion

#region Events
	
	private Action _onDestroy;
	public void AddOnDestroy(Action callback) {
		_onDestroy += callback;
	}
	public void RemoveOnDestroy(Action callback) {
		if (_onDestroy != null) {
			_onDestroy -= callback;
		}
	}
	public void RunOnDestroy() {
		if (_onDestroy != null) {
			_onDestroy();
		}
	}
	
	
#endregion

	private void Update() {
	}
	private void OnDestroy() {
		RunOnDestroy();
	}
	
	

}

}