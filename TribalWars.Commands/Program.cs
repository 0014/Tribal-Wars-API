using System;
using TribalWars.API;

namespace TribalWars.Commands
{
    class Program
    {
        private const int X = 0;
        private const int Y = 1;

        static void Main(string[] args)
        {
            string[] array = new string[14] { "Attack", "21654", "509", "355", "1", "0", "0", "0", "0", "0", "0", "0", "0", "0" };

            Attack(array);
            /*if (args[0].Equals("Attack"))
                Attack(args);
            else if (args[0].Equals("Attack")) 
                Build(args);
            else
                Console.WriteLine("Wrong command entered.");*/
        }

        private static void Attack(string[] args)
        {
            var armyInfo = new int[10];

            var command = new FarmActions(args[1]);

            var coordinates = new int[2];
            coordinates[X] = int.Parse(args[2]);
            coordinates[Y] = int.Parse(args[3]);

            for (var i = 4; i < args.Length; i++)
                armyInfo[i - 4] = int.Parse(args[i]);
            
            var army = new ArmyBuilder(armyInfo);

            command.Attack(coordinates[X], coordinates[Y], army);

            Console.WriteLine("Attack is made to " + coordinates[X] + "," + coordinates[Y]);
            Console.ReadKey();
        }

        private static void Build(string[] args)
        {

        }
    }
}
