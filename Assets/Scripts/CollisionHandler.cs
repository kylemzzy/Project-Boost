using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{   
    
    float timeDelay = 1f;
    
    AudioSource audioSource;
    AudioClip death;
    AudioClip finish;
    // we will create serialized fields for these ones becuase
    // we already positioned where we want the particles to appear.
    // this just makes it easier
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    // when we reload a state, it goes back to false. thus we do not want to revert state ourselves
    bool isTransitioning = false;
    void Start(){
        
        audioSource = GetComponent<AudioSource>();
        death = Resources.Load<AudioClip>("Sounds/death");
        finish = Resources.Load<AudioClip>("Sounds/finish");
    }
    void OnCollisionEnter(Collision other)
    {   
        // if we are not transitioning then return
        if (isTransitioning) { return; }

        switch (other.gameObject.tag){
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                Debug.Log("Finish");
                StartFinishSequence();
                break;
            default:
                StartCrashSequence();
                break;
            
        }
    }

    void StartFinishSequence(){
        isTransitioning = true;
        // there is a movement script inside the rocket. 
        // WHEN PLAYING SOUND, RELOADING OR LOADING A SCENE WILL STOP THE AUDIO
        audioSource.Stop();
        audioSource.PlayOneShot(finish);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        // calls the function AFTER 1 second float
        Invoke("LoadNextLevel", timeDelay);
    }
    void StartCrashSequence(){
        isTransitioning = true;
        // there is a movement script inside the rocket. 
        // WHEN PLAYING SOUND, RELOADING OR LOADING A SCENE WILL STOP THE AUDIO
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        crashParticles.Play();
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
