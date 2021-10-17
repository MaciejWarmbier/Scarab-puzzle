using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarabManager : MonoBehaviour, IClick
{
    [SerializeField] List<ScarabManager> neighbors;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite chosenSprite;
    [SerializeField] Sprite connectedSprite;
    [SerializeField] BoardManager boardManager;
    [SerializeField] LineCreator lineCreator;
    AudioSource audioSource;    
    List<(ScarabManager neighborScarab, bool isConnected)> connections;
    SpriteRenderer sprite;
    bool isChosen;
    public bool IsChosen{get {return isChosen;}}

    void Start()
    {
        sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        connections = new List<(ScarabManager neighborScarab, bool isConnected)>();
        foreach(ScarabManager neighbor in neighbors){
            (ScarabManager neighborScarab, bool isConnected) connection = (neighbor, false);
            connections.Add(connection);
        }
        
    }

    

    public void OnClick(){
        Connect();
    }
    
    void Connect(){
        if(!boardManager.IsStarted())
        {
            ChangeToChosen();
        }
        else
        {
            for(int i=0; i<connections.Count; i++){
                (ScarabManager neighborScarab, bool isConnected) neighbor = connections[i];
                if (neighbor.neighborScarab.IsChosen && !neighbor.isConnected ){
                    connections[i] = (neighbor.neighborScarab,true);
                    neighbor.neighborScarab.AddExternalConnection(this);
                    lineCreator.DrawLine(gameObject.transform.position, neighbor.neighborScarab.transform.position);
                    ChangeToChosen();
                    if(!boardManager.CheckForWin())
                    {
                        if(NeedsReset()){
                            boardManager.ResetBoard();
                        }
                    }
                }
            }
        }
    
    }

    
    
    public void AddExternalConnection(ScarabManager neighborPassed){
        for(int i=0; i<connections.Count; i++){
            (ScarabManager neighborScarab, bool isConnected) neighbor = connections[i];
            if(neighborPassed.GetInstanceID() == neighbor.neighborScarab.GetInstanceID()){
                connections[i] = (neighbor.neighborScarab,true);
                ChangeToConnected();
            }
        }
    }

    void ChangeToConnected(){
        isChosen = false;
        sprite.sprite = connectedSprite;
        audioSource.time = 0.380f;
        audioSource.Play();
    }
    void ChangeToChosen(){
        sprite.sprite = chosenSprite;
        isChosen = true;
    }

    public bool isConnectedToAll(){
        for(int i=0; i<connections.Count; i++){
            if(!connections[i].isConnected){
                return false;
            }
        }
        return true;
    }

    public void ResetScarab() {
        isChosen = false;
        sprite.sprite = defaultSprite;

        for(int i=0; i<connections.Count; i++){
            connections[i] = (connections[i].neighborScarab,false);
        }
    }

    bool NeedsReset(){
        for(int i=0; i<connections.Count; i++){
            if(!connections[i].isConnected){
                return false;
            }
        }
        return true;
    }
}
