using UnityEngine;
using UnityEngine.EventSystems;

public class FixDelayCreditos : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	public Menu menuManager;

	public AudioManager audioManager;

	public void OnPointerDown(PointerEventData eventData)
	{
		audioManager.Play("Menu_Ida");
		menuManager.ativaCreditos();
		audioManager.EnableAmbiencia();
	}
}
