import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
import { tap } from 'rxjs/operators';
import { constantes } from 'src/app/consts/constantes';
import { Produto } from 'src/app/models/produtos/produto';
import { Release } from 'src/app/models/release';
import { ProdutoApiService } from 'src/app/services/apis/produtos/produto-api.service';
import { ReleaseApiService } from 'src/app/services/apis/release-api.service';
import { ComparadorService } from 'src/app/services/comparador.service';
import { TituloService } from 'src/app/services/titulo.service';
import { CrudFormComponent } from '../../crud/crud-form-component';

@Component({
  selector: 'app-releases-form',
  templateUrl: './releases-form.component.html',
  styleUrls: ['./releases-form.component.scss']
})
export class ReleasesFormComponent extends CrudFormComponent<Release> implements OnInit {

  produtos: Produto[] = [];

  constructor(
    private produtoApiService: ProdutoApiService,
    router: Router,
    releaseApiService: ReleaseApiService,
    snackBar: MatSnackBar,
    tituloService: TituloService,
    protected activatedRoute: ActivatedRoute,
    public comparadorService: ComparadorService,
  ) {
    super(router, releaseApiService, snackBar, activatedRoute, 'releases');
    tituloService.definir('Cadastro da Release');
  }

  carregarDependencias(): Observable<void> {
    return this.obterProdutos();
  }

  obterProdutos(): Observable<any> {
    return this.produtoApiService.obterTodos()
      .pipe(
        tap(produtos => this.produtos = produtos)
      );
  }

  sugerirNovo(): void {
    this.entidade = {
      id: constantes.newGuid,
      nome: '',
      observacoes: '',
      produto: null,
      versao: ''
    };
  }

}
