using System;

namespace BreedBrawlSampleLogic
{
    class Player
    {
        public string txid;
        public int health, mana, agility, stamina;
        public int criticalStrike, attackSpeed, mastery, versatility;
    }
    class Program
    {
        static Player P1 = new Player()
        {
            txid = "4dafc412bx425f",
            health = 650,
            mana = 250,
            agility = 400,
            stamina = 200,
            criticalStrike = 60,
            attackSpeed = 50,
            mastery = 40,
            versatility = 30
        };

        static Player P2 = new Player()
        {
            txid = "5cafc412bx421a",
            health = 470,
            mana = 350,
            agility = 500,
            stamina = 250,
            criticalStrike = 70,
            attackSpeed = 45,
            mastery = 45,
            versatility = 35
        };

        static Random randP1 = new Random(P1.txid.GetHashCode());
        static Random randP2 = new Random(P2.txid.GetHashCode());

        static void Main(string[] args)
        {
            // bool winner = Brawl(P1, P2);
            // Player P3 = Breed(P1, P2);
            Player P4 = Mutate(P1, 50);
        }

        //returns 0 player1 wins, 1 player2 wins
        public static bool Brawl(Player player1, Player player2)
        {
            while (player1.health > 0 && player2.health > 0)
            {
                bool criticalStrikeP1 = randP1.Next(100) > player1.criticalStrike;
                int damageP1 = (player1.agility / 7) * (1 + player1.attackSpeed / 100) * (1 - player2.versatility / 100) - randP2.Next(player2.mastery);
                damageP1 = criticalStrikeP1 ? (int)(damageP1 * 1.5) : damageP1;

                bool criticalStrikeP2 = randP2.Next(100) > player2.criticalStrike;
                int damageP2 = (player2.agility / 7) * (1 + player2.attackSpeed / 100) * (1 - player1.versatility / 100) - -(int)randP1.Next(player1.mastery);
                damageP2 = criticalStrikeP2 ? (int)(damageP2 * 1.5) : damageP2;

                Console.WriteLine("Player1 attacks for " + damageP1);
                Console.WriteLine("Player2 atacks for " + damageP2);
                player2.health -= damageP1;

                Console.WriteLine("Player2 health " + player2.health);
                //Check if P2 is finished
                if (player2.health <= 0)
                    return true;

                player1.health -= damageP2;
                Console.WriteLine("Player1 health " + player1.health);
                Console.WriteLine();
            }
            return player2.health <= 0;
        }

        public static Player Breed(Player player1, Player player2)
        {
            return new Player()
            {
                health = randP1.Next(Math.Min(player1.health, player2.health), Math.Max(player1.health, player2.health)),
                stamina = randP1.Next(Math.Min(player1.stamina, player2.stamina), Math.Max(player1.stamina, player2.stamina)),
                agility = randP1.Next(Math.Min(player1.agility, player2.agility), Math.Max(player1.agility, player2.agility)),
                mana = randP1.Next(Math.Min(player1.mana, player2.mana), Math.Max(player1.mana, player2.mana)),
                criticalStrike = randP1.Next(Math.Min(player1.criticalStrike, player2.criticalStrike), Math.Max(player1.criticalStrike, player2.criticalStrike)),
                attackSpeed = randP1.Next(Math.Min(player1.attackSpeed, player2.attackSpeed), Math.Max(player1.attackSpeed, player2.attackSpeed)),
                mastery = randP1.Next(Math.Min(player1.mastery, player2.mastery), Math.Max(player1.mastery, player2.mastery)),
                versatility = randP1.Next(Math.Min(player1.versatility, player2.versatility), Math.Max(player1.versatility, player2.versatility))
            };
        }

        public static Player Mutate(Player player, int maximalChange)
        {
            int randomIndex = randP1.Next(0, 7);
            switch (randomIndex)
            {
                case 0:
                    player.health = randP1.Next(player.health - maximalChange, player.health + maximalChange);
                    break;
                case 1:
                    player.stamina = randP1.Next(player.stamina - maximalChange, player.stamina + maximalChange);
                    break;
                case 2:
                    player.agility = randP1.Next(player.agility - maximalChange, player.agility + maximalChange);
                    break;
                case 3:
                    player.mana = randP1.Next(player.mana - maximalChange, player.mana + maximalChange);
                    break;
                case 4:
                    player.criticalStrike = randP1.Next(player.criticalStrike - maximalChange, player.criticalStrike + maximalChange);
                    break;
                case 5:
                    player.attackSpeed = randP1.Next(player.attackSpeed - maximalChange, player.attackSpeed + maximalChange);
                    break;
                case 6:
                    player.mastery = randP1.Next(player.mastery - maximalChange, player.mastery + maximalChange);
                    break;
                case 7:
                    player.versatility = randP1.Next(player.versatility - maximalChange, player.versatility + maximalChange);
                    break;
            }
            return player;
        }
    }
}
