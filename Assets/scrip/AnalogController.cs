using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AnalogController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [Header("Analog UI")]
    public Image analogBackground;
    public Image analogHandle;

    [Header("Output")]
    public static float Horizontal = 0f;
    public static float Vertical = 0f;

    private Vector2 inputVector;
    private float handleRange = 1f;
    private float deadZone = 0.1f;

    void Start()
    {
        if (analogBackground == null)
            analogBackground = GetComponent<Image>();
        if (analogHandle == null)
            analogHandle = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            analogBackground.rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out pos
        );

        pos.x /= analogBackground.rectTransform.sizeDelta.x;
        pos.y /= analogBackground.rectTransform.sizeDelta.y;

        inputVector = new Vector2(pos.x * 2, pos.y * 2);
        inputVector = inputVector.magnitude > 1f ? inputVector.normalized : inputVector;

        // Gerakkan handle
        analogHandle.rectTransform.anchoredPosition = new Vector2(
            inputVector.x * analogBackground.rectTransform.sizeDelta.x / 2 * handleRange,
            inputVector.y * analogBackground.rectTransform.sizeDelta.y / 2 * handleRange
        );

        Horizontal = Mathf.Abs(inputVector.x) > deadZone ? inputVector.x : 0f;
        Vertical = Mathf.Abs(inputVector.y) > deadZone ? inputVector.y : 0f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        analogHandle.rectTransform.anchoredPosition = Vector2.zero;
        Horizontal = 0f;
        Vertical = 0f;
    }
}