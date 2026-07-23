using FrameDataApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrameDataApp.Services
{
    public class GameService
    {
        private readonly List<Game> _gameList = new();
        private readonly CharacterService _characterService;
        private long _nextGameId = 1;

        public GameService(CharacterService characterService)
        {
            _characterService = characterService;
        }

        public bool GameExists(string title)
        {
            return _gameList.Any(g => g.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }

        public Game? GetGameByTitle(string title)
        {
            return _gameList.FirstOrDefault(g => g.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }

        public List<Character> GetAllCharactersFromGame(string title)
        {
            var game = GetGameByTitle(title);
            return game?.Characters ?? [];
        }

        public bool AddGame(string title, string developer, int releaseYear)
        {
            if (string.IsNullOrWhiteSpace(title) || GameExists(title))
            {
                return false;
            }

            _gameList.Add(new Game
            {
                gameId = _nextGameId++,
                Title = title,
                Developer = developer,
                ReleaseYear = releaseYear
            });

            return true;
        }

        public bool AddCharacterToGame(string gameTitle, string characterName)
        {
            var game = GetGameByTitle(gameTitle);
            var character = _characterService.GetCharacterByName(characterName);

            if (game == null || character == null) return false;

            if (!game.Characters.Any(c => c.CharacterId == character.CharacterId))
            {
                game.Characters.Add(character);

                if (!character.Games.Any(g => g.gameId == game.gameId))
                {
                    character.Games.Add(game);
                }
                return true;
            }

            return false;
        }

        public bool UpdateCharacterGameList(string characterName, string title)
        {
            var character = _characterService.GetCharacterByName(characterName);
            var game = GetGameByTitle(title);

            if (character == null || game == null)
            {
                return false;
            }

            bool alreadyExists = character.Games.Any(g => g.gameId == game.gameId);
            if (alreadyExists)
            {
                return false;
            }

            character.Games.Add(game);
            return true;
        }
    }
}