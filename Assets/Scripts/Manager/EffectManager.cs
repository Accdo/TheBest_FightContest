using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private static EffectManager instance;

    [SerializeField]
    private GameObject[] Eff_Array;
    Dictionary<string, GameObject> Eff_Dic = new Dictionary<string, GameObject>();

    GameObject tempObj;

    public bool ParringComplete = false;

    public static EffectManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EffectManager>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        foreach (GameObject obj in Eff_Array)
        {
            Eff_Dic.Add(obj.name, obj);
        }
    }

    public void PlayEffect(string name)
    {
        if (Eff_Dic.ContainsKey(name) == false)
        {
            Debug.Log(name + " is not Contained Eff_Dic");
            return;
        }
        Instantiate(Eff_Dic[name], Eff_Dic[name].transform.position, Quaternion.identity);
    }

    public void PlayEffect(string name, Vector3 _vec)
    {
        if (Eff_Dic.ContainsKey(name) == false)
        {
            Debug.Log(name + " is not Contained Eff_Dic");
            return;
        }
        Instantiate(Eff_Dic[name], _vec, Quaternion.identity);  
    }

    public void PlayEffectS(string name, Vector3 _vec, float _scaleX)
    {
        if (Eff_Dic.ContainsKey(name) == false)
        {
            Debug.Log(name + " is not Contained Eff_Dic");
            return;
        }
        Eff_Dic[name].transform.localScale = new Vector3(_scaleX, 1.0f, 1.0f);
        Instantiate(Eff_Dic[name], _vec, Quaternion.identity);
    }

    public void PlayEffect(string name, Vector3 _vec, float _deathTime)
    {
        if (Eff_Dic.ContainsKey(name) == false)
        {
            Debug.Log(name + " is not Contained Eff_Dic");
            return;
        }
        tempObj = Instantiate(Eff_Dic[name], _vec, Quaternion.identity);

        Destroy(tempObj, _deathTime);
    }

    public GameObject PlayEffect_returnGameObj(string name, Vector3 _vec)
    {
        if (Eff_Dic.ContainsKey(name) == false)
        {
            Debug.Log(name + " is not Contained Eff_Dic");
            return null;
        }

        return Instantiate(Eff_Dic[name], _vec, Quaternion.identity);  
    }




    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
