using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoardManager : MonoBehaviour
{
    [SerializeField] ScarabManager[] scarabs; 
    [SerializeField] TextMeshProUGUI winText;
    [SerializeField] ParticleSystem winParticles;
    [SerializeField] bool blockXAxis;
    [SerializeField] bool blockZAxis;
    [SerializeField] float winTime = 2f;

    [SerializeField] LineCreator lineCreator;
    [SerializeField] AudioClip resetAudio;
    [SerializeField] AudioClip winAudio;
    AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    
    public bool IsStarted(){
        foreach(ScarabManager scarab in scarabs){
            if(scarab.IsChosen){
                return true;
            }
        }
        return false;
    }

    public void ResetBoard(){
        for(int i=0;i<scarabs.Length;i++){
            scarabs[i].ResetScarab();
        }
        audioSource.PlayOneShot(resetAudio);
        lineCreator.Clear();
    }
    public void CheckForWin(){
        foreach(ScarabManager scarab in scarabs){
            if(!scarab.isConnectedToAll()){
                return;
            }
        }
        StartCoroutine(Win());
    }

    /*
    public void MoveScarabs(Vector3 position){
        for(int i=0;i<scarabs.Length;i++){
            scarabs[i].LookAtMousePosition(position, blockXAxis, blockZAxis);
        }
    }
    */

    IEnumerator Win(){
        winText.enabled = true;
        winText.text = "You won!";
        winParticles.Play();
        audioSource.PlayOneShot(winAudio);
        yield return new WaitForSeconds(winTime);

        winText.enabled = false;
    }

}
