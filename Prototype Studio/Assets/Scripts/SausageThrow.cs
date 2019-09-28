using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.UI;

public class SausageThrow : MonoBehaviour
{
    
    //Public
    public GameObject Sausage;
    public float ThrowSpeed;
    public int PlayerNum;
    public float ThrowDistance;
    public GameObject ThrowTarget;
    public Text SausageText;
    
    //Private
    private int _sausageNum;
    private Rewired.Player _rewiredPlayer;
    private Vector3 _mousePos;
    private Camera cam;
    
    // Start is called before the first frame update
    void Start()
    {
        _sausageNum = 3;
        _rewiredPlayer = ReInput.players.GetPlayer(PlayerNum);
        cam = Camera.main;
        SausageText.text = "Sausages: " + _sausageNum;
    }

    // Update is called once per frame
    void Update()
    {
        _mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        var adjustedMouse = new Vector3(Mathf.Clamp(_mousePos.x, transform.position.x - ThrowDistance, transform.position.x + ThrowDistance), 
            Mathf.Clamp(_mousePos.y, transform.position.y -ThrowDistance, transform.position.y + ThrowDistance), 0);
        if (_rewiredPlayer.GetButtonDown("Throw") && _sausageNum > 0)
        {
            var target = Instantiate(Sausage);
            target.transform.position = adjustedMouse;
            _sausageNum--;
            SausageText.text = "Sausages: " + _sausageNum;
        }

        
    }

    public void AddSausage(int newSausage)
    {
        _sausageNum += newSausage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bag"))
        {
            _sausageNum++;
            SausageText.text = "Sausages: " + _sausageNum;
            Destroy(other.gameObject);
        }
    }
}
