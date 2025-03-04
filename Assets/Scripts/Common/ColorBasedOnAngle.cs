using UnityEngine;

public class ColorBasedOnAngle : MonoBehaviour
{
    public Color frontColor = Color.red;
    public Color backColor = Color.blue;
    
    public Transform target;
    public MeshRenderer meshRenderer;

    bool Validate()
    {
        if (target == null || meshRenderer == null) return false;
        return true;
    }

    private void Update()
    {
        if (!Validate()) return;
        Vector3 forward = transform.forward;
        Vector3 toTargetVector = target.position - transform.position;
        
        meshRenderer.material.color = Color.Lerp(backColor, frontColor, (Vector3.Dot(forward, toTargetVector.normalized) + 1f) / 2f);
    }
}
