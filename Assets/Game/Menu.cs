using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    private void Awake()
    {
        Cursor.visible = true;
    }
    public void playButtonClicked(){
		SceneManager.LoadScene ("WorldGenTest");
	}
}
