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
        +List~ICard~ Cards
    }

    class IDrawPile {
        +List~ICard~ Cards
    }

    class DrawPile {
	    +List~ICard~ Cards
        +DrawPile(List~ICard~ cards)
    }

    class DiscardPile {
	    +List~ICard~ Cards
        +DiscardPile(ICard initialCard)
    }

    class IPlayer {
        +string Name
        +int Score
    }

    class Player {
        +string Name
        +int Score
        +Player(string name)
    }
    
    class GameController{
        -CardColor _currentColor
        -Dictionary~IPlayer, List< ICard>~ _hands
        -List~IPlayer~ _players
        -GameDirection _direction
        -IDrawPile _drawPile
        -IDiscardPile _discardPile
        -int _currentPlayerIndex
        -ICard? _drawnCardThisTurn
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
        +CatchUnoViolation(IPlayer player) bool
        +PassTurn(IPlayer player) void

        +GetCurrentPlayer() IPlayer
        +GetCurrentColor() CardColor
        +GetTopDiscardCard() ICard
        +GetPlayers() List~IPlayer~
        +GetPlayerHand(IPlayer player) List~ICard~
        +GetValidCards(IPlayer player) List~ICard~

        -IsValidPlay(ICard card) bool
        -Shuffle() void
        -RefillDrawPile() void
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

    GameController --> CardColor
    GameController --> GameDirection
    GameController --> IDrawPile
    GameController *-- IDiscardPile
    GameController o-- IPlayer
    GameController ..> ICard

    Player ..|> IPlayer
    
    IDiscardPile o-- ICard
    DiscardPile ..|> IDiscardPile

    IDrawPile o-- ICard
    
    DrawPile ..|> IDrawPile
```