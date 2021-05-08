using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class BoBai
    {
        List<Card> removeListCards;
        public BoBai()
        {
            removeListCards = new List<Card>();
        }
        private string RandomId()
        {
            string temp="";
            Random random = new Random();
            int i = random.Next(1, 14);
            switch (i)
            {
                case 11:
                    temp = "J";
                    break;
                case 12:
                    temp = "Q";
                    break;
                case 13:
                    temp = "K";
                    break;
                default:
                    temp = i.ToString();
                    break;
            }
            i = random.Next(1, 5);
            switch(i){
                case 1:
                    temp += "R"; //cơ
                    break;
                case 2:
                    temp += "B"; //bích
                    break;
                case 3:
                    temp += "H";// chuồn
                    break;
                case 4:
                    temp += "C";//rô
                    break;
            }
            return temp;
        }
        private bool AddRemoveList(string id)
        {
            foreach(Card c in removeListCards)
            {
                if (c.getIdCard() == id)
                    return false;
            }
            return true;
        }
        public Card getCard()
        {
            string id = "";
            do
            {
                id = RandomId();
            } while (AddRemoveList(id) != true);
            Card card = new Card(id);
            this.removeListCards.Add(card);
            return card;
        }
        public void resetRomeveList()
        {
            removeListCards.Clear();
        }
    }
}
