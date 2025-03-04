using UnityEngine;

public class LissajousCurveAnimator : MonoBehaviour
{
    [Header("Curve Parameters")]
    [Range(0f, 20f)]
    public float A = 10;
    [Range(0f, 20f)]
    public float B = 10;
    [Range(1, 40)]
    public float a = 5;
    [Range(1, 40)]
    public float b = 5;
    [Range(0f, Mathf.PI*2f)]
    public float delta = Mathf.PI;
    [Range(0.1f, 20f)]
    public float timeScale = 0.1f;

    [Header("Other")]
    public Vector3 offset;
    public bool currentTransformIsOffset = true;

    [Header("Editor Gizmos Parameters")]
    public int gizmosResolution = 100;

    private float _timer = 0f;
    private const float _tMax = 2 * Mathf.PI; 

    // Update is called once per frame
    void Update()
    {
        Vector2 lissajousCurveValue = MathAdditions.MathCurves.LissajousCurve(A, B, a, b, delta, _timer);
        transform.position = new Vector3(lissajousCurveValue.x, transform.position.y, lissajousCurveValue.y) + offset;
        _timer += Time.deltaTime * timeScale;
        if(_timer > _tMax)
        {
            _timer = 0f;
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 curveValueForFirstPoint = MathAdditions.MathCurves.LissajousCurve(A, B, a, b, delta, _timer);
        Vector3 previousPoint = new Vector3(curveValueForFirstPoint.x, transform.position.y, curveValueForFirstPoint.y) + offset;

        for(int i = 1; i < gizmosResolution; i++)
        {
            float t = i * (_tMax / gizmosResolution);
            Vector2 currentCurveValue = MathAdditions.MathCurves.LissajousCurve(A, B, a, b, delta, t);
            Vector3 currentPoint = new Vector3(currentCurveValue.x, transform.position.y, currentCurveValue.y) + offset;
            Gizmos.DrawLine(previousPoint, currentPoint);
            previousPoint = currentPoint;
            Gizmos.color = Color.blue;
        }
    }
}
