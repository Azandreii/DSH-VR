using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine.UIElements;

public class UITextTest : MonoBehaviour
{
    public TextMeshProUGUI choice1;
    public TextMeshProUGUI choice2;
    public TextMeshProUGUI TopText;
    //public TMP_Dropdown dropdown1;
    //public Image panel1;
    //private TextMesh changeto1 = "abc";
    //private TextMeshProUGUI changeto2 = "xyz";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /* switch(dropdown1.value)
        {
            case 1:
                panel1.tintColor = Color.red; break;
            case 2:
                panel1.tintColor = Color.green; break;
            case 3:
                panel1.tintColor = Color.white; break;
        } */
    }

    public void change1()
    {
        choice1.text = "Clicked";
        choice2.text = "Option 2";
        TopText.text = "Option 1";
    }

    public void change2()
    {
        choice2.text = "Clicked";
        choice1.text = "Option 1";
        TopText.text = "Option 2";
    }

    public void reset2()
    {
        choice1.text = "Option 1";
        choice2.text = "Option 2";
        TopText.text = "test";
    }

}
