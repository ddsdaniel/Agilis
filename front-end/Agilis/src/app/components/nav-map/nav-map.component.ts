import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, OnInit } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { constantes } from 'src/app/constants/constantes';
import { EntidadeNodo } from 'src/app/models/entidade-nodo';

const TREE_DATA: EntidadeNodo[] = [
  {
    id: constantes.newGuid,
    nome: 'Fruit',
    filhos: [
      {
        id: constantes.newGuid,
        nome: 'Apple'
      },
      {
        id: constantes.newGuid,
        nome: 'Banana'
      },
      {
        id: constantes.newGuid,
        nome: 'Fruit loops'
      },
    ]
  }, {
    id: constantes.newGuid,
    nome: 'Vegetables',
    filhos: [
      {
        id: constantes.newGuid,
        nome: 'Green',
        filhos: [
          {
            id: constantes.newGuid,
            nome: 'Broccoli'
          },
          {
            id: constantes.newGuid,
            nome: 'Brussels sprouts'
          },
        ]
      }, {
        id: constantes.newGuid,
        nome: 'Orange',
        filhos: [
          {
            id: constantes.newGuid,
            nome: 'Pumpkins'
          },
          {
            id: constantes.newGuid,
            nome: 'Carrots'
          },
        ]
      },
    ]
  },
];

@Component({
  selector: 'app-nav-map',
  templateUrl: './nav-map.component.html',
  styleUrls: ['./nav-map.component.scss']
})
export class NavMapComponent implements OnInit {

  treeControl = new NestedTreeControl<EntidadeNodo>(node => node.filhos);
  dataSource = new MatTreeNestedDataSource<EntidadeNodo>();

  constructor() {
    this.dataSource.data = TREE_DATA;
  }

  hasChild = (_: number, node: EntidadeNodo) => !!node.filhos && node.filhos.length > 0;

  ngOnInit() {
  }

}
