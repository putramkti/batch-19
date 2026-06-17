```mermaid
classDiagram
direction TB
    class CardValue {
	    Zero
	    One
	    Two
	    Three
	    Four
	    Five
	    Six
	    Seven
	    Eight
	    Nine
	    Skip
	    Reverse
	    DrawTwo
	    Wild
	    WildDrawFour
    }

    class CardColor {
	    Red
	    Green
	    Blue
	    Yellow
    }

    class Card {
	    +CardValue Value
	    +CardColor? Color
        +int Points
	    +Card(CardColor? color, CardValue value, int points)
    }

    class ICard {
	    +CardValue Value
        +int Points
	    +CardColor? Color
    }

    class GameDirection {
	    ClockWise
	    CounterClockWise
    }

    class IDiscardPile {
        +AddCard(ICard card) void
        +ViewTopCard() ICard
        +PopAllExceptTop() List~ICard~
    }

    class IDrawPile {
        +DrawCard() ICard
        +Shuffle() void
        +IsEmpty() bool
        +Refill(List~ICard~ newCards) void

    }

    class DrawPile {
	    -List~ICard~ _cards
        +DrawPile(List~ICard~ cards)
        +DrawCard() ICard
        +Shuffle() void
        +IsEmpty() bool
        +Refill(List~ICard~ newCards) void
    }

    class DiscardPile {
	    -List~ICard~ _cards
        +DiscardPile(ICard initialCard)
        +AddCard(ICard card) void
        +ViewTopCard() ICard
        +PopAllExceptTop() List~ICard~
    }

    class IPlayer {
        +string Name
        +int Score
        %% +IReadOnlyList~ICard~ Hand
        +List~ICard~ Hand
        +AddToHand(ICard card) void
        +RemoveFromHand(ICard card) void
    }

    class Player {
        +string Name
        +int Score
        %% +IReadOnlyList~ICard~ Hand
        +List~ICard~ Hand
        +Player(string name)
        +AddToHand(ICard card) void
        +RemoveFromHand(ICard card) void
    }
    
    class GameController{
        +CardColor CurrentColor
        -List~IPlayer~ _players
        %% -Dictionary~IPlayer, List < ICard >~ _hands
        -GameDirection _direction
        -IDrawPile _drawPile
        -IDiscardPile _discardPile
        -int _currentPlayerIndex
        -List~IPlayer~ _unoPendingPlayers
        +Action~IPlayer~ OnTurnStarted
        +Action~IPlayer, ICard~ OnCardPlayed
        +Action~IPlayer~ OnUnoCalled
        +Action~IPlayer~ OnUnoPenaltyApplied
        +Action~IPlayer,int~ OnRoundEnded
        +Action~IPlayer~ OnGameEnded

        +GameController(List~IPlayer~ players, IDrawPile drawPile)
        +StartGame() void
        +PlayCard(IPlayer player, ICard card, CardColor? chosenColor) void
        +DrawCard(IPlayer player) void
        +CallUno(IPlayer player) void
        +CatchUnoViolation(IPlayer player) void

        -IsValidPlay(ICard card) bool
        -ApplyCardEffect(ICard card) void
        -NextTurn() void
        -ApplyUnoPenalty(IPlayer player) void
        -CalculateRoundScore(IPlayer winner) int
        -StartNextRound() void
        -EndGame(IPlayer winner) void
    }

	<<Enumeration>> CardValue
	<<Enumeration>> CardColor
	<<Interface>> ICard
	<<Enumeration>> GameDirection
	<<Interface>> IDiscardPile
	<<Interface>> IDrawPile
    <<Interface>> IPlayer

    Card ..|> ICard
    Card --> CardColor
    Card --> CardValue

    %% ICard ..> IDrawPile
    %% ICard --> DiscardPile

    GameController --> CardColor
    GameController --> GameDirection
    GameController --> IDrawPile
    GameController *-- IDiscardPile
    GameController o-- IPlayer
    GameController ..> ICard

    Player ..|> IPlayer
    IPlayer o-- ICard
    %% IDrawPile *-- ICard
    %% IDrawPile -- IDiscard
    %% IDiscardPile *-- ICard
    
    DiscardPile ..|> IDiscardPile
    DiscardPile o-- ICard

    %% DrawPile --> ICard
    DrawPile o-- ICard

    IDrawPile ..> ICard
    
    DrawPile ..|> IDrawPile
```