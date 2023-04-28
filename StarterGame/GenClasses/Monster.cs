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
        string name { get; set; }
        public void SetEyriskel(int eyr);
        public void SetPossibleItem(Item item, double chance);
        public void SetDamage(int dmg);
        public void SetHealth(int hlt);
        public void SetName(string name);
        public int MonsterAttacks(); //returns damage done by monster
        public int MonsterHurt(double damage); //returns health left on monster
        public void Die(); //returns self to be used to give player stuff and drop stuff in room
    }
    public class Monster : MonsterNPC
    {
        public Item possibleItem { get; set; }
        public double possibleChance { get; set; }
        public int Eyriskel { get; set; }
        public string name { get; set; }
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
        public void SetName(string name)
        {
            this.name = name;
        }
        public Item GetItem()
        {
            Random rnd = new Random();
            if(rnd.NextDouble() > possibleChance)
            {
                return possibleItem;
            }
            return null;
        }
    }

    public class MonsterBuilder
    {
        public virtual Monster GetMonster() { return new Monster(); }
        public virtual void BuildEyriskel() { }
        public virtual void BuildPossibleItem() { }
        public virtual void BuildDamage() { }
        public virtual void BuildHealth() { }
        public virtual void BuildName() { }

    }

    public class HospitalGhost : MonsterBuilder
    {
        private Monster monster;
        public HospitalGhost()
        {
            this.monster = new Monster();
        }

        public override void BuildDamage()
        {
            monster.SetDamage(6);
        }

        public override void BuildEyriskel()
        {
            monster.SetEyriskel(4);
        }

        public override void BuildHealth()
        {
            monster.SetHealth(15);
        }
        public override void BuildName()
        {
            monster.SetName("Ghost");
        }

        public override void BuildPossibleItem()
        {
            Random rnd = new Random();
            List<Item> itemsList = GameWorld.Floor1Items;
            int index = rnd.Next(0, itemsList.Count);
            Item item = itemsList[index];
            double percentChance = rnd.NextDouble();
            monster.SetPossibleItem(item, percentChance);
        }

        public override Monster GetMonster()
        {
            return this.monster;
        }
    }

    public class PossessedJanitor : MonsterBuilder
    {
        private Monster monster;
        public PossessedJanitor()
        {
            this.monster = new Monster();
        }

        public override void BuildDamage()
        {
            monster.SetDamage(10);
        }

        public override void BuildEyriskel()
        {
            monster.SetEyriskel(8);
        }

        public override void BuildHealth()
        {
            monster.SetHealth(25);
        }
        public override void BuildName()
        {
            monster.SetName("Evil Janitor");
        }

        public override void BuildPossibleItem()
        {
            Random rnd = new Random();
            List<Item> itemsList = GameWorld.Floor2Items;
            int index = rnd.Next(0, itemsList.Count);
            Item item = itemsList[index];
            double percentChance = rnd.NextDouble();
            monster.SetPossibleItem(item, percentChance);
        }

        public override Monster GetMonster()
        {
            return this.monster;
        }
    }

    public class HellDemon : MonsterBuilder
    {
        private Monster monster;
        public HellDemon()
        {
            this.monster = new Monster();
        }

        public override void BuildDamage()
        {
            monster.SetDamage(20);
        }

        public override void BuildEyriskel()
        {
            monster.SetEyriskel(10);
        }

        public override void BuildHealth()
        {
            monster.SetHealth(40);
        }
        public override void BuildName()
        {
            monster.SetName("Demon");
        }

        public override void BuildPossibleItem()
        {
            Random rnd = new Random();
            List<Item> itemsList = GameWorld.Floor3Items;
            int index = rnd.Next(0, itemsList.Count);
            Item item = itemsList[index];
            double percentChance = rnd.NextDouble();
            monster.SetPossibleItem(item, percentChance);
        }

        public override Monster GetMonster()
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
            this.monsterBuilder.BuildName();
        }
    }
}
