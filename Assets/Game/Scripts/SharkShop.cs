using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour
{

    [SerializeField]
    private AudioClip _sharkOk;

    private UiManager _uiManager;


    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                if (Input.GetKeyDown(KeyCode.E)){
                    if (player.hasCoin)
                    {
                        AudioSource.PlayClipAtPoint(_sharkOk, Camera.main.transform.position);
                        _uiManager.HideCoin();
                        player.hasCoin = false;
                        player.enableWeapon();

                    }
                    else
                    {
                        Debug.Log("!Get out Here");
                    }
                }
                
            }
        }
    }
}
