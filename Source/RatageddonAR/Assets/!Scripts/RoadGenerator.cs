using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] private float _roadWidth = 2.0f;
    [SerializeField] private Material _roadMaterial;
    [SerializeField] private NavMeshSurface _navMeshSurface;

    public void GenerateRoad(Transform start, Transform end)
    {
        GameObject roadObject = new GameObject("GeneratedRoad");
        MeshFilter meshFilter = roadObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = roadObject.AddComponent<MeshRenderer>();
        meshRenderer.material = _roadMaterial;

        Mesh roadMesh = CreateRoadMesh(start.position, end.position, _roadWidth);
        meshFilter.mesh = roadMesh;

        roadObject.AddComponent<MeshCollider>().sharedMesh = roadMesh;
        roadObject.layer = LayerMask.NameToLayer("Navmesh");
        _navMeshSurface.BuildNavMesh();
    }

    Mesh CreateRoadMesh(Vector3 start, Vector3 end, float width)
    {
        Vector3 direction = (end - start).normalized;
        Vector3 left = Vector3.Cross(Vector3.up, direction) * width * 0.5f;

        List<Vector3> vertices = new List<Vector3>
        {
            start + left,
            start - left,
            end + left,
            end - left
        };

        int[] triangles = {
            0, 1, 2,
            2, 1, 3
        };

        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }
}
