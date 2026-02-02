using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyPad : MonoBehaviour
{
    [SerializeField] TMP_Text Ans;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Number(int number)
    {
        Ans.text += number.ToString();
    }
}
