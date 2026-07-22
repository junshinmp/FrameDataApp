using FrameDataApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameDataApp.Services
{
    public class FrameDataService
    {
        private readonly List<Character> CharacterList = new();
        private readonly List<Game> GameList = new();

        /**
         * ------------------------ GETTER FUNCTIONS ------------------------
         */


        // CharacterList Getters:

        public List<Move> GetMovesForCharacter(string characterName)
        {
            return CharacterList.Where(c => c.Name.Equals(characterName, StringComparison.OrdinalIgnoreCase))
                                .SelectMany(c => c.Moves)
                                .ToList();
        }

        public List<Move> GetCharactersWithFastestStartup(string characterName)
        {
            // First get the character
            var character = CharacterList
                                    .FirstOrDefault(c => c.Name.Equals(characterName, StringComparison.OrdinalIgnoreCase));

            if (character == null) { return []; }

            var fastestMoves = character.Moves
                                   .Where(m => m.Stats.StartUp <= 3)
                                   .ToList();

            return fastestMoves;
        }

        public Move? GetFrameDataForMove(string characterName, string moveName)
        {
            var character = CharacterList
                .FirstOrDefault(c => c.Name.Equals(characterName, StringComparison.OrdinalIgnoreCase));

            return character?.Moves
                .FirstOrDefault(m => m.MoveName.Equals(moveName, StringComparison.OrdinalIgnoreCase));

        }

        public List<string> GetAllCharacterNames()
        {
            return CharacterList
                    .Select(c => c.Name)
                    .OrderBy(name => name)
                    .ToList();
        }

        public List<Move> GetSafeOnBlock(string characterName)
        {
            var character = CharacterList
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
            return CharacterList
                .Any(c => c.Name.Equals(characterName, StringComparison.OrdinalIgnoreCase));
        }

        public bool MoveExists(string characterName, String moveName)
        {
            if (CharacterExists(characterName))
            {
                return false;
            }

            var character = CharacterList
                .FirstOrDefault(c => c.Name.Equals(characterName, StringComparison.OrdinalIgnoreCase));
            
            if (character == null)
            {
                return false;
            }

            return character.Moves
                .Any(m => m.MoveName.Equals(moveName, StringComparison.OrdinalIgnoreCase));
        }

        // GameList Getters:

        public List<Character> GetAllCharactersFromGame(string title)
        {
            var game = GameList
                .FirstOrDefault(g => g.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (game == null)
            {
                return [];
            }

            return game.Characters;
        }

        public bool GameExists(string title)
        {
            return GameList
                .Any(g => g.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }

        /**
         * ------------------------ ALTER FUNCTIONS ------------------------
         */

        public bool AddCharacter(string characterName, double walkSpeed, double dashSpeed)
        {
            // Character already exists, or characterName is empty
            if (string.IsNullOrWhiteSpace(characterName) || CharacterExists(characterName))
            {
                return false;
            }

            var newCharacter = new Character
            {
                Name = characterName,
                WalkSpeed = walkSpeed,
                DashSpeed = dashSpeed,
            };

            CharacterList.Add(newCharacter);
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

            var character = CharacterList
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

        /**
         * ----------------------- TESTER FUNCTIONS -----------------------
         */
        public void SeedDefaultData()
        {
            // Seed Character
            AddCharacter("Ryu", walkSpeed: 0.047, dashSpeed: 0.21);

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
