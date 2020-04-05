using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
public class Mesh_Generator : MonoBehaviour
{

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    public int xSize = 0;
    public int zSize = 0;
    // Start is called before the first frame update
    void Start()
    {
       mesh = new Mesh(); 
       GetComponent<MeshFilter>().mesh = mesh;
       StartCoroutine(CreateShape());
    }

    private void Update(){
       UpdateMesh();
    }

    IEnumerator CreateShape()
    {
        vertices = new Vector3[(xSize+1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++){
            for (int x = 0; x <= xSize; x++) {
                float y = Mathf.PerlinNoise(x * 0.3f, z * 0.3f) * 2f;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }
           
        triangles = new int[xSize * zSize * 6];
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++) {
            for (int x = 0; x < xSize; x++) {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;

                yield return new WaitForSeconds(.01f);

            }
            vert++;
        }
    }

    void UpdateMesh(){
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    private void OnDrawGizmos() {
        if (vertices == null) {
            return;
        }

        for (int i = 0; i < vertices.Length; i++) {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }
}
