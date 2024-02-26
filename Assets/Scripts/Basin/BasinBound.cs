using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasinBound : MonoBehaviour
{
    public MeshFilter counterMeshFilter;
    public Vector3[] Vertex;
    public List<Vector3> vertices = new List<Vector3>();

    public float x;
    public float z;
    float xMin, xMax, zMin, zMax;
    [SerializeField] private GameObject npoint;
    [SerializeField] private BasinMovement basinMovement;
    [SerializeField] private CounterGenerator counterGenerator;

    void Start()
    {
        basinMovement.OnCounterStopMoving += GetMaxAndMinXPosition;
        GettingMeshVertices();
        GetMaxAndMinXPosition();
    }

    private void GettingMeshVertices()
    {
        GetTheVerticesOfCounter(counterMeshFilter);
        vertices.Clear();
        foreach (Vector3 index in Vertex)
        {
            vertices.Add(counterMeshFilter.transform.TransformPoint(index));
        }
       
    }

    void GetMaxAndMinXPosition()
    {
        GettingMeshVertices();
        vertices = vertices.Distinct().ToList();
        xMin = vertices.OrderBy(v => v.x).First().x;
        xMax = vertices.OrderBy(v => v.x).Last().x;
        Debug.Log("BasinBound GetMaxAndMinXPosition : " + xMax + "  " + xMin);

        zMin = vertices.OrderBy(v => v.z).First().z;
        zMax = vertices.OrderBy(v => v.z).Last().z;
        Debug.Log("BasinBound GetMaxAndMinXPosition : " + zMax + "  " + zMin);
    }

    public Vector3 ClapOnXAxis(RaycastHit pointHit, Transform basinSize)
    {
        x = Mathf.Clamp(pointHit.point.x, xMin + basinSize.localScale.x/2, xMax - basinSize.localScale.x/2);
       // z = Mathf.Clamp(pointHit.point.z, zMin + basinSize.localScale.z/2, zMax - basinSize.localScale.z/2);
        z = Mathf.Clamp(pointHit.point.z, zMin + 0.5f, zMax - 0.5f);

        Debug.Log("XAxisMaxLength : "+ x);
        return new Vector3(x, 0, z);  
    }

    void GetTheVerticesOfCounter(MeshFilter counterMeshFilter)
    {
        Vertex = counterMeshFilter.mesh.vertices;
    }



}
