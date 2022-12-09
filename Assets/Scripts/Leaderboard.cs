using System;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;

namespace DefaultNamespace
{
    public class Leaderboard : MonoBehaviour
    {
        private const int LEADERBOURD_MAX_COUNT = 20;
        
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private LeaderboardPlayerView _playerView;
        [SerializeField] private GameObject _loadingSlider;
        
        private string _leaderboardName = "LevelValue";

        private void OnEnable()
        {
            _loadingSlider.SetActive(true);
            GetLeaderBoardData(OnLeaderboardResponce, _leaderboardName);
        }

        private void GetLeaderBoardData(Action<List<LeaderboardData>> onComplete, string _leaderboardName)
        {
            List<LeaderboardData> data = new();
            
            Agava.YandexGames.Leaderboard.GetEntries(_leaderboardName, OnSucces, OnError, LEADERBOURD_MAX_COUNT, LEADERBOURD_MAX_COUNT, true);
            
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
        
        private void OnLeaderboardResponce(List<LeaderboardData> data)
        {
            if (gameObject == null)
                return;

            if (data == null || data.Count == 0)
            {
                _loadingSlider.SetActive(false);
            }
            else
            {
                data.ForEach(user => CreatePlayerView(user));
                _loadingSlider.SetActive(false);
            }
        }
        
        private void CreatePlayerView(LeaderboardData user)
        {
            LeaderboardPlayerView view = Instantiate(_playerView, _rectTransform);
            view.Init(user.UserName, user.ScoreValue);
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