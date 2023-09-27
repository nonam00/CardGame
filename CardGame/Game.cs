using System.Text;
using System.Text.Unicode;

namespace CardGame;

public class Game
{
    private static List<Card> puck = new List<Card>(); //колода

    private static string[] suits = new string[] //масти
    {
        "♣️", "♠", "♥", "♦"
    };
    private static string[] names = new string[] //буквенные названия
    {
        "J", "Q", "K", "A"
    };
    private void CardPuckInit() //инициализвания колоды карт
    {
        for (int i = 6; i <= 10; i++) //инициализация карт с числом в названии
        {
            for (int j = 0; j < 4; j++)
            {
                puck.Add(new Card(suits[j], i.ToString(), i));
            }
        }
        for (int i = 11; i <= 14; i++) //инициализвания карт с буквой в названии
        {
            for (int j = 0; j < 4; j++)
            {
                puck.Add(new Card(suits[j], names[i-11], i));
            }
        }
    }

    private List<Player> players; //список игроков
    private void Distribution() //раздача карт
    {
        Random random = new Random();
        int rndIndex;
        for(int i=0; i<players.Count; i++)
        {
            for(int j=0; j < 36/players.Count; j++) //колода равномерно распределяется по игрокам
            {
                rndIndex = random.Next(puck.Count-1);
                players[i].AddCard(puck[rndIndex]);
                puck.RemoveAt(rndIndex);
            }
        }
    }
    
    List<Card> playground = new List<Card>(); //карты выбранные игроками в раунде, индекс карты соответствует индексу игрока

    private string WinnerPrint(int index) => $"Игрок {index} выиграл";

    public Game(List<Player> _players)
    {
        CardPuckInit();
        players = _players;
        Distribution();

        int winnerIndex;
        while(true)
        {
            Console.Clear();

            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine($"Игрок {i+1}:\n");
                playground.Add(players[i].CardChoice()); //игроки выбирают карты
                Console.Clear();
            }

            winnerIndex = MaxIndex();

            for(int i=0; i<playground.Count; i++) //карты с поля переходят в колоду к выигравшему
                players[winnerIndex].AddCard(playground[i]);

            if (GameWinnerCheck(winnerIndex))
                break;

            Console.Clear();

            RemoveEmptyPlayers();

            Console.WriteLine(WinnerPrint(winnerIndex+1) + $" раунд картой {playground[winnerIndex].Name}\n"); //сообщение о том, что какой-то игрок выиграл раунд
            Thread.Sleep(3000);

            playground.Clear(); //очистка игрового поля
        }
        Console.WriteLine(WinnerPrint(winnerIndex + 1)); //сообщение о том, что какой-то игрок выиграл
    }

    private bool GameWinnerCheck(int index) => players[index].CardsLength() == 36; //если у игрока, выигравшего раунд, 36 карт, это значит, что он выиграл игру 

    private void RemoveEmptyPlayers() //удаление из списка игроков, у которых больше не осталось карт, вывод сообщения о том, что они проиграли
    {
        int prevLength = players.Count;
        for(int i=0; i<players.Count; i++)
        {
            if (players[i].CardsLength() == 0)
            {
                players.RemoveAt(i);
                Console.WriteLine($"Игрок {i + 1} проиграл");
            }
        }
        if(prevLength!=players.Count)
        {
            Thread.Sleep(3000);
            Console.Clear();
        }
    }

    private int MaxIndex() //определяет индекс игрока с лучшей картой
    {
        int max = 0;
        for(int i=1; i<playground.Count; i++)
        {
            if (playground[i].Value > playground[max].Value)
            {
                max = i;
            }
        }
        return max;
    }
}
