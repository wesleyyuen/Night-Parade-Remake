﻿using UnityEngine;

public class Forest_Tree : MonoBehaviour {
    public Transform player;
    public Transform savePointSpawn;

    public Transform area1SpawnPoint;
    public Transform area2SpawnPoint;
    public Transform area3SpawnPoint;
    public Transform rootSpawnPoint;
    void Start () {
        GameMaster gameMaster = FindObjectOfType<GameMaster> ();
        gameMaster.UpdateCurrentScene ();

        if (gameMaster.GetPrevScene () == "_Preload" || gameMaster.GetPrevScene () == "Main_Menu") { // TODO should be "Main_Menu" instead of "Main_Menu", but _Preload is loaded for a frame for some reason
            player.position = savePointSpawn.position;
            player.localScale = new Vector3 (1f, 1f, 1f);
        } else if (gameMaster.GetPrevScene () == "Forest_Area1") {
            player.position = area1SpawnPoint.position;
            player.localScale = new Vector3 (1f, 1f, 1f);
        } else if (gameMaster.GetPrevScene () == "Forest_Area2") {
            player.position = area2SpawnPoint.position;
            player.localScale = new Vector3 (1f, 1f, 1f);
        } else if (gameMaster.GetPrevScene () == "Forest_Area3") {
            player.position = area3SpawnPoint.position;
            player.localScale = new Vector3 (-1f, 1f, 1f);
        } else if (gameMaster.GetPrevScene () == "Forest_Root") {
            player.position = rootSpawnPoint.position;
            player.localScale = new Vector3 (1f, 1f, 1f);
        }

        GetComponent<Parallax> ().FixParallaxOnLoad ();
    }
}