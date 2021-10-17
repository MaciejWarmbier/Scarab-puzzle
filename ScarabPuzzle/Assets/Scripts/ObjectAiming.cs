using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAiming : MonoBehaviour
{
    Camera cameraPointer;
    void Start()
    {
        cameraPointer = GetComponent<Camera>();
    }

    
    void Update()
    {
        //LocatingMousePosition();

        if(Input.GetKeyDown(KeyCode.R)){
            ResetObjects();
        }

        if(Input.GetMouseButtonDown(0)){
            ClickOnObject();
        }
    }

    /*
    void LocatingMousePosition(){
        RaycastHit[] hits;
        Ray ray = cameraPointer.ScreenPointToRay(Input.mousePosition);
        hits = Physics.RaycastAll(ray);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if(hit.collider!=null){
                Vector3 position = hit.point;
                Debug.Log(position);
                BoardManager boardManager = hit.collider.gameObject.GetComponent<BoardManager>();
                if(boardManager != null){
                    boardManager.MoveScarabs(position);
                    return;
                }
            }

            
        }

    }
    */

    void ResetObjects(){
        RaycastHit[] hits;
        Ray ray = cameraPointer.ScreenPointToRay(Input.mousePosition);
        hits = Physics.RaycastAll(ray);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if(hit.collider!=null){
                BoardManager boardManager = hit.collider.gameObject.GetComponent<BoardManager>();
                if(boardManager != null){
                    boardManager.ResetBoard();
                    return;
                }
            }

            
        }
    }

    void ClickOnObject(){
        RaycastHit[] hits;
        Ray ray = cameraPointer.ScreenPointToRay(Input.mousePosition);
        hits = Physics.RaycastAll(ray);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if(hit.collider!=null){
                IClick click = hit.collider.gameObject.GetComponent<IClick>();
                    if(click != null){
                        click.OnClick();
                        return;
                    }
            }

            
        }
        
    }
}
