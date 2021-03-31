using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Player
    {
        int numberOfCard;
        List<Card> cards;
        int Sum;
        int loaiNguoi;
        public Player()
        {
            numberOfCard = 0;
            cards = new List<Card>();
            Sum = 0;
        }
        public int getLoaiNguoi()
        {
            return this.loaiNguoi;
        }
        public void setLoaiNguoi(int a)
        {
            this.loaiNguoi = a;
        }
       
        public int getNumberCard(int i=0)
        {
            numberOfCard += i;
            return numberOfCard;
        }
        public void addCard(Card temp)
        {
            cards.Add(temp);
        }
    
        public int getSum()
        {
            int sum = 0;
            foreach (Card c in cards)
            {
                if (c.getIdCard().Substring(0, c.getIdCard().Length == 2 ? 1 : 2) != "1")
                    sum += c.getValue();
            }
            Sum = sum;
            return Sum;
        }
        public int getSum(int a)
        {
            Sum += a;
            return Sum;
        }
    }
}
