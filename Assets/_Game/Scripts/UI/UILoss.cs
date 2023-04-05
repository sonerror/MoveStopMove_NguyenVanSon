using System.Collections;
using System.Collections.Generic;
using UIExample;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILoss : UICanvas
{
    public void MenuButton()
    {
        UIManager.Ins.OpenUI<MainMenu>();
        this.CloseDirectly();
        ResetGame();
        
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
