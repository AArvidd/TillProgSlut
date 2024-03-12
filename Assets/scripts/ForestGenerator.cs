using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.Rendering;
using UnityEngine.XR;

public class ForestGenerator : MonoBehaviour
{

    GameObject forest;

    List<Vector3> vertexList = new List<Vector3>();
    List<Vector3> worldVertexList = new List<Vector3>();

    Vector3 randomPoint;


    [SerializeField]
    GameObject tree;

    List<Vector3> trees = new List<Vector3>();

    [SerializeField]
    float distens = 0;


    void Awake(){
        vertexList = new List<Vector3>(GetComponent<MeshFilter>().sharedMesh.vertices);
        uppdatePoints();
    }

    void Update() {
        generateTree();
    }

    private void generateTree(){
        generateRandomPointOnPlane();
        bool check = true;
        for(int i = 0; i < trees.Count; i++){
            if(Vector3.Distance(randomPoint, trees[i]) < distens){
                check = false;
            } 
        }
        if(check){
            trees.Add(randomPoint);
            Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            Instantiate(tree, randomPoint, rotation, gameObject.transform);
        }
    }


    private void uppdatePoints(){
        worldVertexList.Clear();
        for(int i = 0; i < vertexList.Count; i++){
            worldVertexList.Add(transform.TransformPoint(vertexList[i]));
        }
    }

    private void generateRandomPointOnPlane(){
        int max = (worldVertexList.Count-3) / 4;
        int square = Random.Range(0, max);
        generateRandomPoint(square);
    }

    private List<Vector3> generatesSquare(int i){
        
        List<Vector3> result = new List<Vector3>();

        i *= 4;

        result.Add(transform.TransformPoint(vertexList[i]));
        result.Add(transform.TransformPoint(vertexList[i+1]));
        result.Add(transform.TransformPoint(vertexList[i+2]));
        result.Add(transform.TransformPoint(vertexList[i+3]));
        
        return result;
    }

    private void generateRandomPoint(int i){
        

        List<Vector3> square = generatesSquare(i);
        int triangle = Random.Range(0, 2) == 0 ? 0 : 2;

        List<Vector3> edgeVectors = new List<Vector3>();

        edgeVectors.Add(square[3] - square[triangle]);
        edgeVectors.Add(square[1] - square[triangle]);

        float u = Random.Range(0f, 1f);
        float v = Random.Range(0f, 1f);
        if (u + v > 1){
            u = 1 - u;
            v = 1 - v;
        }

        randomPoint = square[triangle] + u * edgeVectors[0] + v * edgeVectors[1];

    }
    
}
