using UnityEngine;

public class RotateTowardsTarget : MonoBehaviour
{
    public Transform target;
    public float angularSpeed = 5f;

    private bool Validate()
    {
        if (target == null)
        {
            Debug.LogError("Validation Error!");
            return false;
        }
        return true;
    }

    private void Update()
    {
        if (!Validate()) return;

        Vector3 directionToTarget = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angularSpeed * Time.deltaTime);
    }
}
