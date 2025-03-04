using UnityEngine;
using MeshHelper;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class ObjectBMesher : MonoBehaviour
{
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private bool RuntimeValidate()
    {
        if (meshFilter == null || meshRenderer == null)
        {
            Debug.LogErrorFormat("Validation Error! {0}");
            return false;
        }
        return true;
    }

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();

        if (RuntimeValidate())
        {
            CreateMesh();
        }
    }

    /// <summary>
    /// Creates ObjectB mesh - one cube
    /// </summary>
    private void CreateMesh()
    {
        Mesh cubeSphere = MeshHelper.MeshHelper.CreateCube(false);
        meshFilter.mesh = cubeSphere;
    }
}
