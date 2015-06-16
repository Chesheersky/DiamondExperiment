var assert = require('assert');
var diamond = require('../Modules/Diamond.js');

suite('Diamond', function () {
    describe('Basic case', function () {
        it('should return A for A', function () {
            var result = diamond.produce('A');
            assert.equal(result, 'A');
        })
    })
    
    describe('Upper part', function () {
        it('should have A for A-diamond', function () {
            var result = diamond.produce('A');
            var splitted = result.split('\n');
            
            assert.equal(splitted[0], 'A');
        })
        
        it('should have A for B-diamond', function () {
            var result = diamond.produce('B');
            var splitted = result.split('\n');
            
            assert.equal(splitted[0], ' A ');
        })
        
        it('should have two rows for C-diamond', function () {
            var result = diamond.produce('C');
            var splitted = result.split('\n');
            
            assert.equal(splitted[0], '  A  ');
            assert.equal(splitted[1], ' B B ');
        })
    })
    
    describe('Middle part', function () {
        it('should be A for A-diamond', function () {
            var result = diamond.produce('A');
            var splitted = result.split('\n');
            
            assert.equal(splitted[0], 'A');
        })
        
        it('should have B B for B-diamond', function () {
            var result = diamond.produce('B');
            var splitted = result.split('\n');
            
            assert.equal(splitted[1], 'B B');
        })
        
        it('should have C   C for C-diamond', function () {
            var result = diamond.produce('C');
            var splitted = result.split('\n');
            
            assert.equal(splitted[2], 'C   C');
        })
    })
    
    describe('Lower part', function () {
        it('should have A for A-Diamond', function () {
            var result = diamond.produce('A');
            var splitted = result.split('\n');
            
            assert.equal(splitted[0], 'A');
        })
        
        it('should have A for B-Diamond', function () {
            var result = diamond.produce('B');
            var splitted = result.split('\n');
            
            assert.equal(splitted[2], ' A ');
        })
        
        it('should have two rows for C-Diamond', function () {
            var result = diamond.produce('C');
            var splitted = result.split('\n');
            
            assert.equal(splitted[3], ' B B ');
            assert.equal(splitted[4], '  A  ');
        })
    })
    
    describe('Complete result', function () {
        it('should be as predefined for A-diamond', function () {
            var result = diamond.produce('A');
            var splitted = result.split('\n');
            
            assert.equal(splitted[0], 'A');
        })
        
        it('should be as predefined for B-diamond', function () {
            var result = diamond.produce('B');
            var splitted = result.split('\n');
            
            assert.equal(splitted[0], ' A ');
            assert.equal(splitted[1], 'B B');
            assert.equal(splitted[2], ' A ');
        })
        
        it('should be as predefined for C-diamond', function () {
            var result = diamond.produce('C');
            var splitted = result.split('\n');
            
            assert.equal(splitted[0], '  A  ');
            assert.equal(splitted[1], ' B B ');
            assert.equal(splitted[2], 'C   C');
            assert.equal(splitted[3], ' B B ');
            assert.equal(splitted[4], '  A  ');
        })
    })
})
