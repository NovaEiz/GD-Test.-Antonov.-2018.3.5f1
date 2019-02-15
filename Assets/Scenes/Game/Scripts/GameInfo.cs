using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Nighday {

public class GameInfo : MonoBehaviour {

	[SerializeField] private Text _timeLeftText;
	[SerializeField] private Text _pointsText;
	[SerializeField] private Text _plusPointsText;

	public void SetTimeLeft(float timeLeft) {
		_timeLeftText.text = "" + Convert.ToInt32(timeLeft);
	}
	public void SetPoints(int points) {
		_pointsText.text   = "" + points;
	}

	public void AddPoints(int points) {
		if (points == 0) {
			_plusPointsText.gameObject.SetActive(false);
			return;
		}
		_plusPointsText.gameObject.SetActive(true);

		_plusPointsText.text = "+" + points;
	}

}

}