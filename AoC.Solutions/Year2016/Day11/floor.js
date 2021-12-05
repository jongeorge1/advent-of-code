class Floor {
  constructor(level, input) {
    this.level = level;
    this.chips = [];
    this.generators = [];

    if (input) {
      this.parse(input);
    }
  }

  clone() {
    let newFloor = new Floor(this.level);
    newFloor.chips = this.chips.slice();
    newFloor.generators = this.generators.slice();

    return newFloor;
  }

  parse(input) {
    if (input.indexOf('relevant.') !== -1) {
      return;
    }

    let components = input.split(',');
    components.map(el => this.parseInputComponent(el));
  }

  get isEmpty() {
    return this.chips.length === 0 && this.generators.length === 0;
  }

  addChips() {
    this.chips.push(...arguments);
    this.chips.sort();
  }

  removeChips() {
    let a = Array.from(arguments);
    a.map(el => {
      let i = this.chips.indexOf(el);
      if (i !== -1) this.chips.splice(i, 1);
    });
  }

  removeGenerators() {
    let a = Array.from(arguments);
    a.map(el => {
      let i = this.generators.indexOf(el);
      if (i !== -1) this.generators.splice(i, 1);
    });
  }

  addGenerators() {
    this.generators.push(...arguments);
    this.generators.sort();
  }

  parseInputComponent(input) {
    let components = input.split(' ');
    let element = components[components.length - 2].replace('-compatible', '');
    let device = components[components.length - 1].replace('.', '');
    element;
    if (device === 'generator') {
      this.generators.push(element);
    } else {
      this.chips.push(element);
    }
  }

  isStateValid() {
    if (this.generators.length === 0 || this.chips.length === 0) {
      return true;
    }

    // Need to see if there's a chip without it's generator
    return this.chips.reduce((acc, el) => acc && this.generators.indexOf(el) !== -1, true);
  }

  serialize() {
    return `${this.level}|${this.generators.join(',')}|${this.chips.join(',')}`;
  }

  buildCombo(chips, generators) {
    let combo = {
      chips: chips || [],
      generators: generators || []
    };

    combo.chips.sort();
    combo.generators.sort();

    combo.id = combo.chips.map(el => 'c' + el).join(',') + combo.generators.map(el => 'g' + el).join(',');
    
    return combo;
  }

  getAllMovableCombinations() {
    let combos = [];

    combos.push(...this.chips.map(el => this.buildCombo([el], [])));
    combos.push(...this.generators.map(el => this.buildCombo([], [el])));

    // Now all combinations of chips, and chips and generators
    this.chips.map(el1 => {
      this.chips.map(el2 => {
        if (el1 !== el2) {
          combos.push(this.buildCombo([el1, el2], []));
        }
      });

      this.generators.map(el2 => {
        combos.push(this.buildCombo([el1], [el2]));
      });
    });

    // Now all combinations of generators
    this.generators.map(el1 => {
      this.generators.map(el2 => {
        if (el1 !== el2) {
          combos.push(this.buildCombo([], [el1, el2]));
        }
      });
    });

    // Finally, filter them down
    let uniques = combos.reduce((acc, el, i) => {
      if (!acc.find(x => x.id === el.id)) {
        acc.push(el);
      }

      return acc;
    }, []);

    return uniques;
  }
}

module.exports = Floor;