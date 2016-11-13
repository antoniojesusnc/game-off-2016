using UnityEngine;  
using System.Collections;  
using UnityEngine.EventSystems;  
using UnityEngine.UI;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public Text theText;
	private Color originalColor;

	void Start() 
	{
		theText = GetComponentInChildren<Text> ();
		originalColor = theText.color;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		theText.color = Color.red; //Or however you do your color
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		theText.color = originalColor; //Or however you do your color
	}

	public void SetNextScene(string nextSceneName) {
		Debug.Log ("Setting next scene name to " + nextSceneName);
		SpyGame.SceneController.SwitchScene (nextSceneName);
	}

	public void Quit() {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
