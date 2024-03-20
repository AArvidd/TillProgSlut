using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Timeline;

public class treeCotroller : MonoBehaviour
{
    GameObject parent;
    int position;
    
    float timer = 3;

    int helth = 100;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //timer -= Time.deltaTime;
        if (helth <= 0){
            killMe();
        }
        
    }

    public void takeDamage(int damage){
        helth -= damage;
        GameObject child = transform.GetChild(0).gameObject;
        child.GetComponent<Animator>().Play("Tree_damage");
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
