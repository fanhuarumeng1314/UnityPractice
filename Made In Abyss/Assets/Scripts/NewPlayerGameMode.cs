using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewPlayerGameMode : MonoBehaviour {

    public static NewPlayerGameMode Instance;
    public GameObject loadingUi;
    public Slider loadingTiao;
    public GameObject sportLight;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            GameObject manage = new GameObject("manage");
            manage.AddComponent<PlayGameMode>();
            DontDestroyOnLoad(manage);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            InitStart();
            Destroy(transform.gameObject);
        }
    }

    void Start ()
    {

        InitStart();
    }

    public void InitStart()
    {
        PlayGameMode.Instance.LoadLevel();
        loadingUi = Resources.Load("MapPrefeb/CsLoading") as GameObject;
        var tmp_Ui = Instantiate(loadingUi);
        tmp_Ui.AddComponent<LoadingUi>();

        sportLight = Resources.Load("RolePreFeb/Spotlight") as GameObject;
        var tmp_Light = Instantiate(sportLight);
        tmp_Light.AddComponent<SportLight>();
    }
}
