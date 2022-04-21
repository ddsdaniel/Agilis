import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/internal/operators/tap';
import { switchMap } from 'rxjs/operators';
import { constantes } from 'src/app/consts/constantes';
import { OperacaoFormCrud } from 'src/app/enums/operacao-form-crud.enum';
import { RegraUsuario } from 'src/app/enums/regra-usuario.enum';
import { TipoTarefa, TipoTarefaLabel } from 'src/app/enums/tipo-tarefa.enum';
import { UsuarioConsulta } from 'src/app/models/seguranca/usuario-consulta';
import { Tarefa } from 'src/app/models/tarefas/tarefa';
import { FeatureApiService } from 'src/app/services/apis/produtos/feature-api.service';
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
  tipos = Object.keys(TipoTarefa);

  constructor(
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
    tituloService.definir('Cadastro da Tarefa');
  }

  carregarDependencias(): Observable<void> {
    return this.obterUsuarios()
      .pipe(
        switchMap(_ => this.identificarFeature())
      );
  }

  obterUsuarios(): Observable<any> {
    return this.usuarioApiService.obterTodos()
      .pipe(
        tap(usuarios => this.usuarios = usuarios)
      );
  }

  private identificarFeature(): Observable<any> {
    return this.activatedRoute.queryParams
      .pipe(
        tap(params => {
          if (params.featureId) {
            this.featureApiService.obterUm(params.featureId)
              .subscribe({
                next: feature => {

                  if (super.operacao === OperacaoFormCrud.adicionando) {
                    this.entidade.feature = feature;
                  }
                  this.rotaPesquisa = `/produtos/${feature.epico.produto.id}/backlog`;
                }
              });
          }
        }
        ));
  }

  sugerirNovo(): void {
    this.entidade = {
      id: constantes.newGuid,
      titulo: '',
      descricao: '',
      horasPrevistas: '00:00',
      horasRealizadas: '00:00',
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
