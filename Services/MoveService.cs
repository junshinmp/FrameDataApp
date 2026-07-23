using FrameDataApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrameDataApp.Services
{
    public class MoveService
    {
        private readonly CharacterService _characterService;
        public MoveService(CharacterService characterService)
        {
            _characterService = characterService;
        }

        public List<Move> GetMovesForCharacter(string characterName)
        {
            var character = _characterService.GetCharacterByName(characterName);
            return character?.Moves ?? [];
        }

        public List<Move> GetFastestStartup(string characterName)
        {
            var character = _characterService.GetCharacterByName(characterName);
            if (character == null) { return []; }

            return character.Moves
                .Where(m => m.Stats != null && m.Stats.StartUp <= 3)
                .ToList();
        }

        public Move? GetFrameDataForMove(string characterName, string moveName)
        {
            var character = _characterService.GetCharacterByName(characterName);
            return character?.Moves
                .FirstOrDefault(m => m.MoveName.Equals(moveName, StringComparison.OrdinalIgnoreCase));
        }

        public List<Move> GetSafeOnBlock(string characterName)
        {
            var character = _characterService.GetCharacterByName(characterName);
            if (character == null) { return []; }

            return character.Moves
                .Where(m => m.Stats != null && m.Stats.OnBlock >= -3)
                .OrderBy(m => m.Stats.StartUp)
                .ToList();
        }

        public bool MoveExists(string characterName, string moveName)
        {
            var character = _characterService.GetCharacterByName(characterName);
            return character?.Moves
                .Any(m => m.MoveName.Equals(moveName, StringComparison.OrdinalIgnoreCase)) ?? false;
        }

        public bool AddMoveToCharacter(
            string characterName,
            string moveName,
            string commandInput,
            FrameData frameData,
            List<CancelType>? cancelTypes = null)
        {
            if (string.IsNullOrWhiteSpace(characterName) ||
                string.IsNullOrWhiteSpace(moveName) ||
                string.IsNullOrWhiteSpace(commandInput))
            {
                return false;
            }

            if (MoveExists(characterName, moveName))
            {
                return false;
            }

            var character = _characterService.GetCharacterByName(characterName);
            if (character == null)
            {
                return false;
            }

            var newMove = new Move
            {
                CharacterId = (int)character.CharacterId,
                MoveName = moveName,
                CommandInput = commandInput,
                Stats = frameData,
                Cancelable = cancelTypes ?? new List<CancelType>()
            };

            character.Moves.Add(newMove);
            return true;
        }

        public bool RemoveMove(string characterName, string moveName)
        {
            var character = _characterService.GetCharacterByName(characterName);
            if (character == null) return false;

            var move = character.Moves.FirstOrDefault(m => m.MoveName.Equals(moveName, StringComparison.OrdinalIgnoreCase));
            if (move == null) return false;

            return character.Moves.Remove(move);
        }

        public bool UpdateMoveFrameData(string characterName, string moveName, FrameData updatedStats)
        {
            var move = GetFrameDataForMove(characterName, moveName);
            if (move == null || updatedStats == null) return false;

            move.Stats = updatedStats;
            return true;
        }

        public void SeedDefaultMoveData()
        {
            var hadokenStats = new FrameData
            {
                StartUp = 14,
                Active = 2,
                Recovery = 42,
                OnBlock = -6
            };

            AddMoveToCharacter(
                characterName: "Ryu",
                moveName: "Heavy Hadoken",
                commandInput: "236HP",
                frameData: hadokenStats,
                cancelTypes: new List<CancelType> { CancelType.SuperCancelable }
            );
        }
    }
}