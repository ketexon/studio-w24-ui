using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonManager_complete : MonoBehaviour, IPointerClickHandler
{
    public GameObject currentlySelectedButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // if background click:
        if (eventData.selectedObject == null)
        {
            EventSystem.current.SetSelectedGameObject(currentlySelectedButton);
        }
    }
}
