using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Nighday {

public class GameStartPanel : MonoBehaviour {

	[SerializeField] private GameInfo _gameInfo;
	[SerializeField] private Text _startButtonText;

	private void OnEnable() {
		if (GameManager.Instance == null || GameManager.Instance.AmountGames == 0) {
			_gameInfo.gameObject.SetActive(false);
			_startButtonText.text = "Start game";
		} else {
			_startButtonText.text = "Restart game";
		}
	}


}

}