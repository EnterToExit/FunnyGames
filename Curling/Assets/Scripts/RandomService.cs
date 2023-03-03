using UnityEngine;

public class RandomService : Service, IUpdate
{
    [SerializeField] private float _speed;
    [SerializeField] private AnimationCurve _curve;
    private float _add;
    public float ForceMultiplier;

    public void GameUpdate(float delta)
    {
        _add += _speed * delta;
        ForceMultiplier = _curve.Evaluate(Mathf.Repeat(_add, 1f));
    }
}