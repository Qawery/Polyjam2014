using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    private float _timeScale;
    private bool _isPaused;

    private Rect _pauseMenuRect;

	// Use this for initialization
    void Start()
    {
        var pauseMenuWidth = 300f;
        var pauseMenuHeight = 250f;
        var x = (Screen.width - pauseMenuWidth) * 0.5f;
        var y = (Screen.height - pauseMenuHeight) * 0.5f;
        _pauseMenuRect = new Rect(x, y, pauseMenuWidth, pauseMenuHeight);
    }
	
	// Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _timeScale = Time.timeScale;
            _isPaused = true;
            Time.timeScale = 0;
        }
    }

    void OnGUI()
    {
        if (_isPaused)
        {
            DisplayPauseMenu();
        }
    }

    private void DisplayPauseMenu()
    {
        float buttonSpacing = 5f;

        GUILayout.BeginArea(_pauseMenuRect);
        GUILayout.BeginVertical();

        if (MenuHelper.GUILayoutButton("Resume"))
        {
            Time.timeScale = _timeScale;
            _isPaused = false;
        }
        GUILayout.Space(buttonSpacing);
        if (MenuHelper.GUILayoutButton("Exit to menu"))
        {
            Time.timeScale = _timeScale;
            Welcome.initialMode = Welcome.Mode.Menu;
            Application.LoadLevel("welcome");
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}
