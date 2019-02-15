using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nighday {

public class StretchObjectToCamera : MonoBehaviour {
	
	[SerializeField] private Camera _camera;
	[SerializeField] private float _distance = 10;
	[SerializeField] private Transform _contentRealSize;
	
	private float _distanceIsSet;
	
	private void Initialize() {
		Vector2 scaleFactor = new Vector2(1, 1);
		
		var cam  = _camera;
		var ray1 = cam.ViewportPointToRay(new Vector3(0, 0));
		var ray2 = cam.ViewportPointToRay(new Vector3(1, 0));
		var ray3 = cam.ViewportPointToRay(new Vector3(1, 1));
		var ray4 = cam.ViewportPointToRay(new Vector3(0, 1));

		var p1 = ray1.origin + ray1.direction * _distance;
		var p2 = ray2.origin + ray2.direction * _distance;
		var p3 = ray3.origin + ray3.direction * _distance;
		var p4 = ray4.origin + ray4.direction * _distance;

		var scaleX = (p1 - p2).magnitude;
		var scaleY = (p1 - p4).magnitude;
            
		if (scaleFactor.x <= float.Epsilon) {
			scaleFactor.x = 1;
		}
		if (scaleFactor.y <= float.Epsilon) {
			scaleFactor.y = 1;
		}

		transform.position   = (p1 + p3) / 2;
		transform.localScale = new Vector3(scaleX/scaleFactor.x, scaleY/scaleFactor.y, 1);

		Quaternion quaternionCamera = cam.transform.rotation;
		//quaternionCamera.y -= 180;
		transform.rotation = quaternionCamera;

		_distanceIsSet = _distance;

		if (_contentRealSize != null) {
			Vector3 realScale = new Vector3(1f/transform.localScale.x, 1f/transform.localScale.y, 1);
			_contentRealSize.localScale = realScale;
		}
	}

	private void Awake() {
		Initialize();
	}

	private void Update() {
		if (_distanceIsSet != _distance) {
			Initialize();
		}
	}

}

}
