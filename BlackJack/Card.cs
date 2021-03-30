using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Card
    {
        string id;
        public Card()
        {
            id = "";
        }
        public Card(string temp)
        {
            id = temp;
        }
        public void AddIdCard(string temp)
        {
            id = temp;
        }
        public string getIdCard()
        {
            return id;
        }
        public int getValue()
        {
            int sum=0;
            string temp = id.Substring(0, id.Length == 2 ? 1 : 2);
            switch (temp)
            {
                case "K":
                case "Q":
                case "J":
                        sum = 10;
                        break;
                default:
                    sum = int.Parse(temp);
                    break;
            }
            return sum;
        }
    }
}
