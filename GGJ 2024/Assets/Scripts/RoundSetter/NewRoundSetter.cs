using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;

public sealed class NewRoundSetter : MonoBehaviour
{
    #region Serialized Vars
    [Header("Desired Color Setter")]
    [SerializeField] private DesiredColorSetter desColorSetter;
    [Header("Respawn Timer")]
    [SerializeField] private RespawnTimer respawnTimer;
    [Header("Player Spawner")]
    [SerializeField] private PlayerSpawn playerSpawn;
    [Header("Item Spawn")]
    [SerializeField] private ItemSpawn itemSpawn;
    [Header("Loose Screen")]
    [SerializeField] private GameObject looseScreen;
    [Header("Mixer Groups")]
    [SerializeField] private AudioMixerGroup[] mixerGroups;
    [Header("Audio")]
    [SerializeField] private AudioSource newRoundAudio;
    #endregion

    #region Set Tiles
    //Change the tiles color randomly
    public void SetTiles()
    {
        newRoundAudio.Play();
        if (StartTimer.Instance.isTimeOver != true) return;
        foreach (Tile tile in TilesList.Instance.Tiles)
        {
            Transform tilePos = tile.transform;

            tile.transform.position = new Vector3(tilePos.position.x, 0, tilePos.position.z);
            tile.TileColor = ColorDatabase.Colors[Random.Range(0, ColorDatabase.Colors.Length)];
        }
        CheckPlayer();
        CheckEnemies();
        itemSpawn.SpawnItem();
        //Checking all tiles for desired color ones
        Invoke(nameof(CheckTiles), 0.05f);
    }
    #endregion

    #region Check Tiles
    //Check's all tiles for desired color ones and assign one randomly if it's need
    private void CheckTiles()
    {
        List<Tile> tilesWithDesiredColor = TilesList.Instance.Tiles.FindAll(tile => tile.TileColor.color == desColorSetter.desiredColor);

        if (tilesWithDesiredColor.Count == 0)
        {
            // If no tiles with the desired color, select a random tile and change its color
            if (TilesList.Instance.Tiles.Count > 0)
            {
                Tile randomTile = TilesList.Instance.Tiles[Random.Range(0, TilesList.Instance.Tiles.Count)];
                foreach(TileColor tileColor in ColorDatabase.Colors)
                {
                    if (tileColor.num == desColorSetter.desiredNum)
                        randomTile.TileColor = tileColor;
                }
            }
        }
        EnemyAI[] enemyAIs = FindObjectsOfType<EnemyAI>();
        foreach (EnemyAI enemy in enemyAIs)
        {
            enemy.CheckTiles();
        }
    }
    #endregion

    #region Check Player
    //Check's if the player is dead and if yes respawn
    private void CheckPlayer()
    {
        if (respawnTimer.canRespawn)
        {
            looseScreen.SetActive(false);
            playerSpawn.RespawnPlayer();
            respawnTimer.canRespawn = false;
            for (int i = 0; i < mixerGroups.Length; i++)
            {
                mixerGroups[i].audioMixer.DOSetFloat("LowPass", 10000,1);
            }
        }

    }
    #endregion

    #region Check Enemies
    //Check's if the enemies are dead and if yes respawn
    private void CheckEnemies()
    {
        EnemyRespawn[] enemyRespawns = FindObjectsOfType<EnemyRespawn>();
        foreach(EnemyRespawn enemyRespawn in enemyRespawns)
        {
            if (enemyRespawn.canRespawn)
            {
                playerSpawn.RespawnEnemy(enemyRespawn.enemy);
                enemyRespawn.canRespawn = false;
            }
        }

    }
    #endregion

    #region Start Round
    public void StartRound()
    {
        InvokeRepeating(nameof(SetTiles), 0, 10f);
    }
    #endregion
}
