namespace FlashCards.Models;

public class Stack
{
    public int Id { get; set; }
    public string Theme { get; set; }
}

public class StackCardsDTO
{
    public string Theme { get; set; }
    public List<CardDTO> CardsDTO { get; set; }
}
