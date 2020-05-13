Array.prototype.insert = insert;
Array.prototype.removeAt = removeAt;

interface Array<T> {
  insert: typeof insert;
  removeAt: typeof removeAt;
}

// adiciona na posicao
function insert<T>(index: number, element: T): T[] {
  return this.splice(index, 0, element);
}

// remove na posicao
function removeAt<T>(index: number): T[] {
  return this.splice(index, 1);
}


// export { }
// declare global {
//   interface Array<T> {
//     remove(elem: T): Array<T>;
//   }
// }

// if (!Array.prototype.remove) {
//   Array.prototype.remove = function <T>(elem: T): T[] {
//     return this.filter(e => e !== elem);
//   }
// }
