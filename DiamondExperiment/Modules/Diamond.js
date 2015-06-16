var Diamond = function () { };

String.prototype.replaceAt = function (index, character) {
    return this.substr(0, index) + character + this.substr(index + character.length);
}

Diamond.prototype.produce = function (char) {
    var base = 'A'.charCodeAt(0);
    var charCode = char.charCodeAt(0);
    var shift = charCode - base;
    var length = shift * 2 + 1;
    
    var result = new Array(length);
    
    for (var i = base; i <= charCode; i++) {
        var current = String.fromCharCode(i);
        var row = new Array(length + 1).join(' ');
        var currentShift = i - base;
        
        var leftXIndex = shift - currentShift;
        var rightXIndex = leftXIndex + currentShift * 2;
        
        var topYIndex = currentShift;
        var bottomYIndex = length - currentShift - 1;
        
        row = row.replaceAt(leftXIndex, current);
        row = row.replaceAt(rightXIndex, current);
        
        result[topYIndex] = row;
        result[bottomYIndex] = row;
    }
    
    return result.join('\n');
};

module.exports = new Diamond();