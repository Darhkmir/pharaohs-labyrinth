using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	
	public Text theText;
	
	public void OnPointerEnter(PointerEventData eventData) {
		theText.color = new Color32(236, 0, 95, 255);
	}
	
	public void OnPointerExit(PointerEventData eventData) {
		theText.color = new Color32(94, 32, 116, 255);
	}
	
	public void ResetHover() {
		theText.color = new Color32(94, 32, 116, 255);
	}
}
