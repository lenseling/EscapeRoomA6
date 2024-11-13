using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public GameObject letters;
    private Dictionary<char, Transform> letterTargets;
    private Transform cur;
    private Transform target;
    private float speed;
    private bool reached;
    private char curl;
    // Start is called before the first frame update
    void Start()
    {
        letterTargets = new Dictionary<char, Transform>();

        foreach (Transform child in letters.transform)
        {
            Debug.Log(child.gameObject.name + "\n");
            letterTargets[child.gameObject.name[0]] = child;
        }

        speed = 2.0f;
        reached = true;
        target = letterTargets['A'];
        curl = 'A';
    }

    // Update is called once per frame
    void Update()
    {
        if (!reached)
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        
        if (Input.GetKeyDown("space"))
        {
            reached = false;
            curl = curl == 'A' ? 'M' : 'A';
            target = letterTargets[curl];
        }

        if (!reached && Vector3.Distance(transform.position, target.position) < 0.01f)
            reached = true;
    }
}
