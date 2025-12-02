using UnityEngine;
using UnityEngine.EventSystems;

public class HoverToPlayAnimation : MonoBehaviour, IPointerEnterHandler
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("idle", -1, 0f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("get into");
        anim.Play("door_anim", -1, 0f);
    }
}