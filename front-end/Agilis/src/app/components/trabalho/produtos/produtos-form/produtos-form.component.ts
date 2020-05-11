import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Produto } from 'src/app/models/trabalho/produtos/produto';
import { ProdutoApiService } from 'src/app/services/api/trabalho/produto-api.service';
import { HttpErrorResponse } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Observable } from 'rxjs';
import { Ator } from 'src/app/models/pessoas/ator';
import { AtorApiService } from 'src/app/services/api/pessoas/ator-api.service';

@Component({
  selector: 'app-produtos-form',
  templateUrl: './produtos-form.component.html',
  styleUrls: ['./produtos-form.component.scss']
})
export class ProdutosFormComponent implements OnInit {

  produto: Produto;
  atores: Observable<Ator[]>;

  constructor(
    private router: Router,
    private produtoApiService: ProdutoApiService,
    private snackBar: MatSnackBar,
    private atorApiService: AtorApiService,
  ) { }

  ngOnInit() {
    this.produto = {
      nome: '',
      ator: {
        id: '',
        nome: ''
      },
      narrativa: '',
      objetivo: '',
      historia: '',
    };

    this.atores = this.atorApiService.obteTodos();
    this.atores.subscribe(atores => this.produto.ator.id = atores[0].id);
  }

  salvar() {

    console.log(this.produto);

    this.produtoApiService.adicionar(this.produto)
      .subscribe(
        (id: string) => this.router.navigateByUrl('produtos'),
        (error: HttpErrorResponse) => {
          console.log(error);
          this.snackBar.open(error.message);
        }
      );
  }

  cancelar() {
    // TODO: mater o estado da pesquisa
    this.router.navigateByUrl('produtos');
  }

}
