using UnityEngine;

public class ExampleClass : MonoBehaviour
{
	// When added to an object, draws colored rays from the
	// transform position.
	public int lineCount = 100;
	public float radius = 3.0f;

	static Material lineMaterial;
	static void CreateLineMaterial ()
	{
		if (!lineMaterial)
		{
			// Unity has a built-in shader that is useful for drawing
			// simple colored things.
			Shader shader = Shader.Find ("Hidden/Internal-Colored");
			lineMaterial = new Material (shader);
			lineMaterial.hideFlags = HideFlags.HideAndDontSave;
			// Turn on alpha blending
			lineMaterial.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			lineMaterial.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			// Turn backface culling off
			lineMaterial.SetInt ("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
			// Turn off depth writes
			lineMaterial.SetInt ("_ZWrite", 0);
		}
	}

	// Will be called after all regular rendering is done
	public void OnRenderObject ()
	{
		CreateLineMaterial ();
		// Apply the line material
		lineMaterial.SetPass (0);

		GL.PushMatrix ();
		// Set transformation matrix for drawing to
		// match our transform
		GL.MultMatrix (transform.localToWorldMatrix);

		// Draw lines
		GL.Begin (GL.LINES);
//		for (int i = 0; i < lineCount; ++i)
//		{
//			float a = i / (float)lineCount;
//			float angle = a * Mathf.PI * 2;
//			// Vertex colors change from red to green
//			GL.Color (new Color (a, 1-a, 0, 0.8F));
//			// One vertex at transform position
//			GL.Vertex3 (0, 0, 0);
//			// Another vertex at edge of circle
//			GL.Vertex3 (Mathf.Cos (angle) * radius, Mathf.Sin (angle) * radius, 0);
//		}
		for (int i = 0; i < 6; i++) {
			// Set one color only for now
			GL.Color (new Color (1, 1, 1));
			Vector3 current = HexCorner (transform.position, radius, i);
			GL.Vertex3 (current.x, current.y, current.z);
			Vector3 next = HexCorner (transform.position, radius, (i+1) % 6);
			GL.Vertex3 (next.x, next.y, next.z);
		}
		GL.End ();
		GL.PopMatrix ();
	}


	private Vector3 HexCorner(Vector3 center, float size, int i)
	{
		float angle_deg = 60 * i + 30;
		float angle_rad = Mathf.PI / 180 * angle_deg;
		return new Vector3 (center.x + size * Mathf.Cos (angle_rad), center.y + size * Mathf.Sin (angle_rad), 0);
	}

}