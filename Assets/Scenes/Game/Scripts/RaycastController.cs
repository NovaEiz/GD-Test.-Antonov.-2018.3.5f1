using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nighday {

public class RaycastController : MonoBehaviour {

	[SerializeField] private BoomBallManager _boomBallManager;

	private int _layerMask;
	
	private void Awake() {
		_layerMask = 1 << LayerMask.NameToLayer("Balls");
	}

	private void Update() {
		if (Input.GetMouseButtonDown(0)) {
			RunRaycast();
		}
	}

	private void RunRaycast() {
		int pointerId = -1;//Левая кнопка мыши на PC
		if (EventSystem.current.IsPointerOverGameObject(pointerId)) {
			return;
		}
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask)) {
			BallEntity ball = hit.transform.gameObject.GetComponent<BallEntity>();
			if (ball != null) {
				_boomBallManager.RunBoom(ball);
			}
		}
	}

}

}