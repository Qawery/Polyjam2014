using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MenuHelper : MonoBehaviour
{
    private bool _isInitialized;

    private static GUIStyle _styleButton;
    private static GUILayoutOption[] _optionsMenuButton;

    public static bool GUILayoutButton(string text)
    {
        return GUILayout.Button(text, _styleButton, _optionsMenuButton);
    }

    void OnGUI()
    {
        if (!_isInitialized)
        {
            Initialize();
        }
    }

    private void Initialize()
    {
        if (_isInitialized)
        {
            return;
        }

        _styleButton = new GUIStyle(GUI.skin.button);
        _styleButton.fontStyle = FontStyle.Bold;
        _styleButton.fontSize = 18;
        _styleButton.normal.textColor = Color.gray;

        _optionsMenuButton = new GUILayoutOption[]
        {
            GUILayout.Height(45f)
        };

        _isInitialized = true;
    }
}
