import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { constantes } from 'src/app/consts/constantes';
import { Produto } from 'src/app/models/produtos/produto';
import { ProdutoApiService } from 'src/app/services/apis/produtos/produto-api.service';
import { ComparadorService } from 'src/app/services/comparador.service';
import { TituloService } from 'src/app/services/titulo.service';
import { CrudFormComponent } from '../../crud/crud-form-component';

@Component({
  selector: 'app-produtos-form',
  templateUrl: './produtos-form.component.html',
  styleUrls: ['./produtos-form.component.scss']
})
export class ProdutosFormComponent extends CrudFormComponent<Produto> implements OnInit {


  constructor(
    router: Router,
    produtoApiService: ProdutoApiService,
    snackBar: MatSnackBar,
    tituloService: TituloService,
    protected activatedRoute: ActivatedRoute,
    public comparadorService: ComparadorService,
  ) {
    super(router, produtoApiService, snackBar, activatedRoute, 'produtos');
    tituloService.definir('Cadastro do Produto');
  }

  sugerirNovo(): void {
    this.entidade = {
      id: constantes.newGuid,
      nome: '',
      descricao: '',
      urlRepositorio: '',
      epicos: [],
    };
  }

}
