import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { constantes } from 'src/app/consts/constantes';
import { RegraUsuario } from 'src/app/enums/regra-usuario.enum';
import { TipoTarefa, TipoTarefaLabel } from 'src/app/enums/tipo-tarefa.enum';
import { Produto } from 'src/app/models/produtos/produto';
import { UsuarioConsulta } from 'src/app/models/seguranca/usuario-consulta';
import { Tarefa } from 'src/app/models/tarefas/tarefa';
import { FeatureApiService } from 'src/app/services/apis/produtos/feature-api.service';
import { ProdutoApiService } from 'src/app/services/apis/produtos/produto-api.service';
import { TarefaApiService } from 'src/app/services/apis/tarefa-api.service';
import { UsuarioApiService } from 'src/app/services/apis/usuario-api.service';
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
    private usuarioApiService: UsuarioApiService,
    router: Router,
    tarefaApiService: TarefaApiService,
    snackBar: MatSnackBar,
    tituloService: TituloService,
    protected activatedRoute: ActivatedRoute,
    public comparadorService: ComparadorService,
  ) {
    super(router, tarefaApiService, snackBar, activatedRoute, 'tarefas');

    this.sugerirNovo();
    tituloService.definir('Cadastro da Tarefa');
    this.inicializar();
  }

  inicializar() {
    this.obterProdutos();
    this.obterUsuarios();
    this.identificarFeature();
  }

  obterUsuarios() {
    this.usuarioApiService.obterTodos()
      .subscribe({
        next: usuarios => this.usuarios = usuarios
      });
  }

  private identificarFeature() {
    this.activatedRoute.queryParams.subscribe({
      next: params => {
        if (params.featureId) {
          this.featureApiService.obterUm(params.featureId)
            .subscribe({
              next: feature => {
                this.entidade.feature = feature;

                this.rotaPesquisa = `/produtos/${feature.epico.produto.id}/backlog`;
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
      feature: {
        id: constantes.newGuid,
        nome: '',
        tarefas: [],
        epico: {
          id: constantes.newGuid,
          nome: '',
          features: [],
          produto: {
            id: constantes.newGuid,
            nome: '',
            descricao: '',
            urlRepositorio: '',
            epicos: []
          }
        }
      },
      relator: {
        id: constantes.newGuid,
        nome: '',
        sobrenome: '',
        ativo: true,
        email: '',
        regra: RegraUsuario.Usuario,
      },
      tipo: TipoTarefa.Novidade,
    };
  }

  obterLabelTipo(tipo: TipoTarefa): string {
    return TipoTarefaLabel.get(tipo);
  }

}
