using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedConveyor : MonoBehaviour
{
    public float SpeedX => _speedX;
    [SerializeField] private float _speedX = 0.1f;

    public float SpeedY => _speedY;
    [SerializeField] private float _speedY = 0.1f;
    
    private float curX, curY;

    void Start()
    {
        curX = GetComponent<Renderer>().material.mainTextureOffset.x;
        curY = GetComponent<Renderer>().material.mainTextureOffset.y;
    }

    void FixedUpdate()
    {
        curX += Time.deltaTime * _speedX;
        curY += Time.deltaTime * _speedX;
        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(curX, curY));
    }
}
