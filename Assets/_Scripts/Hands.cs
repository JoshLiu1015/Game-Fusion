using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Hands : MonoBehaviour
{

    XRDirectInteractor dirInteractor;
    public InputActionReference selectRef;
    public GameObject redStone;

    GameObject stoneCreated;

    bool inFire;

    // Start is called before the first frame update
    void Start()
    {
        inFire = false;
        dirInteractor = GetComponent<XRDirectInteractor>();
        stoneCreated = null;

        selectRef.action.started += GripStarted;
        selectRef.action.canceled += GripReleased;

    }


    private void OnDestroy(){
        selectRef.action.started -= GripStarted;
        selectRef.action.canceled -= GripReleased;
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    void GripStarted(InputAction.CallbackContext context){
        if (inFire && stoneCreated == null){
            stoneCreated = Instantiate(redStone, transform.position, transform.rotation);
            dirInteractor.StartManualInteraction(stoneCreated.GetComponent<IXRSelectInteractable>());
        }
        
    }

    void GripReleased(InputAction.CallbackContext context){
        if (dirInteractor.isPerformingManualInteraction && stoneCreated != null){
            stoneCreated = null;
            dirInteractor.EndManualInteraction();
        }
    }
    
    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Fire")){
            inFire = true;
        }
    }

    void OnTriggerExit(Collider other){
        if (other.gameObject.CompareTag("Fire")){
            inFire = false;
        }
    }
}
