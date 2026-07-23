using FrameDataApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrameDataApp.Services
{
    public class CharacterService
    {
        private readonly List<Character> _characterList = new();
        private long _nextCharacterId = 1;

        public Character? GetCharacterById(long id)
        {
            return _characterList.FirstOrDefault(c => c.CharacterId == id);
        }

        public Character? GetCharacterByName(string characterName)
        {
            return _characterList.FirstOrDefault(c => c.Name.Equals(characterName, StringComparison.OrdinalIgnoreCase));
        }

        public List<string> GetAllCharacterNames()
        {
            return _characterList
                .Select(c => c.Name)
                .OrderBy(name => name)
                .ToList();
        }

        public bool CharacterExists(string characterName)
        {
            return _characterList.Any(c => c.Name.Equals(characterName, StringComparison.OrdinalIgnoreCase));
        }

        public bool AddCharacter(
            string characterName,
            double walkSpeed,
            double dashSpeed,
            List<Game>? gameList = null,
            List<Move>? moveList = null)
        {
            if (string.IsNullOrWhiteSpace(characterName) || CharacterExists(characterName))
            {
                return false;
            }

            var newCharacter = new Character
            {
                CharacterId = _nextCharacterId++,
                Name = characterName,
                WalkSpeed = walkSpeed,
                DashSpeed = dashSpeed,
                Games = gameList ?? new List<Game>(),
                Moves = moveList ?? new List<Move>()
            };

            _characterList.Add(newCharacter);
            return true;
        }

        public bool RemoveCharacter(string characterName)
        {
            var character = GetCharacterByName(characterName);
            if (character == null) return false;

            return _characterList.Remove(character);
        }

        public void SeedDefaultData()
        {
            AddCharacter("Ryu", walkSpeed: 0.047, dashSpeed: 0.21);
        }
    }
}