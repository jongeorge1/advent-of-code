let Area = require('./area');

class Day11Part1 {
  getMovesToCompletion(state, previousMoves) {
    previousMoves.push(state.serialize());

    if (previousMoves.length > 19) return null;

    // console.log('Recursing - previous moves: ' + previousMoves.length + '...');
    // console.log(previousMoves);
    if (state.isStateFinished) {
      // console.log('Complete');
      return 0;
    }
    
    // state.write();
    
    let allPossibleMoves = state.getPossibleMoves();
    let newMoves = allPossibleMoves.filter(el => previousMoves.indexOf(el.serialize()) === -1);
    
    if (newMoves.length === 0) {
      // console.log('No new moves');
      return null;
    }

    let distances = [];

    for (let move of newMoves) {
      let moveResult = this.getMovesToCompletion(move, previousMoves.slice());
      if (moveResult !== null) {
        // console.log(moveResult);
        distances.push(moveResult + 1);
      }
    }

    if (distances.length > 0) {
      // console.log(distances);
      let min = Math.min.apply(null, distances);
      console.log(previousMoves.length + ' - found a solution in ' + min);
      return min;
      
    }
    
    return null;
  }

  process(input) {
    let area = new Area(input);

    return this.getMovesToCompletion(area, []);
  }
}

module.exports = Day11Part1;