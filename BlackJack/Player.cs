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
        public int getNumberCard()
        {
            if (numberOfCard == 5)
            {
                return -1;
            }
            numberOfCard++;
            return numberOfCard;
        }
        public void addCard(Card temp)
        {
            cards.Add(temp);
        }
        public int sum()
        {
            int sum = 0;
            foreach (Card c in cards)
            {
                if(c.getIdCard().Substring(0, c.getIdCard().Length == 2 ? 1 : 2) !="1")
                    sum += c.getValue();
            }
            Sum = sum;
            return sum;
        }
        public int sumCom()
        {
            int sum = 0;
            foreach (Card c in cards)
            {
                if (c.getIdCard().Substring(0, c.getIdCard().Length == 2 ? 1 : 2) != "1")
                    sum += c.getValue();
            }
            Sum = sum;
            return sum;
        }
        public int getSum(int a=0)
        {
            return Sum+a;
        }
    }
}
