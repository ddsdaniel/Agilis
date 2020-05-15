import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Produto } from 'src/app/models/trabalho/produtos/produto';
import { ProdutoApiService } from 'src/app/services/api/trabalho/produtos-api.service';
import { AutenticacaoService } from 'src/app/services/seguranca/autenticacao.service';

@Component({
  selector: 'app-produtos-form',
  templateUrl: './produtos-form.component.html',
  styleUrls: ['./produtos-form.component.scss']
})
export class ProdutosFormComponent implements OnInit {

  produto: Produto;

  constructor(
    private router: Router,
    private produtoApiService: ProdutoApiService,
    private snackBar: MatSnackBar,
    private autenticacaoService: AutenticacaoService,
  ) { }

  ngOnInit() {
    this.produto = {
      id: '00000000000000000000000000000000',
      nome: '',
      usuarioId: this.autenticacaoService.usuarioLogado.usuario.id
    };

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
