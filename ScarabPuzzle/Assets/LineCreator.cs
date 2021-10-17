using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreator : MonoBehaviour
{
    [SerializeField] Material lineMaterial;
    [SerializeField] float lineWidth = 0.4f;
    [SerializeField] float zLineOffset;
    [SerializeField] float xLineOffset;
    List<Vector3> positions;
    [SerializeField] GameObject Line;
    LineRenderer lineRenderer;
    
    private void Awake() {
        lineRenderer = Line.GetComponent<LineRenderer>();
    }
    

    public void DrawLine(Vector3 startPosition, Vector3 endPosition){
        
        setLineSettings();
        startPosition = new Vector3(startPosition.x + xLineOffset ,startPosition.y, startPosition.z + zLineOffset);
        endPosition = new Vector3(endPosition.x + xLineOffset,endPosition.y, endPosition.z + zLineOffset);
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1,endPosition);
        Instantiate(Line, transform);
        
    }

    void setLineSettings(){
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.material = lineMaterial;
        //lineRenderer.transform.parent = transform;
        lineRenderer.useWorldSpace = true;  
    }

    public void Clear(){
        foreach(Transform child in transform){
            Destroy(child.gameObject);
        }
        
    }
    
    
}

