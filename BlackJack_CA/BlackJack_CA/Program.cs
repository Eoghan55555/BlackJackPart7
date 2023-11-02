using System.Security.Cryptography.X509Certificates;

namespace BlackJack_CA
{
    internal class Program
    {
        static Card Player1 = new Card(); 
        static Card Dealer = new Card();
        static Betting _Player1 = new Betting();
        static int p_num_ofcards = 0;//Count the num of cards a player would have
        static int d_num_ofcards = 0;//Count the num of cards a dealer would have
        static void Main(string[] args)
        {
            int[] compareValues = new int[2];

            Player1.DeckReshuffle();//Shuffles Deck

            _Player1.GetName();//Start of the Final Draft
            _Player1.GetTotalBettingChips();
            _Player1.BetCalc();
            int[] handsValue = cardsDrawn();
            if (handsValue[0] != 21 || handsValue[1] != 21) //Game will continue if there is no BlackJack
            {
                compareValues[0] = PlayerPlays(handsValue[0]);
                compareValues[1] = DealerPlays(handsValue[1]);
            }

            GetResult(compareValues);
        }
        static int[] cardsDrawn()
        {
            //Variables
            string card;
            int cardValue;


            int[] handsvalue=new int[2];//Player is the value and the Dealer is the second value 


            //Player goes first
            while (p_num_ofcards <= 1) //Gets the first two cards for the player
            {
                card = Player1.GetCard();//Gets the card as a string
                cardValue = Player1.GetCardValue(card);//Turns the string value into a number
  
                Console.WriteLine($"You have drawn {card} which is worth {cardValue}");
                handsvalue[0] += cardValue;

                p_num_ofcards++;
            }
            Console.WriteLine($"Your total is {handsvalue[0]}\n");


            //Dealer goes next
            while (d_num_ofcards <= 1) //Gets the first two cards for the dealer
            {
                card = Dealer.GetCard();//Gets the card as a string
                cardValue = Dealer.GetCardValue(card);//Turns the string value into a number
                
                Console.WriteLine($"Dealer has drawn {card} which is worth {cardValue}");
                handsvalue[1] += cardValue;

                d_num_ofcards++;
            }
            Console.WriteLine($"Dealer's total is {handsvalue[1]}\n");


            return handsvalue;
        }
        static int PlayerPlays(int playersHandValue)
        {
            //Variables
            string input = "";
            string lowercaseinput="";
            string card;
            int cardValue;
;
            
            while (lowercaseinput != "stay")
            {

                if (lowercaseinput == "stay")
                {
                    break;
                }

                card = Player1.GetCard();
                cardValue = Player1.GetCardValue(card);
                p_num_ofcards++;
                Console.WriteLine($"You have drawn {card} which is worth {cardValue}");
                
                playersHandValue+= cardValue;
                Console.WriteLine($"Total is now {playersHandValue}\n");

                if (playersHandValue >= 21) //Second check to see if the reached to or past 21
                {
                    break;
                }

                Console.Write("Do you want to stay: ");
                input=Console.ReadLine();
                lowercaseinput = input.ToLower();


            } 
            
            return playersHandValue;
        }

        static int DealerPlays(int dealersHandValue)
        {
            while (dealersHandValue < 17)
            {
                string card = Dealer.GetCard();//Gets the card as a string
                int cardValue = Dealer.GetCardValue(card);//Turns the string value into a number
                d_num_ofcards++;
                Console.WriteLine($"\nDealer has drawn {card} which is worth {cardValue}");
                dealersHandValue += cardValue;
                Console.WriteLine($"Dealer's Total is now {dealersHandValue}\n");
            }
            return dealersHandValue;
        }
        static void GetResult(int[] handsValue)//0 is the player and 1 is the dealer
        {
            //Loss Scenarios
            if (handsValue[0] > 21) //Player Busts
            {
                _Player1.BetWinning(Betting.Result.Loss);
            }
            else if (handsValue[0] < handsValue[1] && handsValue[1] <= 21) //Dealer is below 22 and has more than the player
            {
                _Player1.BetWinning(Betting.Result.Loss);
            }
            //Win Scenarios
            else if (handsValue[1] > 21)  //Player is Below 22 and Dealer Busts
            {
                _Player1.BetWinning(Betting.Result.Win);
            }
            else if (handsValue[0] > handsValue[1]) //Player has more than Dealer
            {
                _Player1.BetWinning(Betting.Result.Win);
            }
            //Draw Scenarios
            else if (handsValue[0] == handsValue[1]) //Player and Dealer has the same amount
            {
                _Player1.BetWinning(Betting.Result.Draw);
            }
            //Player gets BlackJack
            else if (handsValue[0] == 21 && p_num_ofcards == 2)  //Player gets a natural 21
            {
                _Player1.BetWinning(Betting.Result.BlackJack);
            }
        }
    }
}