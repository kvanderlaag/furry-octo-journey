using UnityEngine;

// From http://catlikecoding.com/unity/tutorials/rounded-cube/

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class RoundedCube : MonoBehaviour {

    private const int _width = 10;
    private const int _height = 10;
    private const int _depth = 10;

    private const int roundness = 4;

    private Mesh mesh;
    private Vector3[] vertices;
    private Vector3[] normals;

	private void Awake () {
        Generate();
	}
	
    private void Generate() {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Custom Cube Mesh";
        CreateVertices();
        CreateTriangles();
    }

    private void CreateVertices() {
        int cornerVertices = 8;
        int edgeVertices = (_width + _height + _depth - 3) * 4;
        int faceVertices = (
            (_width - 1) * (_height - 1) +
            (_width - 1) * (_depth - 1) +
            (_height - 1) * (_depth - 1)) * 2;
        vertices = new Vector3[cornerVertices + edgeVertices + faceVertices];
        normals = new Vector3[vertices.Length];

        int v = 0;
        for (int y = 0; y <= _height; y++) {
            // Fill in row on the "front" of the cube
            for (int x = 0; x <= _width; x++) {
                SetVertex(v++, x, y, 0);
            }

            // Fill in sides along the z axis
            for (int z = 1; z <= _depth; z++) {
                SetVertex(v++, _width, y, z);
            }  

            for (int x = _width -1; x >= 0; x--) {
                SetVertex(v++, x, y, _depth);
            }

            for (int z = _depth - 1; z > 0; z--) {
                SetVertex(v++, 0, y, z);
            }
        }

        // Fill in top and bottom
        for (int z = 1; z < _depth; z++) {
            for (int x = 1; x < _width; x++) {
                SetVertex(v++, x, _height, z);
            }
        }
        for (int z = 1; z < _depth; z++) {
            for (int x = 1; x < _width; x++) {
                SetVertex(v++, x, 0, z);
            }
        }

        mesh.vertices = vertices;
        mesh.normals = normals;
    }

    private void SetVertex(int i , int x, int y, int z) {
        vertices[i] = new Vector3(x, y, z);
    }

    private void CreateTriangles() {
        int quads = (_width * _height + _width * _depth + _height * _depth) * 2;
        int[] triangles = new int[quads * 6];
        int ring = (_width + _depth) * 2;
        int t = 0, v = 0;

        for(int y = 0; y < _height; y++, v++) {
            for (int q = 0; q < ring - 1; q++, v++) {
                t = SetQuad(triangles, t, v, v + 1, v + ring, v + ring + 1);
            }
            t = SetQuad(triangles, t, v, v - ring + 1, v + ring, v + 1);
        }

        t = CreateTopFace(triangles, t, ring);
        t = CreateBottomFace(triangles, t, ring);

        mesh.triangles = triangles;
    }

    private int CreateTopFace(int[] triangles, int t, int ring) {
        int v = ring * _height;
        // First row:
        for (int x = 0; x < _width - 1; x++, v++) {
            t = SetQuad(triangles, t, v, v + 1, v + ring - 1, v + ring);
        }
        t = SetQuad(triangles, t, v, v + 1, v + ring - 1, v + 2);
        // End first row

        // Middle rows:
        int vMin = ring * (_height + 1) - 1;
        int vMid = vMin + 1;
        int vMax = v + 2;
        for (int z = 1; z < _depth - 1; z++, vMin--, vMid++, vMax++) {
            // First quad for this row (first and last are split out because they deal with the outer ring
            t = SetQuad(triangles, t, vMin, vMid, vMin - 1, vMid + _width - 1);
            // Middle quads:
            for (int x = 1; x < _width - 1; x++, vMid++) {
                t = SetQuad(triangles, t, vMid, vMid + 1, vMid + _width - 1, vMid + _width);
            }
            // Final quad:
            t = SetQuad(triangles, t, vMid, vMax, vMid + _width - 1, vMax + 1);
        }

        // Last row:
        int vTop = vMin - 2;
        t = SetQuad(triangles, t, vMin, vMid, vTop + 1, vTop);
        for (int x = 1; x < _width - 1; x++, vTop--, vMid++) {
            t = SetQuad(triangles, t, vMid, vMid + 1, vTop, vTop -1);
        }
        t = SetQuad(triangles, t, vMid, vTop - 2, vTop, vTop -1);

        return t;
    }

    private int CreateBottomFace(int[] triangles, int t, int ring) {
        int v = 1;
        int vMid = vertices.Length - (_width - 1) * (_depth - 1);
        t = SetQuad(triangles, t, ring - 1, vMid, 0, 1);
        for (int x = 1; x < _width - 1; x++, v++, vMid++) {
            t = SetQuad(triangles, t, vMid, vMid + 1, v, v + 1);
        }
        t = SetQuad(triangles, t, vMid, v + 2, v, v + 1);

        int vMin = ring - 2;
        vMid -= _width - 2;
        int vMax = v + 2;

        for (int z = 1; z < _depth - 1; z++, vMin--, vMid++, vMax++) {
            t = SetQuad(triangles, t, vMin, vMid + _width - 1, vMin + 1, vMid);
            for (int x = 1; x < _width - 1; x++, vMid++) {
                t = SetQuad(triangles, t, vMid + _width - 1, vMid + _width, vMid, vMid + 1);
            }
            t = SetQuad(triangles, t, vMid + _width - 1, vMax + 1, vMid, vMax);
        }

        int vTop = vMin - 1;
        t = SetQuad(triangles, t, vTop + 1, vTop, vTop + 2, vMid);
        for (int x = 1; x < _width - 1; x++, vTop--, vMid++) {
            t = SetQuad(triangles, t, vTop, vTop - 1, vMid, vMid + 1);
        }
        t = SetQuad(triangles, t, vTop, vTop - 1, vMid, vTop - 2);

        return t;
    }

    private static int SetQuad(int[] triangles, int i, int v00, int v10, int v01, int v11) {
        triangles[i] = v00;
        triangles[i + 1] = triangles[i + 4] = v01;
        triangles[i + 2] = triangles[i + 3] = v10;
        triangles[i + 5] = v11;
        return i + 6;
    }

    private void OnDrawGizmos () {
        if (vertices == null) {
            return;
        }

        for (int i = 0; i < vertices.Length; i++) {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(vertices[i], 0.1f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(vertices[i], normals[i]);

        }
    }
}
