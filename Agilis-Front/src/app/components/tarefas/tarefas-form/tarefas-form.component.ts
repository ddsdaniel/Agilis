import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { constantes } from 'src/app/consts/constantes';
import { TipoTarefa, TipoTarefaLabel } from 'src/app/enums/tipo-tarefa.enum';
import { Produto } from 'src/app/models/produtos/produto';
import { Tarefa } from 'src/app/models/tarefas/tarefa';
import { UsuarioConsulta } from 'src/app/models/usuario-consulta';
import { FeatureApiService } from 'src/app/services/apis/produtos/feature-api.service';
import { ProdutoApiService } from 'src/app/services/apis/produtos/produto-api.service';
import { TarefaApiService } from 'src/app/services/apis/tarefa-api.service';
import { ComparadorService } from 'src/app/services/comparador.service';
import { TituloService } from 'src/app/services/titulo.service';
import { CrudFormComponent } from '../../crud/crud-form-component';

@Component({
  selector: 'app-tarefas-form',
  templateUrl: './tarefas-form.component.html',
  styleUrls: ['./tarefas-form.component.scss']
})
export class TarefasFormComponent extends CrudFormComponent<Tarefa> implements OnInit {

  usuarios: UsuarioConsulta[];
  produtos: Produto[] = [];
  tipos = Object.keys(TipoTarefa);

  constructor(
    private produtoApiService: ProdutoApiService,
    private featureApiService: FeatureApiService,
    router: Router,
    tarefaApiService: TarefaApiService,
    snackBar: MatSnackBar,
    tituloService: TituloService,
    protected activatedRoute: ActivatedRoute,
    public comparadorService: ComparadorService,
  ) {
    super(router, tarefaApiService, snackBar, activatedRoute, 'tarefas');
    tituloService.definir('Cadastro da Tarefa');
    this.inicializar();
  }

  inicializar() {
    this.obterProdutos();
    this.identificarFeature();
  }

  private identificarFeature() {
    this.activatedRoute.queryParams.subscribe({
      next: params => {
        if (params.featureId) {
          this.featureApiService.obterUm(params.featureId)
            .subscribe({
              next: feature => {
                this.entidade.feature = feature;
                this.entidade.featureId = feature.id;

                this.rotaPesquisa = `/produtos/${feature.epico.produtoId}/backlog`;
              }
            });
        }
      }
    });
  }

  private obterProdutos() {
    this.produtoApiService.obterTodos()
      .subscribe({
        next: produtos => this.produtos = produtos
      });
  }

  sugerirNovo(): void {
    this.entidade = {
      id: constantes.newGuid,
      titulo: '',
      descricao: '',
      featureId: constantes.newGuid,
      tipo: TipoTarefa.Novidade,
    };
  }

  salvar() {
    if (this.entidade.feature) {
      this.entidade.featureId = this.entidade.feature.id;
    }
    super.salvar();
  }

  obterLabelTipo(tipo: TipoTarefa): string {
    return TipoTarefaLabel.get(tipo);
  }

}
