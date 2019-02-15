using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nighday {

public class BoomBallManager : MonoBehaviour {
    
    [SerializeField] private BangEffect _bangEffectPrefab;

    [Space]
    [SerializeField] private int _pointForBall;
    [SerializeField] private float _pointsForBallByScaleFactor;

    public void RunBoom(BallEntity ball) {

        RunEffectBoom(ball);
        
        Destroy(ball.gameObject);
    }

    private int GetPointsForBall(BallEntity ball) {
        int addPoints = (int)((_pointForBall * (_pointsForBallByScaleFactor / ball.transform.localScale.x)) / ball.transform.localScale.x);
        
        return addPoints;
    }

    private void RunEffectBoom(BallEntity ball) {
        GameManager.Instance.Player.ChangePoints(GetPointsForBall(ball));

        var bangEffect = Instantiate(_bangEffectPrefab);
        bangEffect.SetStartLifeTimeParticles(ball.transform.localScale.x * 0.05f);
        bangEffect.SetColor(GetGradientForBang(ball));
        bangEffect.transform.position = ball.transform.position;
    }

    private ParticleSystem.MinMaxGradient GetGradientForBang(BallEntity ball) {
        Gradient gradient = CreateGradient(
            new (Color color, float alpha, float time)[] {
                (ball.Color, 1, 0),
                (ball.Color, 1, 0.8f),
                (new Color(1, 1, 1), 0, 1),
            }
        );
		
        ParticleSystem.MinMaxGradient minMaxGradient = new ParticleSystem.MinMaxGradient(gradient);
        return minMaxGradient;
    }

    private Gradient CreateGradient((Color color, float alpha, float time)[] keys) {
        int len = keys.Length;
        
        Gradient gradient = new Gradient();
        GradientColorKey[] colorKey = new GradientColorKey[len];
        GradientAlphaKey[] alphaKey = new GradientAlphaKey[len];

        for (var i=0; i<len; i++) {
            var key = keys[i];
            
            colorKey[i].color = key.color;
            colorKey[i].time  = key.time;
            
            alphaKey[i].alpha = key.alpha;
            alphaKey[i].time  = key.time;
        }
        
        gradient.SetKeys(colorKey, alphaKey);
        return gradient;
    }
    
}

}