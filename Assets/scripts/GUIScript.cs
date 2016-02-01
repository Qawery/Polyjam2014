using UnityEngine; 
using System.Collections; 

public class GUIScript : MonoBehaviour { 
    public GUIStyle style; 
    float life; 
    GameObject player;
    Character character;
    private bool _isStyleSet;
    private GUIStyle _guiStyle;

    void Start()
    {
        player = GameObject.Find("Player");
        character = player.GetComponent<Character>();
    }

    void OnGUI() 
    {
        if (!_isStyleSet)
        {
            _guiStyle = new GUIStyle(GUI.skin.label);
            _guiStyle.fontSize = 20;
            _guiStyle.fontStyle = FontStyle.Bold;
            _guiStyle.normal.textColor = new Color(0.8f, 0.1f, 0.1f);
            _guiStyle.alignment = TextAnchor.UpperRight;
        }
        life = character.getPlayerHp();

        GUI.Label(new Rect(10, 10, 50, 40), "HP:", _guiStyle);
        GUI.Label(new Rect(60, 10, 80, 40), string.Format("{0}/{1}", life, character.maxPlayerHP), _guiStyle);

        //GUILayout.BeginArea(new Rect(Screen.width-200, 0, 5000, 500)); 
        //Tutaj można sobie forem wyświetlać jakieś fajne listy 
        //GUILayout.BeginVertical 
        //GUILayout.Label("ŻYCIE: " + life, style); 
        //Z boku pasek do przewijania 
        //GUILayout.BeginScrollView 
        //GUILayout.EndArea(); 
    } 
} 
