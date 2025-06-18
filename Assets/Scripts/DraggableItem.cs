using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform originalParent;
    private Vector2 startPos;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    public string itemType; // "GoldMine" or "Smelter"
    public float cost = 5f;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        startPos = transform.position;
        transform.SetParent(canvas.transform, false);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject target = eventData.pointerEnter;
        if (target != null && target.CompareTag("DropZone"))
        {
            if (GameObject.FindObjectOfType<GameManager>().gold >= cost)
            {
                GameObject.FindObjectOfType<GameManager>().gold -= cost;

                // Instantiate object in the drop zone
                if (itemType == "GoldMine")
                {
                    // Add gold mine logic here
                    Debug.Log("Gold Mine placed");
                }
                else if (itemType == "Smelter")
                {
                    // Add smelter logic here
                    Debug.Log("Smelter placed");
                }

                GameObject.FindObjectOfType<GameManager>().UpdateUI();
            }
            else
            {
                Debug.Log("Not enough gold!");
                ReturnToStart();
            }
        }
        else
        {
            ReturnToStart();
        }
    }

    void ReturnToStart()
    {
        transform.SetParent(originalParent, false);
        transform.position = startPos;
        canvasGroup.blocksRaycasts = true;
    }
}
