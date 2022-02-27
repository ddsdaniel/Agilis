Array.prototype.insert = insert;
Array.prototype.removeAt = removeAt;
Array.prototype.moveToLast = moveToLast;

interface Array<T> {
  insert: typeof insert;
  removeAt: typeof removeAt;
  moveToLast: typeof moveToLast;
}

// adiciona na posicao
function insert<T>(index: number, element: T): T[] {
  return this.splice(index, 0, element);
}

// remove na posicao
function removeAt<T>(index: number): T[] {
  return this.splice(index, 1);
}

function moveToLast<T>(element: T): T[] {
  return this.push(this.splice(this.indexOf(element), 1)[0]);
}
