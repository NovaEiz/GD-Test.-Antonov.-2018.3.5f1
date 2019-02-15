using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nighday {



public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	[Space]
	[Header("Settable fields")]
	[SerializeField] private Player _player;
	[SerializeField] private GameInfo _gameInfo;
	[SerializeField] private GameStartPanel _gameStartPanel;

	[SerializeField] private CreatorBalls _creatorBalls;
	[SerializeField] private GameObject _world;
	[SerializeField] private MovementSystem _movementSystem;

	[Space]
	[SerializeField] private int _maxBallsCounts;
	/// <summary>
	/// Максимальное время игры. in seconds
	/// </summary>
	[SerializeField] private float _maxPlayTime;
	
	[Space]
	[Header("Debug fields")]
	[SerializeField] private int _currentBallsCounts;
	[SerializeField] private float _startTimeGame;
	[SerializeField] private float _timeLeftGame;

	private bool _gameIsProcess;
	public bool IsGameEnd => !_gameIsProcess;

	private List<BallEntity> _balls;

	private int _amountGames;
	public int AmountGames => _amountGames;

	public float GetTimeLeftGame() {
		float res = _maxPlayTime - (Time.time - _startTimeGame);
		if (res < 0) {
			res = 0;
		}
		return res;
	}
	public float GetMaxPlayTime() {
		return _maxPlayTime;
	}

	public Player Player => _player;

	private void Awake() {
		if (Instance != null) {
			Destroy(this);
			return;
		}
		Instance = this;
		
		Player.AddOnChangedPoints((int addPoints) => {
			_gameInfo.SetPoints(Player.Points);
			_gameInfo.AddPoints(addPoints);
		});
	}

	private void StartGame() {
		_gameInfo.gameObject.SetActive(true);
		_amountGames++;
		Player.ClearPoints();
		
		_balls = new List<BallEntity>();
		_startTimeGame = Time.time;
		_currentBallsCounts = 0;
		_gameIsProcess = true;
		StartCoroutine(CreatorIe());
	}
	
	private void Update() {
		if (!IsGameEnd && GetTimeLeftGame() <= float.Epsilon) {
			_gameIsProcess = false;
			_gameStartPanel.gameObject.SetActive(true);
		}
		if (IsGameEnd) {
			return;
		}
		
		_gameInfo.SetTimeLeft(GetTimeLeftGame());
	}

	private IEnumerator CreatorIe() {
		float wait = 0.2f;
		while (!IsGameEnd) {
			if (_currentBallsCounts < _maxBallsCounts) {
				var ball = CreateBall();
				_currentBallsCounts++;
				yield return new WaitForSeconds(wait);
				continue;
			}
			yield return null;
		}
	}

	private BallEntity CreateBall() {
		var ball = _creatorBalls.CreateBall();
		ball.AddOnDestroy(() => {
			_currentBallsCounts--;
			_movementSystem.Remove(ball);
			_balls.Remove(ball);
		});
		_movementSystem.Add(ball);
		_balls.Add(ball);
		return ball;
	}

	private void BallOnDestroy() {
		_currentBallsCounts--;
	}
	
	public void RestartGame() {
		DestroyAllBalls();
		StartGame();
		_gameStartPanel.gameObject.SetActive(false);
	}

	private void DestroyAllBalls() {
		if (_balls == null) {
			return;
		}
		foreach (var item in _balls) {
			if (item != null) {
				Destroy(item.gameObject);
			}
		}
		_balls.Clear();
	}

}

}