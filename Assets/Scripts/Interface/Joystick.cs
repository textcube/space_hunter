using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Vector2 posBase;
    public Vector2 posStick;

    public Transform stick;
    public float radius = 30;

    [HideInInspector]
    public bool isPress = false;
    [HideInInspector]
    public Vector2 delta = Vector3.zero;

    static Joystick s_instance = null;
    public static Joystick Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = FindObjectOfType(typeof(Joystick)) as Joystick;
                if (null == s_instance)
                    Debug.Log("Fail to get instance");
            }
            return s_instance;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPress = true;
        posStick = eventData.position;
        ResetDelta();
        MoveStick();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPress = false;
        posStick = posBase;
        ResetDelta();
        MoveStick();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 vec = eventData.position - posBase;
        float dist = vec.magnitude;
        if (dist > radius)
            posStick = posBase + (vec.normalized * radius);
        else
            posStick = eventData.position;
        MoveStick();
    }

    void ResetDelta()
    {
        posBase = transform.position;
        delta = Vector2.zero;
    }

    void MoveStick()
    {
        stick.position = posStick;
        delta = (posStick - posBase) / radius;
#if UNITY_STANDALONE && !UNITY_EDITOR
        delta /= 10f;
#endif
    }

    void OnApplicationQuit()
    {
        s_instance = null;
    }
}