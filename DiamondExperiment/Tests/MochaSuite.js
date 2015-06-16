var assert = require('assert');
var diamond = require('../Modules/Diamond.js');

describe('Diamond', function() {
    it('A returns A', function () {
        var result = diamond.produce('A');
        assert.ok(result === 'A', "The only row should be A");
    })

    it('B returns diamond', function() {
        var result = diamond.produce('B');
        var splitted = result.split('\n');
        
        assert.ok(splitted[0] === ' A ', "First row should be A");
        assert.ok(splitted[1] === 'B B', "Second row should be B B");
        assert.ok(splitted[2] === ' A ', "Last row should be A");
    })
})
