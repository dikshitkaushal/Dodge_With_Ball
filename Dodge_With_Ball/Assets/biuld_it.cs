using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class biuld_it : MonoBehaviour
{
    public GameObject cube;
    public GameObject sphere;
    public Material red;
    public Material green;

    Perceptron p;
    // Start is called before the first frame update
    void Start()
    {
        p = GetComponent<Perceptron>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameObject g = Instantiate(cube, Camera.main.transform.position, Camera.main.transform.rotation);
            g.GetComponent<Renderer>().material = red;
            g.GetComponent<Rigidbody>().AddForce(0, 0, 500f);
            p.getinput(0, 1, 0);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameObject g = Instantiate(cube, Camera.main.transform.position, Camera.main.transform.rotation);
            g.GetComponent<Renderer>().material = green;
            g.GetComponent<Rigidbody>().AddForce(0, 0, 500f);
            p.getinput(0, 0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameObject g = Instantiate(sphere, Camera.main.transform.position, Camera.main.transform.rotation);
            g.GetComponent<Renderer>().material = green;
            g.GetComponent<Rigidbody>().AddForce(0, 0, 500f);
            p.getinput(1, 0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GameObject g = Instantiate(sphere, Camera.main.transform.position, Camera.main.transform.rotation);
            g.GetComponent<Renderer>().material = red;
            g.GetComponent<Rigidbody>().AddForce(0, 0, 500f);
            p.getinput(1, 1, 1);
        }
    }
}
