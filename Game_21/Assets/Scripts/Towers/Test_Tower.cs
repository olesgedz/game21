using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Tower : Tower_Base
{
    public float rpm;
	private List<GameObject> targetList;
	private GameObject target;
	private float timer;
	[SerializeField]private int damage = 1;
	[SerializeField]private float radius = 3f;
	private CircleCollider2D cirCol;
	public Color testPewColor;
	public float pewDuration = 0.3f;

	public override void Start() {
		base.Start();
		targetList = new List<GameObject>();
	}

	public override void OnBuild() {
		base.OnBuild();
		cirCol = gameObject.AddComponent<CircleCollider2D>();
		cirCol.radius = radius;
		cirCol.isTrigger = true;
	}

	void postBuildUpdate() {
		if (isBuilt && Time.time > timer + 60 / rpm) {
			timer = Time.time;
			if (targetList.Count == 0) {
				return;
			}
			target = targetList[0];
			StartCoroutine("pewHandle");
			int ret = target.GetComponent<g_Health>().damage(damage);
			targetPos = target.transform.position;
			if (ret <= 0) {
				targetList.Remove(target);
			}
		}
	}

	IEnumerator pewHandle() {
		drawPew = true;
		yield return new WaitForSeconds(pewDuration);
		drawPew = false;
	}

	public override void Update() {
		base.Update();
		if (isBuilt) {
			postBuildUpdate();
		}
	}

    void OnTriggerEnter2D(Collider2D col) {
		if (isBuilt) {
			if (col.tag == "Enemy") {
				targetList.Add(col.gameObject);
			}
		}
	}
	public override void OnTriggerStay2D(Collider2D col) {
		base.OnTriggerStay2D(col);
	}

	public override void OnTriggerExit2D(Collider2D col) {
		base.OnTriggerExit2D(col);
		if (isBuilt) {
			if (col.tag == "Enemy") {
				targetList.Remove(col.gameObject);
			}
		}
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

	private bool drawPew = false;
	private Vector3 targetPos;

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
