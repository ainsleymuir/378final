using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI ammoText;
    public int ammoCount;
    // Start is called before the first frame update
    void Start()
    {
        ammoText.text = ammoCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addAmmo() {
        ammoCount += 1;
        ammoText.text = ammoCount.ToString();
    }

    public void decreaseAmmo() {
        ammoCount -= 1;
        ammoText.text = ammoCount.ToString();
    }
}
