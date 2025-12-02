using UnityEngine;
using UnityEngine.EventSystems;

public class HoverToPlayAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Animator anim;

    public void OnPointerEnter(PointerEventData eventData)
    {
        anim.SetTrigger("Show");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anim.SetTrigger("Hide");
    }
}