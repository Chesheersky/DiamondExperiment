var assert = require('assert');
var diamond = require('../Modules/Diamond.js');

describe('Diamond', function() {
    describe('Basic case', function (){
        it('A returns A', function () {
            var result = diamond.produce('A');
            assert.ok(result === 'A', "The only row should be A");
        })
    })

    describe('Upper part', function () {
        it('A-diamond has A in upper part', function () {
            var result = diamond.produce('A');
            var splitted = result.split('\n');

            assert.ok(splitted[0] === 'A', "The only row should be A");
        })

        it('B-diamond has A in upper part', function () {
            var result = diamond.produce('B');
            var splitted = result.split('\n');

            assert.ok(splitted[0] === ' A ', "The only row should be A");
        })

        it('C-diamond has two rows in upper part', function () {
            var result = diamond.produce('C');
            var splitted = result.split('\n');
            
            assert.ok(splitted[0] === '  A  ', "The only row should be A");
            assert.ok(splitted[1] === ' B B ', "The only row should be A");
        })
    })
    
    describe('Middle part', function () {
        it('A-diamond has A in middle part', function () {
            var result = diamond.produce('A');
            var splitted = result.split('\n');
            
            assert.ok(splitted[0] === 'A', "The only row should be A");
        })
        
        it('B-diamond has B B in middle part', function () {
            var result = diamond.produce('B');
            var splitted = result.split('\n');
            
            assert.ok(splitted[1] === 'B B', "The only row should be A");
        })
        
        it('C-diamond has C   C in middle part', function () {
            var result = diamond.produce('C');
            var splitted = result.split('\n');
            
            assert.ok(splitted[2] === 'C   C', "The only row should be C   C");
        })
    })
    
    describe('Lower part', function () {
        it('A-diamond has A in lower part', function () {
            var result = diamond.produce('A');
            var splitted = result.split('\n');
            
            assert.ok(splitted[0] === 'A', "The only row should be A");
        })
        
        it('B-diamond has A in lower part', function () {
            var result = diamond.produce('B');
            var splitted = result.split('\n');
            
            assert.ok(splitted[2] === ' A ', "The only row should be A");
        })
        
        it('C-diamond has two rows in lower part', function () {
            var result = diamond.produce('C');
            var splitted = result.split('\n');
            
            assert.ok(splitted[3] === ' B B ', "The only row should be B B");
            assert.ok(splitted[4] === '  A  ', "The only row should be A");
        })
    })
    
    describe('Upper part', function () {
        it('A-diamond has A in upper part', function () {
            var result = diamond.produce('A');
            var splitted = result.split('\n');
            
            assert.ok(splitted[0] === 'A', "The only row should be A");
        })
        
        it('B-diamond has A in upper part', function () {
            var result = diamond.produce('B');
            var splitted = result.split('\n');
            
            assert.ok(splitted[0] === ' A ', "The only row should be A");
            assert.ok(splitted[1] === 'B B', "The only row should be B B");
            assert.ok(splitted[2] === ' A ', "The only row should be A");
        })
        
        it('C-diamond has two rows in upper part', function () {
            var result = diamond.produce('C');
            var splitted = result.split('\n');
            
            assert.ok(splitted[0] === '  A  ', "The only row should be A");
            assert.ok(splitted[1] === ' B B ', "The only row should be B B");
            assert.ok(splitted[2] === 'C   C', "The only row should be C   C");
            assert.ok(splitted[3] === ' B B ', "The only row should be B B");
            assert.ok(splitted[4] === '  A  ', "The only row should be A");
        })
    })
})
