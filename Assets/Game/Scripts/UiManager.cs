using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    [SerializeField]
    private Text _ammoText;

    [SerializeField]
    private GameObject _coinInventory;

    public void UpdateAmmo(int count)
    {
        _ammoText.text = "    Ammo :" + count;
    }

    public void ShowCoin()
    {
        _coinInventory.SetActive(true);
    }

    public void HideCoin()
    {
        _coinInventory.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
