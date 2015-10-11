var cards = {
    '2': 2,
    '3': 3,
    '4': 4,
    '5': 5,
    '6': 6,
    '7': 7,
    '8': 8,
    '9': 9,
    '10': 10,
    'J': 11,
    'Q': 12,
    'K': 13,
    'A': 14,
};

function cardify(input) {
    input = input.substring(0, input.length - 1);
    return cards[input];
}

function play(deck1, deck2) {
    var rounds = 0;
    while (deck1.length > 0 && deck2.length > 0) {
        round(deck1, deck2)
        rounds++;
    }
    return {
        winner: deck2.length == 0?1:2,
        rounds: rounds
    }
}

function round(deck1, deck2) {
    var firstWins = function (card1, card2) {
        deck1.push(card1);
        deck1.push(card2);
    }
    
    var secondWins = function (card1, card2) {
        deck2.push(card1);
        deck2.push(card2);
    }
    
    war(deck1, deck2, firstWins, secondWins);
}

function war(deck1, deck2, firstWins, secondWins) {
    var draw = function (card1, card2) {
        var bonus = [card1, card2];
        battle(bonus, deck1, deck2);
    }
    
    fight(deck1, deck2, firstWins, secondWins, draw);
}

function battle(bonus, deck1, deck2) {
    var buffer1 = [deck1.dispatch(), deck1.dispatch(), deck1.dispatch()];
    var buffer2 = [deck2.dispatch(), deck2.dispatch(), deck2.dispatch()];
    
    var firstWins = function (card1, card2) {
        var bs = accumulateBonus(bonus, buffer1, buffer2);
        bs = accumulateBonus(bs, [card1], [card2]);
        bs.forEach(function (item) { deck1.push(item); });
    }
    
    var secondWins = function (card1, card2) {
        var bs = accumulateBonus(bonus, buffer1, buffer2);
        bs = accumulateBonus(bs, [card1], [card2]);
        bs.forEach(function (item) { deck2.push(item); });
    }
    
    var draw = function (card1, card2) {
        var bs = accumulateBonus(bonus, buffer1, buffer2);
        bs = accumulateBonus(bs, [card1], [card2]);
        battle(bs, deck1, deck2);
    }
    
    fight(deck1, deck2, firstWins, secondWins, draw);
}

function fight(deck1, deck2, firstWins, secondWins, draw) {
    var card1 = deck1.dispatch();
    var card2 = deck2.dispatch();
    
    var delta = card1 - card2;
    
    if (delta == 0) {
        draw(card1, card2);
    }
    else if (delta > 0)
        firstWins(card1, card2)
    else
        secondWins(card1, card2)
}

function accumulateBonus(bonus, bonus1, bonus2) {
    var middle = bonus.length / 2;
    var left = bonus.slice(0, middle);
    var right = bonus.slice(middle);
    
    return left.concat(bonus1, right, bonus2);
}

var isPat = false;
var dispatch = function(pat) {
    var result = this.shift();
    if (result === undefined)
        isPat = true;
    return result;
}

//var decks = []
//decks[1] = [];
//var n = parseInt(readline()); // the number of cards for player 1
//for (var i = 0; i < n; i++) {
//    var cardp1 = readline(); // the n cards of player 1
//    decks[1].push(cardify(cardp1));
//}
//decks[2] = [];
//var m = parseInt(readline()); // the number of cards for player 2
//for (var i = 0; i < m; i++) {
//    var cardp2 = readline(); // the m cards of player 2
//    decks[2].push(cardify(cardp2));
//}

var decks = [];
decks[1] = [cardify('8C'), cardify('KD'), cardify('AH'), cardify('QH'), cardify('2S')];
decks[1].dispatch = dispatch;
decks[2] = [cardify('8D'), cardify('2D'), cardify('3H'), cardify('4D'), cardify('3S')];
decks[2].dispatch = dispatch;
// Write an action using print()
// To debug: printErr('Debug messages...');

var result = play(decks[1], decks[2]);

//print(result.winner == 0?'PAT':result.winner + ' ' + result.rounds);
console.log(result.winner + ' ' + result.rounds);