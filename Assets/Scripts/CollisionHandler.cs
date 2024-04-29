using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{   
    float timeDelay = 1f;
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag){
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                Debug.Log("Finish");
                LoadNextLevel();
                break;
            default:
                StartCrashSequence();
                break;
            
        }
    }
    void StartCrashSequence(){
        // there is a movement script inside the rocket. 
        GetComponent<Movement>().enabled = false;
        // calls the function AFTER 1 second float
        Invoke("ReloadCurrentLevel", timeDelay);
    }
    void ReloadCurrentLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        GetComponent<Movement>().enabled = true;
    }

    void LoadNextLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
