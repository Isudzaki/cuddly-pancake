using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Awake()
    {
        Invoke(nameof(CheckTiles), 5f);
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        CheckTiles();
        Debug.Log(TilesList.Instance.Tiles);
        Debug.Log(DesiredColorSetter.instance.desiredColor);
    }

    #region Check Tiles
    //Change the tiles color randomly
    public void CheckTiles()
    {
        if (TilesList.Instance.Tiles != null && DesiredColorSetter.instance.desiredColor != null)
        {
            foreach (Tile tile in TilesList.Instance.Tiles)
            {
                if (tile.TileColor.color == DesiredColorSetter.instance.desiredColor)
                {
                    agent.destination = tile.transform.position;
                }
            }
        }

        Invoke(nameof(CheckTiles), 10f);
    }
    #endregion

}
