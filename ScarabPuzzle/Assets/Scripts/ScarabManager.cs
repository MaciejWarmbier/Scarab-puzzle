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
    [SerializeField] float scarabOffset = 0.35f;
    
    List<(ScarabManager neighborScarab, bool isConnected)> connections;
    SpriteRenderer sprite;
    bool isChosen;
    public bool IsChosen{get {return isChosen;}}

    void Start()
    {
        sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        
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
        if(!boardManager.IsStarted()){
            ChangeToChosen();
        }
        else
        {
            for(int i=0; i<connections.Count; i++){
                (ScarabManager neighborScarab, bool isConnected) neighbor = connections[i];
                if (neighbor.neighborScarab.IsChosen && !neighbor.isConnected ){
                    connections[i] = (neighbor.neighborScarab,true);
                    neighbor.neighborScarab.AddConnection(this);
                    lineCreator.DrawLine(gameObject.transform.position, neighbor.neighborScarab.transform.position);
                    ChangeToChosen();
                    boardManager.CheckForWin();
                }
            }
        }
    }

    
    
    public void AddConnection(ScarabManager neighborPassed){
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

    public void LookAtMousePosition(Vector3 position, bool blockXAxis, bool blockZAxis){
        var angle = Mathf.Atan2(position.y, position.x) * Mathf.Rad2Deg;
        Vector3 old = position;
        Vector3 rotation = Quaternion.LookRotation(position).eulerAngles;
        if(blockZAxis){
            position = new Vector3(position.x, position.y , transform.TransformPoint(Vector3.zero).z);
            rotation.x = transform.rotation.x;
            rotation.y = transform.rotation.y;
        }
        else if(blockXAxis){
            rotation.z = transform.rotation.z;
            rotation.y = transform.rotation.y;
        }
        Debug.Log("Ray : " + old + "  Position: " + position + "  Scarab:" + transform.position);
        //transform.LookAt(position);
        transform.rotation = Quaternion.Euler(rotation);
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
       // transform.rotation = Quaternion.Euler(rotation);
    }
    
}
