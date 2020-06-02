import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { CrudComponent } from 'src/app/components/crud-component';
import { Produto } from 'src/app/models/trabalho/produtos/produto';
import { ProdutosApiService } from 'src/app/services/api/trabalho/produtos-api.service';

@Component({
  selector: 'app-produtos',
  templateUrl: './produtos.component.html',
  styleUrls: ['./produtos.component.scss']
})
export class ProdutosComponent extends CrudComponent<Produto> {

  constructor(
    public produtosApiService: ProdutosApiService,
    public snackBar: MatSnackBar,
    router: Router,
  ) {
    super(produtosApiService, snackBar, router, 'produtos');
  }
}
