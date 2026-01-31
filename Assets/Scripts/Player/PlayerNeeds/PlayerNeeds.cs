using System.Collections.Generic;
using UnityEngine;

public class PlayerNeeds : MonoBehaviour
{
    public List<NeedsInstance> stats = new();

    void Start()
    {
        foreach (var stat in stats)
        {
            stat.Initialize();
        }
    }

    void Update()
    {
        foreach (var stat in stats)
        {
            stat.UpdateStat(Time.deltaTime);
        }
    }
}
