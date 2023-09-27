using System.Text;
using System.Xml.Serialization;

namespace CardGame;

public class Player
{
    private static int ids = 1; //счётчик игроков
    public int id { get; } //id игрока

    private List<Card> cards = new List<Card>(); //колода игрока
    public Player() => id = ids++;
    public void AddCard(Card card) => cards.Add(card);

    public Card CardChoice() //выбор карты во время раунда
    {
        Console.WriteLine(this);
        Console.WriteLine("\nВыберете карту (её порядковый номер в вашей колоде)");
        int index = Convert.ToInt32(Console.ReadLine());
        Card res;
        if (index >= cards.Count || index < 0) //если индекс введён неверно, автоматически выбирается последняя карта в колоде
        {
            res = cards[cards.Count - 1];
            cards.RemoveAt(cards.Count - 1);
        }
        else
        {
            res = cards[index-1];
            cards.RemoveAt(index-1);
        }
        return res;
    }

    public int CardsLength() => cards.Count;

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < cards.Count; i++)
            sb.Append("##############\t");
        sb.AppendLine();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < cards.Count; j++)
            {
                if (i == 1)
                {
                    sb.Append($"#  {cards[j].Name}");
                    if (cards[j].Name == "10")
                        sb.Append("        #\t");
                    else
                        sb.Append("         #\t");
                    continue;
                }
                //else if(i == 3)
                //sb.Append($"#     {cards[j].Suit}      #\t");
                else if (i == 5)
                {
                    sb.Append($"#        {cards[j].Name}");
                    if (cards[j].Name == "10")
                        sb.Append("  #\t");
                    else
                        sb.Append("   #\t");
                    continue;
                }
                else if (i == 7)
                    sb.Append("##############\t");
                else
                    sb.Append("#            #\t");
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }

}
