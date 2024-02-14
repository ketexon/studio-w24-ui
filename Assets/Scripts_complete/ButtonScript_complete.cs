using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonScript_complete : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    public ButtonManager_complete ButtonManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Debug.Log(this.name + " got clicked!");
    }

    // Hook into Unity Event:
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("mouse entered " + name);
        // do stuff like displaying a preview, etc.
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("mouse exited " + name);
        //
    }

    //
    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("button selected " + name);
        // save currently selected button:
        ButtonManager.currentlySelectedButton = gameObject;
    }
    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("button deselected " + name);
    }
}
