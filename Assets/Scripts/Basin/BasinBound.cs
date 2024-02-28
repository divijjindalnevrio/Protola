using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasinBound : MonoBehaviour
{
    public MeshFilter counterMeshFilter;
    public MeshFilter BasinMeshFilter;

    public Vector3[] Vertex;
    public Vector3[] basinVertex;

    public List<Vector3> vertices = new List<Vector3>();
    public List<Vector3> BasinVertices = new List<Vector3>();

    public float x;
    public float z;
    float xMin, xMax, zMin, zMax;
    [SerializeField] private GameObject npoint;
    [SerializeField] private BasinMovement basinMovement;
    [SerializeField] private CounterGenerator counterGenerator;

    void Start()
    {
        basinMovement.OnCounterStopMoving += GetMaxAndMinXPosition;
        basinMovement.OnGameobjectSelected += BasinMovement_OnGameobjectSelected;
        GettingMeshVertices();
        GetMaxAndMinXPosition();
    }

    private void BasinMovement_OnGameobjectSelected(object sender, SelectedObject e)
    {
       if(e == SelectedObject.basin)
        {

        }
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

    private void GettingBasinMeshVertices()
    {
        GetTheVerticesOfBasin();
        vertices.Clear();
        foreach (Vector3 index in basinVertex)
        {
            BasinVertices.Add(BasinMeshFilter.transform.TransformPoint(index));
        }
    }


    public void GetMaxAndMinXPosition()
    {
        GettingMeshVertices();
        vertices = vertices.Distinct().ToList();
        xMin = vertices.OrderBy(v => v.x).First().x;
        xMax = vertices.OrderBy(v => v.x).Last().x;

        zMin = vertices.OrderBy(v => v.z).First().z;
        zMax = vertices.OrderBy(v => v.z).Last().z;
    }

    void GetMaxAndMinXPositionBasin()
    {
        GettingBasinMeshVertices();
        BasinVertices = BasinVertices.Distinct().ToList();
    }

    public Vector3 ClapOnXAxis(RaycastHit pointHit, Transform basinSize)
    {
        x = Mathf.Clamp(pointHit.point.x, xMin + basinSize.localScale.x/2, xMax - basinSize.localScale.x/2);
        z = Mathf.Clamp(pointHit.point.z, zMin + basinSize.localScale.z / 2, zMax - basinSize.localScale.z / 2);

        // z = Mathf.Clamp(pointHit.point.z, zMin + 0.5f, zMax - 0.5f);

        Debug.Log("XAxisMaxLength : "+ x);
        return new Vector3(x, 0, z);  
    }


    void GetTheVerticesOfCounter(MeshFilter counterMeshFilter)
    {
        Vertex = counterMeshFilter.mesh.vertices;
    }

    private void GetTheVerticesOfBasin()
    {
       // BasinMeshFilter = basinMovement.SelectedGameobject.get;
        basinVertex = BasinMeshFilter.mesh.vertices;
    }


}
