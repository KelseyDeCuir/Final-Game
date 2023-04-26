using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    public interface MonsterNPC
    {
        public Item possibleItem { get; set; }
        public double possibleChance { get; set; }
        int Eyriskel { get; set; }
        public void SetEyriskel(int eyr);
        public void SetPossibleItem(Item item, double chance);
        public void SetDamage(int dmg);
        public void SetHealth(int hlt);
        public int MonsterAttacks(); //returns damage done by monster
        public int MonsterHurt(double damage); //returns health left on monster
        public void Die(); //returns self to be used to give player stuff and drop stuff in room
    }
    public class Monster : MonsterNPC
    {
        public Item possibleItem { get; set; }
        public double possibleChance { get; set; }
        public int Eyriskel { get; set; }
        private int _baseDamage;
        private int _health;
        private bool _alive = true;

        public void Die()
        {
            _alive = false;
        }

        public int MonsterAttacks()
        {
            return _baseDamage;
        }

        public int MonsterHurt(double damage)
        {
            _health -= (int)Math.Ceiling(damage);
            return _health;
        }

        public void SetEyriskel(int eyr)
        {
            this.Eyriskel = eyr;
        }

        public void SetPossibleItem(Item item, double chance)
        {
            this.possibleItem = item;
            this.possibleChance = chance;
        }
        public void SetDamage(int dmg)
        {
            _baseDamage = dmg;
        }
        public void SetHealth(int hlt)
        {
            _health = hlt;
        }
    }

    public interface MonsterBuilder
    {
        public Monster GetMonster();
        public void BuildEyriskel();
        public void BuildPossibleItem();
        public void BuildDamage();
        public void BuildHealth();

    }

    public class HospitalGhost : MonsterBuilder
    {
        private Monster monster;
        public HospitalGhost()
        {
            this.monster = new Monster();
        }

        public void BuildDamage()
        {
            monster.SetDamage(6);
        }

        public void BuildEyriskel()
        {
            monster.SetEyriskel(4);
        }

        public void BuildHealth()
        {
            monster.SetHealth(15);
        }

        public void BuildPossibleItem()
        {
            Random rnd = new Random();
            List<Item> itemsList = GameWorld.Floor1Items;
            int index = rnd.Next(0, itemsList.Count);
            Item item = itemsList[index];
            double percentChance = rnd.NextDouble();
            monster.SetPossibleItem(item, percentChance);
        }

        public Monster GetMonster()
        {
            return this.monster;
        }
    }

    public class RoomMonster
    {
        private MonsterBuilder monsterBuilder;
        public RoomMonster(MonsterBuilder monsterBuilder)
        {
            this.monsterBuilder = monsterBuilder;
        }
        public Monster GetMonster()
        {
            return this.monsterBuilder.GetMonster();
        }
        public void BuildMonster()
        {
            this.monsterBuilder.BuildDamage();
            this.monsterBuilder.BuildEyriskel();
            this.monsterBuilder.BuildHealth();
            this.monsterBuilder.BuildPossibleItem();
        }
    }
}
