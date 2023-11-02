using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack_CA
{
    internal class Betting
    {
        public enum Result { BlackJack, Win, Draw, Loss } //Enum for each scenario in BlackJack
        
        private double _bettingChips;
        private double _betValue;
        private string _name;
        
        public double BetValue 
        { 
            get;
            set; 
        }
        public double BettingChips
        {
      
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public Betting()
        {

        }
        //Way of getting the remaing chips after inputting the BetValue.
        public double BetCalc() //Calculate 
        {
            Console.Write("Enter Bet Amount: ");
            _betValue = double.Parse(Console.ReadLine());


            if (_betValue >= _bettingChips) //To avoid going into negative chips
            {
                _betValue = _bettingChips;
                _bettingChips = 0;
            }
            else if (_betValue < _bettingChips)
            {   
                _bettingChips = _bettingChips - _betValue;
            }
            return _bettingChips;
        }
        public void BetWinning(Result match)
        {
            if (match == Result.BlackJack) //Scenario when player gets BlackJack
            {
                double blackjack = _betValue * 2.5;
                Console.WriteLine("BlackJack!");
                _bettingChips = _bettingChips + blackjack;
                Console.WriteLine($"You won {blackjack}\nTotal is now {_bettingChips}");
            }
            else if (match == Result.Win) //Scenario when player wins
            {
                double winning = _betValue * 2;
                Console.WriteLine("Win!");
                _bettingChips = _bettingChips + winning;
                Console.WriteLine($"You won {winning}\nTotal is now {_bettingChips}");
            }
            else if (match == Result.Draw) //Scenario when player draws
            {
                double draw = _betValue;
                _bettingChips += draw;
                Console.WriteLine("Draw!");
                Console.WriteLine($"No money lost you still have {_bettingChips} chips left");
            }
            else if (match == Result.Loss) //Scenario when player loses or busts
            {
                double losing = _bettingChips - _betValue;
                Console.WriteLine("Loss!");
                Console.WriteLine($"You lost {losing}\nTotal is now {_bettingChips}");
            }
        }
        public string GetName() //Gets the username of the player
        {
            Console.Write("Enter Name: ");
            _name = Console.ReadLine();
            return _name;
        }
        public double GetTotalBettingChips() //Gets the total amount of chips
        {
            Console.Write("Enter the amount of betting chips you want to have: ");
            _bettingChips = double.Parse(Console.ReadLine());
            return _bettingChips;
        } 

        public override string ToString() //Test to see if the fields have values
        {
            return _name + " your total betting chips is " + _bettingChips;
        }
    }
}
