using UnityEngine;

// From http://catlikecoding.com/unity/tutorials/procedural-grid/

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshGrid : MonoBehaviour {

    private const int _width = 15;
    private const int _height = 15;

    private Vector3[] vertices;

    private Mesh mesh;

	// Use this for initialization
	void Awake () {
        Generate();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Generate() {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";
        vertices = new Vector3[(_width + 1) * (_height + 1)];
        Vector2[] uv = new Vector2[vertices.Length];
        Vector4[] tangents = new Vector4[vertices.Length];
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
        for (int i = 0, y = 0; y <= _height; y++) {
            for (int x = 0; x <= _width; x++, i++)
            {
                vertices[i] = new Vector3(x, y);
                uv[i] = new Vector2((float)x / _width, (float)y / _height);
                tangents[i] = tangent;
            }
        }
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.tangents = tangents;
        int[] triangles = new int[_width * _height * 6];
        for (int ti = 0, vi = 0, y = 0; y < _height; y++, vi++) {
            for (int x = 0; x < _width; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + _width + 1;
                triangles[ti + 5] = vi + _width + 2;
            }
        }
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
