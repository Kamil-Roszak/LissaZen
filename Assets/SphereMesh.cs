using UnityEngine;
using MeshHelper;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent (typeof(MeshFilter))]
public class SphereMesh : MonoBehaviour
{
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;

    private bool RuntimeValidate()
    {
        if (_meshFilter == null || _meshRenderer == null)
        {
            Debug.LogErrorFormat("Validation Error! {0}");
            return false;
        }
        return true;
    }

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshFilter = GetComponent<MeshFilter>();

        if (RuntimeValidate())
        {
            _meshFilter.mesh = MeshHelper.MeshHelper.CreateSphere(20, 20, true);
        }

    }

}
