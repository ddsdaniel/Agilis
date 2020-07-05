import { NestedTreeControl } from '@angular/cdk/tree';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { EntidadeNodo } from 'src/app/models/entidade-nodo';
import { NavigationMapApiService } from 'src/app/services/api/navigation-map-api.service';

@Component({
  selector: 'app-nav-map',
  templateUrl: './nav-map.component.html',
  styleUrls: ['./nav-map.component.scss']
})
export class NavMapComponent implements OnInit {

  constructor(
    private navigationMapApiService: NavigationMapApiService,
    private snackBar: MatSnackBar,
  ) {

  }

  treeControl = new NestedTreeControl<EntidadeNodo>(node => node.filhos);
  dataSource = new MatTreeNestedDataSource<EntidadeNodo>();

  hasChild = (_: number, node: EntidadeNodo) => !!node.filhos && node.filhos.length > 0;

  ngOnInit() {
    this.navigationMapApiService.obterUm()
      .subscribe(
        (root) => this.dataSource.data = root.filhos,
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

}
