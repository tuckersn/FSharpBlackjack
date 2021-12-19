open System

type Suit =
    | Hearts
    | Diamonds 
    | Clubs 
    | Spades 
    static member list = [Hearts;Diamonds;Clubs;Spades]
    static member symbol (suit : Suit) =
        match suit with 
            | Hearts -> "♥"
            | Diamonds -> "♦"
            | Clubs -> "♣"
            | Spades -> "♠"

type Rank = 
    | Ace
    | Two
    | Three
    | Four
    | Five
    | Six
    | Seven
    | Eight
    | Nine
    | Ten
    | Jack
    | Queen
    | King
    static member list = [Ace;Two;Three;Four;Five;Six;Seven;Eight;Nine;Ten;Jack;Queen;King]
    static member value (rank : Rank) =
        match rank with
            | Ace -> 11
            | Two -> 2
            | Three -> 3
            | Four -> 4
            | Five -> 5
            | Six -> 6
            | Seven -> 7
            | Eight -> 8
            | Nine -> 9
            | Ten -> 10
            | Jack -> 10
            | Queen -> 10
            | King -> 10

type Card = 
    { 
        Suit: Suit;
        Rank: Rank
    }
    member this.value () =
        (Rank.value(this.Rank))
    static member (+) (cardA: Card, cardB: Card) =
        (cardA.value() + cardB.value())
    static member (+) (card: Card, number: int) =
        (card.value() + number)
    
let cardString(card : Card) =
    Suit.symbol(card.Suit) + card.Rank.ToString()

type Deck () = 
    member val Cards: Card list = [] with get, set
    member this.remaining () =
        this.Cards.Length
    member this.shuffle () =
        let random = Random()
        this.Cards <- this.Cards |> List.sortBy (fun _ -> random.Next())
    member this.print () =
        for card in this.Cards do
            printf "%s\t" (cardString(card))
        printf "\n"
    member this.draw () =
        if this.Cards.Length < 1 then
            this.Cards <- [
                for suit in Suit.list do
                    for rank in Rank.list do
                        yield {Suit=suit;Rank=rank}]
            this.shuffle()
        let card = this.Cards.Head
        this.Cards <- this.Cards.Tail
        card

// TODO: research active patterns
// https://stackoverflow.com/questions/3722591/pattern-matching-on-the-beginning-of-a-string-in-f
let (|Prefix|_|) (p:string) (s:string) =
    if s.StartsWith(p) then
        Some(s.Substring(p.Length))
    else
        None

type Action =
    | Hit
    | Stand
    | Quit
    | Nop
    static member parse(inputString : string) =
        if inputString.Length < 1 then
            Nop
        else
            match inputString.ToLower() with
                | Prefix "h" _ -> Hit
                | Prefix "s" _ -> Stand
                | Prefix "q" _ -> Quit
                | _ -> Nop


let blackJack (deck : Deck) = 
    let player: Card list = [deck.draw(); deck.draw()]
    let dealer: Card list = [deck.draw(); deck.draw()]

    let evaluateHand (hand : Card list) =
        let mutable highAces = 0
        let mutable sum = 0
        for i in 0..hand.Length-1 do
            sum <- hand[i] + sum
            if hand[i].Rank = Ace then
                highAces <- highAces + 1
            while highAces > 0 && sum > 21 do
                sum <- sum - 10
                highAces <- highAces - 1       
        (sum)

    let handString (hand: Card list) =
        ("(" + evaluateHand(hand).ToString() + ")").PadLeft(4) + " " + (String.concat "" (List.map (fun elem -> cardString(elem).PadRight(8)) hand))

    let displayHandsPlayerRound (player : Card list, dealer : Card list) =
        printf "PLAYER: %s\n" (handString player)
        printf "DEALER: %s******\n" (handString dealer[0..0])
    
    let displayHandsDealerRound (player : string, dealer : Card list) =
        printf "PLAYER: %s\n" player
        printf "DEALER: %s\n" (handString dealer)


    let rec dealerLoop (player: {|value : int; str : string|}, dealer: Card list, deck: Deck) =
        let value = evaluateHand dealer

        let hit () =
            let drawnCard = deck.draw()
            printf "Dealer hits! %s\n" (handString(drawnCard :: dealer))
            dealerLoop(player, (drawnCard :: dealer), deck) 

        match value with
        | _ when value > 21 -> (
            printf "Dealer busted!\n★ YOU WIN! ★\n")
        | _ when value > player.value -> (
            printf "☹ Dealer wins! ☹\n")
        | _ when value < player.value -> (
            hit ())
        | _ -> (
            if value < 15 then
                hit ()
            else
                if value = 21 then
                    printf "★ DEALER HAS BLACKJACK! ★\n"
                printf "Dealer holds!\nIT'S A TIE!\n"
        )

    let rec playerLoop (player: Card list, dealer: Card list, deck: Deck) =
        let value = evaluateHand player
        match value with
        | t when t > 21 -> (
            displayHandsDealerRound(handString player, dealer)
            printf "☹ YOU BUST! ☹\n")
        | 21 -> (
            displayHandsPlayerRound (player, dealer)
            printf "★ BLACKJACK! ★\n"
            printf ("\nDealer goes now!\n")
            dealerLoop({|
                value = 21;
                str = handString player;
            |}, dealer, deck))
        | t when t < 21 -> (
            displayHandsPlayerRound (player, dealer)
            printf "(h)it (s)tand (q)uit: "
            let userInput = Console.ReadLine().ToLower()
            match (Action.parse(userInput)) with
                | Hit -> (
                    let drawnCard = deck.draw()
                    printf "\nDrawn card: %s\n" (cardString(drawnCard))
                    playerLoop(drawnCard :: player,dealer,deck))
                | Stand -> (
                    printf ("\nDealer goes now!\n")
                    displayHandsDealerRound(handString player, dealer)
                    dealerLoop ({|
                        value = evaluateHand player;
                        str = handString player;
                    |}, dealer, deck))
                | Quit -> ()
                | Nop -> playerLoop(player,dealer,deck))


    playerLoop(player,dealer,deck)
    printf "\n------\n"


[<EntryPoint>]
let main _ = (

    let deck = Deck()
    let gameState = true
    while gameState do
        blackJack deck
        printf "Play again (y)es/no? "
        let userInput = Console.ReadLine().ToLower()
        match userInput.ToLower() with
        | Prefix "y" _ -> blackJack deck
        | _ -> ()
    
    printfn "Thanks for playing!"

    0
)
