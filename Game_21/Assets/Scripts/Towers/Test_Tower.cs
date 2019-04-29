using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Tower : Tower_Base
{
	[SerializeField]private int damage = 1;
	public Color testPewColor;
	public float pewDuration = 0.3f;
	private bool drawPew = false;
	private Vector3 targetPos;

	public override int Shoot(GameObject target) {
		int res = target.GetComponent<g_Health>().damage(damage);
		targetPos = target.transform.position;
		StartCoroutine("pewHandle");
		return res;
	}

	IEnumerator pewHandle() {
		drawPew = true;
		yield return new WaitForSeconds(pewDuration);
		drawPew = false;
	}

	// FIXME dirty pew render
	static Material lineMaterial;
    static void CreateLineMaterial()
    {
        if (!lineMaterial)
        {
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(shader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            // Turn on alpha blending
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            lineMaterial.SetInt("_ZWrite", 0);
        }
    }


	public void OnRenderObject()
    {
		if (!drawPew)
			return ;
		// drawPew = false;

        CreateLineMaterial();
        // Apply the line material
        lineMaterial.SetPass(0);

        GL.PushMatrix();
        // Set transformation matrix for drawing to
        // match our transform
        GL.MultMatrix(transform.localToWorldMatrix);

        // Draw lines
        GL.Begin(GL.LINES);
		// Vertex colors change from red to green
		GL.Color(testPewColor);
		// One vertex at transform position
		GL.Vertex3(0, 0, 0);
		Vector3 dir = targetPos - transform.position;
		// Another vertex at edge of circle
		GL.Vertex3(dir.x, dir.y, 0f);
        GL.End();
        GL.PopMatrix();
    }
}
