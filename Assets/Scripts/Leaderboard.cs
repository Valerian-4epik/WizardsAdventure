using System;
using System.Collections.Generic;
using Agava.YandexGames;
using Unity.VisualScripting;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;

namespace DefaultNamespace
{
    public class Leaderboard : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private LeaderboardPlayerView _playerView;
        
        
        private string _leaderboardName = "LevelValue";
        protected const int LeaderbourdMaxCount = 20;

        private void OnEnable()
        {
            // GetLeaderBoardData();
        }

        private void GetLeaderBoardData(Action<List<LeaderboardData>> onComplete, string _leaderboardName)
        {
            List<LeaderboardData> data = new();
            
            Agava.YandexGames.Leaderboard.GetEntries(_leaderboardName, OnSucces, OnError, LeaderbourdMaxCount, LeaderbourdMaxCount, true);
            
            void OnSucces(LeaderboardGetEntriesResponse result)
            {
                foreach (var entry in result.entries)
                {
                    string name = entry.player.publicName;

                    if (string.IsNullOrEmpty(name))
                        name = "Anonymous";

                    int score = entry.score;

                    data.Add(new LeaderboardData(name, score));
                }

                onComplete?.Invoke(data);
            }

            void OnError(string error)
            {
                onComplete?.Invoke(null);
            }
        }
        
        public void SetLeaderboardValue(string leaderboardName, int value)
        {
            Agava.YandexGames.Leaderboard.SetScore(leaderboardName, value, OnSucces, OnError);

            void OnSucces()
            {
                Debug.Log($"player's leaderboard data succesfully updated!");
            }

            void OnError(string error)
            {
                Debug.Log($"player's leaderboard data update failed");
            }
        }
    }
}

public class LeaderboardData
{
    public readonly string UserName;
    public readonly int ScoreValue;

    public LeaderboardData(string name, int value)
    {
        UserName = name;
        ScoreValue = value;
    }
}