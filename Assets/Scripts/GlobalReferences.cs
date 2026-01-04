using UnityEngine;

public class GlobalReferences : MonoBehaviour

{   public static GlobalReferences Instance { get; set; }
    public GameObject bulletImpactEffectPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if(Instance !=this && Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
