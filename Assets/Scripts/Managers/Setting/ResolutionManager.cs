using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionManager : Singleton<ResolutionManager>
{
    Dropdown drop;

    private void Start()
    {
        drop = GetComponent<Dropdown>();
    }
    public int GetDropVal() {  return drop.value; }
}
