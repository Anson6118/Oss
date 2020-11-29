﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongSelectionButton : MonoBehaviour, ILeftClickable
{
    private bool isSelected = false;
    public Text Title;
    public Song Song { get; set; }

    public void OnLeftClick()
    {
        if (!isSelected)
        {
            UpdateMetadataDisplay();
            if (MusicPlayer.CurrentPlaying.MetadataSection.Title != Song.MetadataSection.Title)
            {
                MusicPlayer.PlaySelected(Song.MetadataSection.Title, 1.0f);
            }
            isSelected = true;
        }
        else
        {
            StartCoroutine(PlayField.Instance.SwitchToPlayField());
        }
    }

    private void UpdateMetadataDisplay()
    {
        SongSelection.Instance.SongName.text = Title.text;
        SongSelection.Instance.Mapper.text = "Mapped by " + Song.MetadataSection.Creator;
        SongSelection.Instance.Length.text = "Length: " + TimeSpan.FromSeconds(Song.GeneralSection.Length).ToString(@"m\:ss");
        SongSelection.Instance.Bpm.text = "BPM: " + (1 / Song.TimingPoints[0].BeatLength * 60000).ToString();
        SongSelection.Instance.Objects.text = "Objects: " + Song.HitObjects.Count.ToString();
        SongSelection.Instance.Circles.text = "Circles: " + Song.GeneralSection.CirclesCount.ToString();
        SongSelection.Instance.Sliders.text = "Sliders: " + Song.GeneralSection.SlidersCount.ToString();
        SongSelection.Instance.Spinners.text = "Spinners: " + Song.GeneralSection.SpinnersCount.ToString();
        SongSelection.Instance.Cs.text = "CS: " + Song.DifficultySection.CircleSize.ToString();
        SongSelection.Instance.Ar.text = "AR: " + Song.DifficultySection.ApproachRate.ToString();
        SongSelection.Instance.Od.text = "OD: " + Song.DifficultySection.OverallDifficulty.ToString();
        SongSelection.Instance.Hp.text = "HP: " + Song.DifficultySection.HPDrainRate.ToString();
        SongSelection.Instance.Stars.text = "Stars: IDK";
        SongSelection.Instance.Background.GetComponent<Image>().sprite =
            Sprite.Create
            (
                Song.Background,
                new Rect(0.0f, 0.0f, Song.Background.width, Song.Background.height),
                new Vector2(0.5f, 0.5f)
            );
    }
}
