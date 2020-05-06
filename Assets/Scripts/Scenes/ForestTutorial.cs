﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestTutorial : MonoBehaviour {
    public Transform player;
    public Transform area1SpawnPoint;
    public Transform area2SpawnPoint;
    public void Start () {
        GameMaster gameMaster = FindObjectOfType<GameMaster> ();
        gameMaster.UpdateCurrentScene ();
        if (gameMaster.getPrevScene () != "") {
            foreach (GameObject obs in GameObject.FindGameObjectsWithTag("BreakableObstacle")) {
                obs.SetActive(false);
            }
        }
        if (gameMaster.getPrevScene () == "Forest_Area1") {
            player.position = area1SpawnPoint.position;
            player.localScale = new Vector3 (-1f, 1f, 1f);
        }
        else if (gameMaster.getPrevScene () == "Forest_Area2") {
            player.position = area2SpawnPoint.position;
            player.localScale = new Vector3 (-1f, 1f, 1f);
        }
    }
}