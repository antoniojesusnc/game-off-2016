using UnityEditor;
using UnityEngine;

public class PlayMenu : MonoBehaviour {

	[MenuItem("Play/StartScene")]
	public static void PlayFromPrelaunchScene() {
		if (EditorApplication.isPlaying == true) {
			EditorApplication.isPlaying = false;
			return;
		}

		EditorApplication.SaveCurrentSceneIfUserWantsTo();
		EditorApplication.OpenScene("Assets/Scenes/StartScene.unity");

		EditorApplication.isPlaying = true;
	}
}
