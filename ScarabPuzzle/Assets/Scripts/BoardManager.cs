using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoardManager : MonoBehaviour
{
    [SerializeField] ScarabManager[] scarabs; 
    [SerializeField] TextMeshProUGUI upperText;
    [SerializeField] ParticleSystem winParticles;
    [SerializeField] bool blockXAxis;
    [SerializeField] bool blockZAxis;
    [SerializeField] float textTime = 2f;

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
        StartCoroutine(UpdateText("RESET"));
        audioSource.PlayOneShot(resetAudio);
        lineCreator.Clear();
    }

    IEnumerator UpdateText(string text) {
        upperText.enabled = true;
        upperText.text = text;

        yield return new WaitForSeconds(textTime);

        upperText.enabled = false;
    }
    public bool CheckForWin(){
        foreach(ScarabManager scarab in scarabs){
            if(!scarab.isConnectedToAll()){
                return false;
            }
        }
        Win();
        return true;
    }
    

    void Win(){
        StartCoroutine(UpdateText("You Won!"));
        winParticles.Play();
        audioSource.PlayOneShot(winAudio);

    }

}
