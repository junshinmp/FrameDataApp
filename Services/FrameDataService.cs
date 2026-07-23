using FrameDataApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameDataApp.Services
{
    public class FrameDataService
    {
        private readonly List<Character> _characterList = new();
        private readonly List<Game> _gameList = new();

        /**
         * ==================================================================
         * ------------------------ GETTER FUNCTIONS ------------------------
         * ==================================================================
         */

        // -------------------- _characterList Getters: ---------------------

        public List<Move> GetMovesForCharacter(string characterName)
        {
            return _characterList
                .Where(c => c.Name.Equals(characterName, StringComparison.OrdinalIgnoreCase))
                .SelectMany(c => c.Moves)
                .ToList();
        }

        public List<Move> GetFastestStartup(string characterName)
        {
            // First get the character
            var character = _characterList
                .FirstOrDefault(c => c.Name.Equals(characterName, StringComparison.OrdinalIgnoreCase));

            if (character == null) { return []; }

            var fastestMoves = character.Moves
                .Where(m => m.Stats != null && m.Stats.StartUp <= 3)
                .ToList();

            return fastestMoves;
        }

        public Move? GetFrameDataForMove(string characterName, string moveName)
        {
            var character = _characterList
                .FirstOrDefault(c => c.Name.Equals(characterName, StringComparison.OrdinalIgnoreCase));

            return character?.Moves
                .FirstOrDefault(m => m.MoveName.Equals(moveName, StringComparison.OrdinalIgnoreCase));

        }

        public List<string> GetAllCharacterNames()
        {
            return _characterList
                .Select(c => c.Name)
                .OrderBy(name => name)
                .ToList();
        }

        public List<Move> GetSafeOnBlock(string characterName)
        {
            var character = _characterList
                .FirstOrDefault(c => c.Name.Equals(characterName, StringComparison.OrdinalIgnoreCase));

            if (character == null)
            {
                return [];
            }

            return character.Moves
                .Where(m => m.Stats != null && m.Stats.OnBlock >= -3)
                .OrderBy(m => m.Stats.StartUp)
                .ToList();
                
        }

        public bool CharacterExists(string characterName)
        {
            return _characterList
                .Any(c => c.Name.Equals(characterName, StringComparison.OrdinalIgnoreCase));
        }

        public bool MoveExists(string characterName, string moveName)
        {
            if (!CharacterExists(characterName))
            {
                return false;
            }

            var character = _characterList
                .FirstOrDefault(c => c.Name.Equals(characterName, StringComparison.OrdinalIgnoreCase));
            
            if (character == null)
            {
                return false;
            }

            return character.Moves
                .Any(m => m.MoveName.Equals(moveName, StringComparison.OrdinalIgnoreCase));
        }


        // ---------------------- _gameList Getters: ----------------------

        public List<Character> GetAllCharactersFromGame(string title)
        {
            var game = _gameList
                .FirstOrDefault(g => g.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (game == null)
            {
                return [];
            }

            return game.Characters;
        }

        public bool GameExists(string title)
        {
            return _gameList
                .Any(g => g.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }

        /**
         * ==================================================================
         * ----------------------- UPDATE FUNCTIONS -------------------------
         * ==================================================================
         */

        // ------------------------- ADDING ELEMENTS ------------------------
        public bool AddCharacter(
            string characterName, 
            double walkSpeed, 
            double dashSpeed,
            List<Game> gameList,
            List<Move> moveList)
        {
            // Character already exists, or characterName is empty
            if (string.IsNullOrWhiteSpace(characterName) || CharacterExists(characterName))
            {
                return false;
            }

            if (gameList == null)
            {
                gameList = new List<Game>();
            }

            if (moveList == null)
            {
                moveList = new List<Move>();
            }

            var newCharacter = new Character
            {
                Name = characterName,
                WalkSpeed = walkSpeed,
                DashSpeed = dashSpeed,
                Games = gameList,
                Moves = moveList
            };

            _characterList.Add(newCharacter);
            return true;
        }

        public bool AddMoveToCharacter(
            string characterName, 
            string moveName,
            string commandInput,
            FrameData frameData,
            CancelType cancelType)
        {

            // MoveName, CharacterName or inputCommand is empty.
            if (string.IsNullOrWhiteSpace(characterName) ||
                string.IsNullOrWhiteSpace(moveName) ||
                string.IsNullOrWhiteSpace(commandInput))
            {
                return false;
            }

            // If move with the same name already exists
            if (MoveExists(characterName, moveName))
            {
                return false;
            }

            var character = _characterList
                .FirstOrDefault(c => c.Name.Equals(characterName, StringComparison.OrdinalIgnoreCase)); 

            if (character == null)
            {
                return false;
            }

            var newMove = new Move
            {
                MoveName = moveName,
                CommandInput = commandInput,
                Stats = frameData,
                Cancelable = cancelType
            };

            character.Moves.Add(newMove);
            return true;
        }

        public bool AddGame(string title, string developer, int releaseYear)
        {
            if (string.IsNullOrWhiteSpace(title) || GameExists(title))
            {
                return false;
            }

            _gameList.Add(new Game
            {
                Title = title,
                Developer = developer,
                ReleaseYear = releaseYear
            });

            return true;
        }

        public bool AddCharacterToGame(string gameTitle, string characterName)
        {
            var game = _gameList.FirstOrDefault(g => g.Title.Equals(gameTitle, StringComparison.OrdinalIgnoreCase));
            var character = _characterList.FirstOrDefault(c => c.Name.Equals(characterName, StringComparison.OrdinalIgnoreCase));

            if (game == null || character == null) return false;

            if (!game.Characters.Any(c => c.Name.Equals(characterName, StringComparison.OrdinalIgnoreCase)))
            {
                game.Characters.Add(character);

                if (!character.Games.Any(g => g.Title.Equals(gameTitle, StringComparison.OrdinalIgnoreCase)))
                {
                    character.Games.Add(game);
                }
                return true;
            }

            return false;
        }

        // ------------------------- REMOVE ELEMENTS ------------------------

        public bool RemoveCharacter(string characterName)
        {
            var character = _characterList.FirstOrDefault(c => c.Name.Equals(characterName, StringComparison.OrdinalIgnoreCase));
            if (character == null) return false;

            return _characterList.Remove(character);
        }

        public bool RemoveMove(string characterName, string moveName)
        {
            var character = _characterList
                                .FirstOrDefault(c => c.Name.Equals(characterName, StringComparison.OrdinalIgnoreCase));

            if (character == null)
            {
                return false;
            }

            var move = character.Moves.FirstOrDefault(m => m.MoveName.Equals(moveName, StringComparison.OrdinalIgnoreCase));
            if (move == null)
            {
                return false;
            }

            return character.Moves.Remove(move);
        }

        // ------------------------- UPDATE ELEMENTS ------------------------
        public bool UpdateMoveFrameData(string characterName, string moveName, FrameData updatedStats)
        {
            var move = GetFrameDataForMove(characterName, moveName);
            if (move == null || updatedStats == null) return false;

            move.Stats = updatedStats;
            return true;
        }

        public bool UpdateCharacterGameList(string characterName, string title)
        {
            // Check if this character exists.
            var character = _characterList
                .FirstOrDefault(c => c.Name.Equals(characterName, StringComparison.OrdinalIgnoreCase));

            if (character == null)
            {
                return false;
            }
            
            // Check if this title exists.
            var game = _gameList
                .FirstOrDefault(g => g.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            
            if (game == null)
            {
                return false;
            }

            // Check if game already added to character
            bool alreadyExists = character.Games
                .Any(g => g.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (alreadyExists)
            {
                return false;
            }

            character.Games.Add(game);
            return true;
        }

        /**
         * ----------------------- TESTER FUNCTIONS -----------------------
         */

        public void SeedDefaultData()
        {
            // Seed Character
            AddCharacter("Ryu", walkSpeed: 0.047, dashSpeed: 0.21, [], []);

            // Create FrameData stats
            var hadokenStats = new FrameData
            {
                StartUp = 14,
                Active = 2,
                Recovery = 42,
                OnBlock = -6
            };

            // Attach Move
            AddMoveToCharacter(
                characterName: "Ryu",
                moveName: "Heavy Hadoken",
                commandInput: "236HP",
                frameData: hadokenStats,
                cancelType: CancelType.SuperCancelable // assuming CancelType is an enum or class
            );
        }

    }
}
