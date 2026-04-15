using UnityEngine;
using UnityEngine.EventSystems;

public class DelayFix : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	public LevelLoader loader;

	public AudioManager manager;

	public void OnPointerDown(PointerEventData eventData)
	{
		manager.Play("Jogar");
		loader.LoadScene("Game");
	}
}
