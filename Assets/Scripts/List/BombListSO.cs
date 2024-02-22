using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bomb/ListBombScriptableObject")]
public class BombListSO : ScriptableObject
{
    public List<BombManager> listBombSO;
}

