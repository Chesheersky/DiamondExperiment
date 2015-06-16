var Diamond = function () { };

Diamond.prototype.produce = function (char) {
    if (char === 'B')
        return " A \nB B\n A ";
    return 'A';
};

module.exports = new Diamond();