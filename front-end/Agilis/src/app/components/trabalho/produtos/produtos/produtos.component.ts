import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Produto } from 'src/app/models/trabalho/produtos/produto';
import { ProdutosApiService } from 'src/app/services/api/trabalho/produtos-api.service';

@Component({
  selector: 'app-produtos',
  templateUrl: './produtos.component.html',
  styleUrls: ['./produtos.component.scss']
})
export class ProdutosComponent implements OnInit {

  produtos: Observable<Produto[]>;

  constructor(
    private userStoryApiService: ProdutosApiService,
    private snackBar: MatSnackBar,
    private router: Router,
  ) { }

  ngOnInit() {
    this.produtos = this.userStoryApiService.obterTodos();
    this.produtos.subscribe(
      () => { },
      (error: HttpErrorResponse) => this.snackBar.open(error.message)
    );
  }

  adicionar() {
    this.router.navigateByUrl('produtos/new');
  }
}
