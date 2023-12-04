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
    public TMP_Dropdown dropdown1;
    public PanelSettings panel1;
    //private TextMesh changeto1 = "abc";
    //private TextMeshProUGUI changeto2 = "xyz";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(dropdown1.value)
        {
            case 1:
                panel1.colorClearValue = Color.red; break;
            case 2:
                panel1.colorClearValue = Color.green; break;
            case 3:
                panel1.colorClearValue = Color.white; break;
        }
    }

    public void change1()
    {
        choice1.text = "abc";
        TopText.text = "test2";
    }

    public void change2()
    {
        choice2.text = "xyz";
        TopText.text = "test2";
    }

    public void reset()
    {
        choice1.text = "Option 1";
        choice2.text = "Option 2";
        TopText.text = "test";
    }

}
