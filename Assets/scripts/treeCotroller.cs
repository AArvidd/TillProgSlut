using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeCotroller : MonoBehaviour
{
    GameObject parent;
    int position;
    
    float timer = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //timer -= Time.deltaTime;
        if (timer < 0){
            killMe();
        }
    }

    public void setVeriabls(GameObject parent, int position){
        this.parent=parent;
        this.position=position;
    }

    public void uppdatePosition(){
        this.position--;
    }

    private void killMe(){
        parent.GetComponent<ForestGenerator>().updateTrees(position);
        Destroy(gameObject);
    }

}
