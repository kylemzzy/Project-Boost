using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject targetObject;
    CollisionHandler targetScript;
    void Start()
    {
        // retrieve the parent of the game object
        targetObject = transform.parent.gameObject;
        if (targetObject == null) {
            Debug.Log("Target object not found");
            return;
        }
        // retrieve the script
        targetScript = targetObject.GetComponent<CollisionHandler>();
        if (targetScript == null){
            Debug.Log("Target script not found");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        PressedLKey();
        PressedCKey();
    }

    void PressedLKey(){
        if (Input.GetKeyDown(KeyCode.L)){
            //verify that it exists
            if (targetScript == null){
                return;
            }
            targetScript.LoadNextLevel();
        }
    }
    void PressedCKey(){
        if (Input.GetKeyDown(KeyCode.C)){
            //verify that it exists
            if (targetScript == null){
                return;
            }
            targetScript.isCollidable = !targetScript.isCollidable;
            
            Debug.Log("C pressed collidable is:" + targetScript.isCollidable);
        }
    }
}
