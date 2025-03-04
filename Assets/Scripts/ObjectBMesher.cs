using UnityEngine;
using MeshHelper;
using System;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class ObjectBMesher : MonoBehaviour
{
    [SerializeField]
    private MeshFilter meshFilter;
    [SerializeField]
    private MeshRenderer meshRenderer;

    private bool RuntimeValidate()
    {
        if (meshFilter == null || meshRenderer == null)
        {
            Debug.LogError("Validation Error!");
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
            meshFilter.mesh = CreateMesh();
        }
    }

    /// <summary>
    /// Creates ObjectB mesh - one cube
    /// </summary>
    private Mesh CreateMesh()
    {
        Mesh cubeSphere = MeshHelper.MeshHelper.CreateCube(false);
        return cubeSphere;
    }

    private void OnDrawGizmos()
    {
        if (RuntimeValidate())
        {
            //just create mesh on Gizmos - not ideal solution but works
            Gizmos.DrawWireMesh(CreateMesh(), transform.position);
        }
    }
}
