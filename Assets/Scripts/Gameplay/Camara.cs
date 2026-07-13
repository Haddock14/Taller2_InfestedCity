using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    private Transform camaraTransform;
    private Transform jugadorTransform;

    // Start is called before the first frame update
    void Start()
    {
        camaraTransform = GetComponent<Transform>();
        jugadorTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Mover();
    }

    void Mover()
    {
        camaraTransform.position = new Vector3(camaraTransform.position.x, camaraTransform.position.y, jugadorTransform.position.z - 2.5f);
    }
}
