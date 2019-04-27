using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Render : MonoBehaviour
{
	public Vector2 startPoint = Vector2.zero;
	public Vector2 size = Vector2.one;
	public Color gridColor = Color.gray;
	private bool gridOn = false;
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

	public void setGrid(bool state) {
		gridOn = state;
	}

    // Will be called after all regular rendering is done
    public void OnRenderObject()
    {
		if (!gridOn)
			return ;
        CreateLineMaterial();
        // Apply the line material
        lineMaterial.SetPass(0);

        GL.PushMatrix();
        // Set transformation matrix for drawing to
        // match our transform
        GL.MultMatrix(transform.localToWorldMatrix);

        // Draw lines
        GL.Begin(GL.LINES);
        for (int x = 0; x <= size.x; x++) {
				//
				// Vertex colors change from red to green
				GL.Color(gridColor);
				// One vertex at transform position
				GL.Vertex3(startPoint.x + x, startPoint.y, 0);
				// Another vertex at edge of circle
				GL.Vertex3(startPoint.x + x, startPoint.y + size.y, 0);
        }
		for (int y = 0; y <= size.y; y++) {
				//
				// Vertex colors change from red to green
				GL.Color(gridColor);
				// One vertex at transform position
				GL.Vertex3(startPoint.x, startPoint.y + y, 0);
				// Another vertex at edge of circle
				GL.Vertex3(startPoint.x + size.x, startPoint.y + y, 0);
        }
        GL.End();
        GL.PopMatrix();
    }
}
