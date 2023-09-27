namespace CardGame;
public class Card
{
    public string Suit { get; init; }
    public string Name { get; set; }
    public int Value { get; set; }

    public Card(string suit, string name, int value)
    {
        Suit = suit;
        Name = name;
        Value = value;
    }
    //отдельный вывод карты не может быть реализован, т.к требуется вывод карт по горизонтали, а не по вертикали
}
