using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class display : MonoBehaviour
{
    public Text obj_text;
    public InputField field;
    // Start is called before the first frame update
    void Start()
    {
        obj_text.text = PlayerPrefs.GetString("Name user");

    }

    // Update is called once per frame
    public void Creat()
    {
        obj_text.text = field.text;
        PlayerPrefs.SetString("user_name", obj_text.text);
        PlayerPrefs.Save();
    }
}
