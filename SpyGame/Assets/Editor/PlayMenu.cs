using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

public class PlayMenu : MonoBehaviour {

	[MenuItem("Play/StartScene")]
	public static void PlayFromPrelaunchScene() {
		if (EditorApplication.isPlaying == true) {
			EditorApplication.isPlaying = false;
			return;
		}

		EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
		EditorSceneManager.OpenScene("Assets/Scenes/StartScene.unity");

		EditorApplication.isPlaying = true;
	}
}
