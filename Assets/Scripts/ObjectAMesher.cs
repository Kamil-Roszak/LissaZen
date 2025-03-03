using UnityEngine;
using MeshHelper;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class ObjectAMesher : MonoBehaviour
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
    /// Creates ObjectA mesh - one center sphere and 4 flipped around it
    /// </summary>
    private void CreateMesh()
    {
        float spheresMargin = 2f;

        Mesh centerSphere = MeshHelper.MeshHelper.CreateSphere(20, 20, false);
        Mesh frontMesh = MeshHelper.MeshHelper.CreateSphere(20, 20, true, transform.forward * spheresMargin);
        Mesh backMesh = MeshHelper.MeshHelper.CreateSphere(20, 20, true, -transform.forward * spheresMargin);
        Mesh leftMesh = MeshHelper.MeshHelper.CreateSphere(20, 20, true, -transform.right * spheresMargin);
        Mesh rightMesh = MeshHelper.MeshHelper.CreateSphere(20, 20, true, transform.right * spheresMargin);

        CombineInstance[] combineInstances =
        {
            CreateCombinedInstance(centerSphere),
            CreateCombinedInstance(frontMesh),
            CreateCombinedInstance(backMesh),
            CreateCombinedInstance(leftMesh),
            CreateCombinedInstance(rightMesh)
        };

        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combineInstances, true, true);

        meshFilter.mesh = combinedMesh;
    }

    private CombineInstance CreateCombinedInstance(Mesh mesh)
    {
        CombineInstance instance = new CombineInstance();
        instance.mesh = mesh;
        instance.transform = transform.localToWorldMatrix;
        return instance;
    }
}
