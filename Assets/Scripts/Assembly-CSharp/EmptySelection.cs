using UnityEngine;
using UnityEngine.EventSystems;

public class EmptySelection : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	public GameManager gameManager;

	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log("funcionou?");
		gameManager.selectedState.text = "";
	}
}
