using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverSound : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler
{
	public AudioManager manager;

	public Button thisButton;

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (thisButton.enabled)
		{
			manager.Play("Mapa_Hover");
		}
	}
}
