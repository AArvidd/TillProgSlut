using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Timeline;

public class treeCotroller : MonoBehaviour
{
    GameObject parent;
    int position;
    
    float timer = 3;

    int helth = 100;

    [SerializeField]
    GameObject log;

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
        Instantiate(log, transform.position + new Vector3(0, 4.5f, 0), Quaternion.Euler(new Vector3(Random.Range(-5, 5), 0, 90 + Random.Range(-5, 5))));
        Destroy(gameObject);
    }

}
