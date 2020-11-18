﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongList : MonoBehaviour
{
    public static SongList Instance { get; set; }
    public GameObject Content;
    public GameObject SongListButton;
    public IEnumerator Activate()
    {
        if (!PlayerData.Instance.IsSongListActive)
        {
            PlayerData.Instance.IsSongListActive = true;
            gameObject.SetActive(true);
            yield return StartCoroutine(gameObject.FadeCanvasGroup(0.2f, 20, 0.0f, 1.0f));
        }
    }
    public IEnumerator Deactivate()
    {
        if (PlayerData.Instance.IsSongListActive)
        {
            PlayerData.Instance.IsSongListActive = false;
            yield return StartCoroutine(gameObject.FadeCanvasGroup(0.2f, 20, 1.0f, 0.0f));
            gameObject.SetActive(false);
        }
    }
    public void UpdateSongList()
    {
        for (int i = Content.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(Content.transform.GetChild(i).gameObject);
        }
        List<Song> songs = SongManager.GetUniqueSongList();
        for (int i = 0; i < songs.Count; i++)
        {
            GameObject songListButton = Instantiate(SongListButton);
            songListButton.transform.SetParent(Content.transform, false);
            LayoutRebuilder.MarkLayoutForRebuild((RectTransform)Content.transform);
            songListButton.transform.Find("Title").GetComponent<Text>().text = songs[i].MetadataSection.TitleUnicode;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}