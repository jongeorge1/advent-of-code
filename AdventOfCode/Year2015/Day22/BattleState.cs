namespace AdventOfCode.Year2015.Day22
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BattleState
    {
        public int NextTurnNumber { get; set; }

        public int PlayerMana { get; set; }

        public int PlayerArmour { get; set;  }

        public int PlayerSpentMana { get; set; }

        public int PlayerHitPoints { get; set; }

        public int BossHitPoints { get; set; }

        public int BossDamage { get; set; }

        public bool HardMode { get; set; } = false;

        public Dictionary<Spells, int> ActiveEffects { get; set; } = new Dictionary<Spells, int>();

        public bool IsComplete => this.PlayerHitPoints <= 0 || this.BossHitPoints <= 0;

        public bool PlayerWins => this.BossHitPoints <= 0;

        public List<string> GameLog { get; set; } = new List<string>();

        public void SpendPlayerMana(int amount)
        {
            this.PlayerMana -= amount;
            this.PlayerSpentMana += amount;
        }

        public BattleState Clone()
        {
            return new BattleState
            {
                NextTurnNumber = this.NextTurnNumber,
                PlayerMana = this.PlayerMana,
                PlayerArmour = this.PlayerArmour,
                PlayerSpentMana = this.PlayerSpentMana,
                PlayerHitPoints = this.PlayerHitPoints,
                BossHitPoints = this.BossHitPoints,
                BossDamage = this.BossDamage,
                HardMode = this.HardMode,
                ActiveEffects = new Dictionary<Spells, int>(this.ActiveEffects),
                GameLog = new List<string>(this.GameLog),
            };
        }

        public IEnumerable<BattleState> TakeTurn()
        {
            bool isPlayerTurn = this.NextTurnNumber % 2 == 1;

            var stateAfterEffects = new BattleState
            {
                NextTurnNumber = this.NextTurnNumber + 1,
                BossDamage = this.BossDamage,
                BossHitPoints = this.BossHitPoints,
                PlayerHitPoints = this.PlayerHitPoints,
                PlayerArmour = 0,
                PlayerMana = this.PlayerMana,
                PlayerSpentMana = this.PlayerSpentMana,
                HardMode = this.HardMode,
                GameLog = new List<string>(this.GameLog),
            };

            stateAfterEffects.GameLog.Add($"-- Turn {this.NextTurnNumber}: {(isPlayerTurn ? "Player" : "Boss")}");
            stateAfterEffects.GameLog.Add($"- Player has {this.PlayerHitPoints} hit points, {this.PlayerMana} mana");
            stateAfterEffects.GameLog.Add($"- Boss has {this.BossHitPoints} hit points");

            if (this.HardMode && isPlayerTurn)
            {
                stateAfterEffects.GameLog.Add($"Playing on hard mode; player loses 1 hit point");
                stateAfterEffects.PlayerHitPoints--;

                if (stateAfterEffects.PlayerHitPoints <= 0)
                {
                    stateAfterEffects.GameLog.Add($"This kills the player, and the boss wins");
                    yield return stateAfterEffects;
                    yield break;
                }
            }

            foreach (Spells spell in this.ActiveEffects.Keys)
            {
                switch (spell)
                {
                    case Spells.Shield:
                        stateAfterEffects.PlayerArmour += 7;
                        stateAfterEffects.GameLog.Add($"Shield provides 7 armour; its timer is now {this.ActiveEffects[spell] - 1}");
                        break;

                    case Spells.Poison:
                        stateAfterEffects.BossHitPoints -= 3;
                        stateAfterEffects.GameLog.Add($"Poison deals 3 damage; its timer is now {this.ActiveEffects[spell] - 1}");
                        break;

                    case Spells.Recharge:
                        stateAfterEffects.PlayerMana += 101;
                        stateAfterEffects.GameLog.Add($"Recharge provides 101 mana; its timer is now {this.ActiveEffects[spell] - 1}");
                        break;
                }

                if (this.ActiveEffects[spell] > 1)
                {
                    stateAfterEffects.ActiveEffects[spell] = this.ActiveEffects[spell] - 1;
                }
                else
                {
                    stateAfterEffects.GameLog.Add($"{spell} wears off");
                }
            }

            // Quick check; it's possible that if the player had the Poison effect, it's just killed the boss.
            // If that's the case, we're done.
            if (stateAfterEffects.IsComplete)
            {
                stateAfterEffects.GameLog.Add("Boss has been killed, and the player wins");
                yield return stateAfterEffects;
                yield break;
            }

            // Now we've processed the effects, we need to move onto the turn.
            // If the turn number is odd, it's a player turn. Otherwise it's a boss turn.
            if (!isPlayerTurn)
            {
                // Boss turn. This makes things easy; we just apply the impact of the boss turn to
                // stateAfterEffects and return it.
                int bossDamage = Math.Max(1, stateAfterEffects.BossDamage - stateAfterEffects.PlayerArmour);
                stateAfterEffects.PlayerHitPoints -= bossDamage;
                stateAfterEffects.GameLog.Add($"Boss attacks for {stateAfterEffects.BossDamage} - {stateAfterEffects.PlayerArmour} = {bossDamage} damage");
                stateAfterEffects.GameLog.Add(string.Empty);
                yield return stateAfterEffects;
                yield break;
            }

            // It's the player's turn. This is less easy. We need to look at the possible spells they could cast, and retun
            // an updated battle state for each one.
            IEnumerable<Spells> possibleSpells = Enum.GetValues<Spells>().Where(x => !stateAfterEffects.ActiveEffects.ContainsKey(x));
            foreach (Spells spell in possibleSpells)
            {
                BattleState stateAfterCastingSpell = stateAfterEffects.Clone();

                switch (spell)
                {
                    case Spells.Poison:
                        stateAfterCastingSpell.SpendPlayerMana(173);
                        stateAfterCastingSpell.ActiveEffects.Add(spell, 6);
                        stateAfterCastingSpell.GameLog.Add("Player casts Poison");
                        break;

                    case Spells.Recharge:
                        stateAfterCastingSpell.SpendPlayerMana(229);
                        stateAfterCastingSpell.ActiveEffects.Add(spell, 5);
                        stateAfterCastingSpell.GameLog.Add("Player casts Recharge");
                        break;

                    case Spells.MagicMissile:
                        stateAfterCastingSpell.SpendPlayerMana(53);
                        stateAfterCastingSpell.BossHitPoints -= 4;
                        stateAfterCastingSpell.GameLog.Add("Player casts Magic Missile, dealing 4 damage");
                        break;

                    case Spells.Shield:
                        stateAfterCastingSpell.SpendPlayerMana(113);
                        stateAfterCastingSpell.ActiveEffects.Add(spell, 6);
                        stateAfterCastingSpell.GameLog.Add("Player casts Shield");
                        break;

                    case Spells.Drain:
                        stateAfterCastingSpell.SpendPlayerMana(73);
                        stateAfterCastingSpell.BossHitPoints -= 2;
                        stateAfterCastingSpell.PlayerHitPoints += 2;
                        stateAfterCastingSpell.GameLog.Add("Player casts Drain, dealing 2 damage and healing 2 hit points");
                        break;
                }

                // If casting this spell means the player has spent more mana than they have, we skip this state
                if (stateAfterCastingSpell.PlayerMana >= 0)
                {
                    stateAfterCastingSpell.GameLog.Add(string.Empty);
                    yield return stateAfterCastingSpell;
                }
            }
        }
    }
}
