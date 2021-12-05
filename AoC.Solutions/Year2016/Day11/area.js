let Floor = require('./floor');

class Area {
  constructor(input) {
    this.elevator = 0;
    this.floors = [];

    if (input) {
      this.floors = input.split('\r\n').map((el, i) => new Floor(i + 1, el));
    }
  }

  clone() {
    let area = new Area();
    area.elevator = this.elevator;
    area.floors = this.floors.map(el => el.clone());

    return area;
  }

  isStateValid() {
    return this.floors.reduce((acc, el) => acc && el.isStateValid(), true);
  }

  get isStateFinished() {
    return this.floors.slice(0, -1).reduce((acc, el) => acc && el.isEmpty, true);
  }

  serialize() {
    return this.elevator + '~' + this.floors.map(el => el.serialize()).join('~');
  }

  write() {
    this.floors.map((el, i) => {
      console.log(`F${el.level} ${(i === this.elevator) ? 'E' : '.' } ${ el.generators.map(x => 'G-' + x).join(' ')} ${ el.chips.map(x => 'C-' + x).join(' ')}`);
    });
  }

  getPossibleMoves() {
    let moves = [];
    
    if (this.elevator !== 0) {
      moves.push(...this.getPossibleMovesInDirection(-1));
    }

    if (this.elevator !== this.floors.length - 1) {
      moves.push(...this.getPossibleMovesInDirection(1));
    }

    return moves;
  }

  getPossibleMovesInDirection(direction) {
    let currentFloorIndex = this.elevator;
    let targetFloorIndex = this.elevator + direction;

    let movableItems = this.floors[currentFloorIndex].getAllMovableCombinations();
    let moves = [];

    movableItems.map(el => {
      let area = this.clone();
      let currentFloor = area.floors[currentFloorIndex];
      let targetFloor = area.floors[targetFloorIndex];
      area.elevator = targetFloorIndex;

      currentFloor.removeGenerators(...el.generators);
      targetFloor.addGenerators(...el.generators);

      currentFloor.removeChips(...el.chips);
      targetFloor.addChips(...el.chips);

      if (area.isStateValid()) {
        moves.push(area);
      }
    });

    return moves;
  }
}

module.exports = Area;